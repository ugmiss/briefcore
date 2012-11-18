/***************************************************************************************
 * KTDictSeg 简介: KTDictSeg 是由KaiToo搜索开发的一款基于字典的简单中英文分词算法
 * 主要功能: 中英文分词，未登录词识别,多元歧义自动识别,全角字符识别能力
 * 主要性能指标:
 * 分词准确度:90%以上(有待专家的权威评测)
 * 处理速度: 600KBytes/s
 * 
 * 版本: V1.2.02 
 * Copyright(c) 2007 http://www.kaitoo.com 
 * 作者:肖波
 * 授权: 开源GPL
 * 公司网站: http://www.kaitoo.com
 * 个人博客: http://blog.csdn.net/eaglet; http://www.cnblogs.com/eaglet
 * 联系方式: blog.eaglet@gmail.com
 * ***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using FTAlgorithm;
using General;

namespace KTDictSeg
{
    /// <summary>
    /// 简单字典分词
    /// </summary>
    public class CSimpleDictSeg
    {
        const string CHS_STOP_WORD_FILENAME = "chsstopwords.txt";
        const string ENG_STOP_WORD_FILENAME = "engstopwords.txt";

        IRule[] m_Rules ;

        //中文停用词哈希表
        Hashtable m_ChsStopwordTbl = new Hashtable();

        //英文停用词哈希表
        Hashtable m_EngStopwordTbl = new Hashtable();

        CExtractWords m_ExtractWords;
        const string PATTERNS = @"[０-９\d]+\%|[０-９\d]{1,2}月|[０-９\d]{1,2}日|[０-９\d]{1,4}年|"+
            @"[０-９\d]{1,4}-[０-９\d]{1,2}-[０-９\d]{1,2}|" +
            @"[０-９\d]+|[^ａ-ｚＡ-Ｚa-zA-Z0-9０-９\u4e00-\u9fa5]|[ａ-ｚＡ-Ｚa-zA-Z]+|[\u4e00-\u9fa5]+";
        //const string PATTERNS = @"[a-zA-Z]+|\d+|[\u4e00-\u9fa5]+";

        String m_DictPath;

        CPOS m_POS;

        PosBinRule m_PosBinRule;

        /// <summary>
        /// 词性
        /// </summary>
        public CPOS Pos
        {
            get
            {
                return m_POS;
            }
        }

        /// <summary>
        /// 字典文件所在路径
        /// </summary>
        public String DictPath
        {
            get
            {
                return m_DictPath;
            }

            set
            {
                m_DictPath = value;
            }
        }


        int GetPosWeight(ArrayList words, ArrayList list)
        {
            int weight = 0;

            for (int i = 0; i < list.Count-1; i++)
            {
                T_WordInfo w1 = (T_WordInfo)words[(int)list[i]];
                T_WordInfo w2 = (T_WordInfo)words[(int)list[i+1]];
                if (m_PosBinRule.Match(w1.Word, w2.Word))
                {
                    weight++;
                }
            }

            return weight;
        }

        bool CompareByPos(ArrayList words, ArrayList pre, ArrayList cur)
        {
            return GetPosWeight(words, pre) < GetPosWeight(words, cur);
        }


        private void InitRules()
        {
            m_Rules = new IRule[3];
            m_PosBinRule = new PosBinRule(m_POS);
            m_Rules[0] = new MergeNumRule(m_POS);
            m_Rules[1] = m_PosBinRule;
            m_Rules[2] = new MatchName(m_POS);
        }

        public CSimpleDictSeg()
        {
            m_MatchName = false;
            m_FilterStopWords = false;
            m_MatchDirection = T_Direction.LeftToRight;
            m_ExtractWords = new CExtractWords();
            m_ExtractWords.CompareByPosEvent = CompareByPos;
            m_POS = new CPOS();
            InitRules();

        }

        /// <summary>
        /// 合并浮点数
        /// </summary>
        /// <param name="words"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private String MergeFloat(ArrayList words, int start, ref int end)
        {
            StringBuilder str = new StringBuilder();

            int dotCount = 0;
            end = start;
            int i ;

            for (i = start; i < words.Count; i++)
            {
                string word = (string)words[i];

                if (word == "")
                {
                    break;
                }
                
                if ((word[0] >= '0' && word[0] <= '9')
                    || (word[0] >= '０' && word[0] <= '９'))
                {
                }
                else if (word[0] == '.' && dotCount == 0)
                {
                    dotCount++;
                }
                else
                {
                    break;
                }

                str.Append(word);
            }

            end = i;

            return str.ToString();
        }

        /// <summary>
        /// 合并Email
        /// </summary>
        /// <param name="words"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private String MergeEmail(ArrayList words, int start, ref int end)
        {
            StringBuilder str = new StringBuilder();

            int dotCount = 0;
            int atCount = 0;
            end = start;
            int i;

            for (i = start; i < words.Count; i++)
            {
                string word = (string)words[i];

                if (word == "")
                {
                    break;
                }

                if ((word[0] >= 'a' && word[0] <= 'z') ||
                    (word[0] >= 'A' && word[0] <= 'Z') ||
                    word[0] >= '0' && word[0] <= '9')
                {
                    dotCount = 0;
                }
                else if (word[0] == '@' && atCount == 0)
                {
                    atCount++;
                }
                else if (word[0] == '.' && dotCount == 0)
                {
                    dotCount++;
                }
                else
                {
                    break;
                }

                str.Append(word);

            }

            end = i;

            return str.ToString();
        }

        #region 维护停用词

        /// <summary>
        /// 从停用词字典中加载停用词
        /// 停用词字典的格式：
        /// 文本文件格式，一个词占一行
        /// </summary>
        /// <param name="chsFileName">中文停用词</param>
        /// <param name="engFileName">英文停用词</param>
        /// <remarks>对文件存取的异常不做异常处理，由调用者进行异常处理</remarks>
        public void LoadStopwordsDict(String chsFileName, String engFileName)
        {
            int numChrStop = 0;//统计中文停用词数目，并作为Value值插入哈希表
            int numEngStop = 0;//统计英文停用词数目，并作为Value值插入哈希表

            try
            {
                StreamReader swChrFile = new StreamReader(chsFileName, Encoding.GetEncoding("UTF-8"));
                StreamReader swEngFile = new StreamReader(engFileName, Encoding.GetEncoding("UTF-8"));

                //加载中文停用词
                while (!swChrFile.EndOfStream)
                {
                    //按行读取中文停用词
                    string strChrStop = swChrFile.ReadLine();

                    //如果哈希表中不包括该停用词则添加到哈希表中
                    if (!m_ChsStopwordTbl.Contains(strChrStop))
                    {
                        m_ChsStopwordTbl.Add(strChrStop, numChrStop);
                        numChrStop++;
                    }
                }

                //加载英文停用词
                while (!swEngFile.EndOfStream)
                {
                    //按行读取中文停用词
                    string strEngStop = swEngFile.ReadLine();

                    //如果哈希表中不包括该停用词则添加到哈希表中
                    if (!m_EngStopwordTbl.Contains(strEngStop))
                    {
                        m_EngStopwordTbl.Add(strEngStop, numEngStop);
                        numEngStop++;
                    }
                }

                swChrFile.Close();
                swEngFile.Close();
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// 将中文停用词保存到文件中 
        /// </summary>
        /// <param name="fileName">要保存文件名</param>
        /// <remarks>对文件存取的异常不做异常处理，由调用者进行异常处理</remarks>
        public void SaveChsStopwordDict(String fileName)
        {
            try
            {
                //创建一个新的存储中文停用词的文本文件，若该文件存在则覆盖
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));


                //遍历中文停用词表，写入文件
                foreach (DictionaryEntry i in m_ChsStopwordTbl)
                {
                    sw.WriteLine(i.Key.ToString());
                }

                sw.Close();
                fs.Close();
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// 将英文停用词保存到文件中 
        /// </summary>
        /// <param name="fileName">要保存文件名</param>
        /// <remarks>对文件存取的异常不做异常处理，由调用者进行异常处理</remarks>
        public void SaveEngStopwordDict(String fileName)
        {
            try
            {
                //创建一个新的存储英文停用词的文本文件，若该文件存在则覆盖
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));


                //遍历英文停用词表，写入文件
                foreach (DictionaryEntry i in m_EngStopwordTbl)
                {
                    sw.WriteLine(i.Key.ToString());
                }
                sw.Close();
                fs.Close();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 增加一个中文停用词
        /// </summary>
        /// <param name="word"></param>
        public void AddChsStopword(String word)
        {
            //如果原来词库中已存在，则不做任何操作
            if (m_ChsStopwordTbl.Contains(word))
            {
                return;
            }
            else
            {
                m_ChsStopwordTbl.Add(word, m_ChsStopwordTbl.Count);

            }

        }


        /// <summary>
        /// 删除一个中文停用词
        /// </summary>
        /// <param name="word"></param>
        public void DelChsStopword(String word)
        {
            //如果原来词库中不存在，则不做任何操作
            m_ChsStopwordTbl.Remove(word);
        }


        /// <summary>
        /// 增加一个英文停用词
        /// </summary>
        /// <param name="word"></param>
        public void AddEngStopword(String word)
        {
            //如果原来词库中已存在，则不做任何操作
            if (m_EngStopwordTbl.Contains(word))
            {
                return;
            }
            else
            {
                m_EngStopwordTbl.Add(word, m_EngStopwordTbl.Count);
            }
        }


        /// <summary>
        /// 删除一个英文停用词
        /// </summary>
        /// <param name="word"></param>
        public void DelEngStopword(String word)
        {
            //如果原来词库中不存在，则不做任何操作
            m_EngStopwordTbl.Remove(word);
        }

        #endregion

        #region 加载字典

        /// <summary>
        /// 加载字典
        /// </summary>
        public void LoadDictFromTextFile()
        {
            String dictStr = CFile.ReadFileToString(m_DictPath + "Dict.txt", "utf-8");

            String[] words = CRegex.Split(dictStr, "\r\n");

            foreach (String word in words)
            {
                String[] wp = CRegex.Split(word, @"\|");

                if (wp == null)
                {
                    continue;
                }

                if (wp.Length != 2)
                {
                    continue;
                }

                int pos = 0;

                try
                {
                    pos = int.Parse(wp[1]);
                }
                catch
                {
                    continue;
                }

                m_ExtractWords.InsertWordToDfa(wp[0]);

                m_POS.AddWordPos(wp[0], pos);

            }


        }

        public void LoadDict()
        {
            Dict dict = new Dict();
            T_DictFile dictFile = dict.LoadFromBinFile(m_DictPath + "Dict.dct");

            foreach (T_DictStruct word in dictFile.Dicts)
            {
                m_ExtractWords.InsertWordToDfa(word.Word);
                m_POS.AddWordPos(word.Word, word.Pos);
            }
        }



        #endregion

        #region 分词属性
        bool m_MatchName;
        
        /// <summary>
        /// 是否匹配汉语人名
        /// </summary>
        public bool MatchName
        {
            get
            {
                return m_MatchName;
            }

            set
            {
                m_MatchName = value;
            }
        }

        T_Direction m_MatchDirection;

        /// <summary>
        /// 匹配方向
        /// 默认为从左至右匹配,即正向匹配
        /// </summary>
        public T_Direction MatchDirection
        {
            get
            {
                return m_MatchDirection;
            }

            set
            {
                m_MatchDirection = value;
            }
        }


        bool m_FilterStopWords;

        /// <summary>
        /// 是否过滤停用词
        /// </summary>
        public bool FilterStopWords
        {
            get
            {
                return m_FilterStopWords;
            }

            set
            {
                if (value)
                {
                    if (m_ChsStopwordTbl.Count == 0 || m_EngStopwordTbl.Count == 0)
                    {
                        LoadStopwordsDict(m_DictPath + CHS_STOP_WORD_FILENAME, m_DictPath + ENG_STOP_WORD_FILENAME);
                    }
                }

                m_FilterStopWords = value;

            }
        }


        #endregion


        #region 分词

        private void InsertWordToArray(String word, ArrayList arr)
        {
            if (m_FilterStopWords)
            {
                if (m_ChsStopwordTbl[word] != null || m_EngStopwordTbl[word] != null)
                {
                    return;
                }
            }

            arr.Add(word);
        }

        /// <summary>
        /// 预分词
        /// </summary>
        /// <param name="str">要分词的句子</param>
        /// <returns>预分词后的字符串输出</returns>
        private ArrayList PreSegment(String str)
        {
            ArrayList initSeg = new ArrayList();


            if (!CRegex.GetSingleMatchStrings(str, PATTERNS, true, ref initSeg))
            {
                return new ArrayList();
            }

            ArrayList retWords = new ArrayList();

            int i = 0;

            m_ExtractWords.MatchDirection = MatchDirection;

            while (i < initSeg.Count)
            {
                String word = (String)initSeg[i];
                if (word == "")
                {
                    word = " ";
                }

                if (i < initSeg.Count - 1)
                {
                    bool mergeOk = false;
                    if (((word[0] >= '0' && word[0] <= '9') ||(word[0] >= '０' && word[0] <= '９')) &&
                        ((word[word.Length - 1] >= '0' && word[word.Length - 1] <= '9') ||
                         (word[word.Length - 1] >= '０' && word[word.Length - 1] <= '９')) 
                        )

                    {
                        word = MergeFloat(initSeg, i, ref i);
                        mergeOk = true;
                    }
                    else if ((word[0] >= 'a' && word[0] <= 'z') ||
                             (word[0] >= 'A' && word[0] <= 'Z') 
                             )
                    {
                        if ((String)initSeg[i + 1] != "")
                        {
                            if (((String)initSeg[i + 1])[0] == '@')
                            {
                                word = MergeEmail(initSeg, i, ref i);
                                mergeOk = true;
                            }
                        }
                    }

                    if (mergeOk)
                    {
                        InsertWordToArray(word, retWords);
                        continue;
                    }
                }


                if (word[0] < 0x4e00 || word[0] > 0x9fa5)
                {
                    InsertWordToArray(word, retWords);
                }
                else
                {
                    ArrayList words = m_ExtractWords.ExtractFullTextMaxMatch(word);
                    int lastPos = 0;

                    foreach (T_WordInfo wordInfo in words)
                    {
                        
                        if (lastPos < wordInfo.Position)
                        {
/*
                            String unMatchWord = word.Substring(lastPos, wordInfo.Position - lastPos);

                            InsertWordToArray(unMatchWord, retWords);
*/ 

                            for (int j = lastPos; j < wordInfo.Position; j++)
                            {
                                InsertWordToArray(word[j].ToString(), retWords);
                            }
 
                        }
                         

                        lastPos = wordInfo.Position + wordInfo.Word.Length ;
                        InsertWordToArray(wordInfo.Word, retWords);
                    }

                    if (lastPos < word.Length)
                    {
                        InsertWordToArray(word.Substring(lastPos, word.Length - lastPos), retWords);
                    }
                }

                i++;
            }

            return retWords;
        }

        /// <summary>
        /// 召回停用词
        /// </summary>
        /// <returns></returns>
        private ArrayList RecoverUnknowWord(ArrayList words)
        {
            ArrayList retWords = new ArrayList();

            int i = 0;

            while (i < words.Count)
            {
                String w = (String)words[i];

                if (i == words.Count-1)
                {
                    retWords.Add(w);
                    break;
                }

                if (m_POS.IsUnknowOneCharWord(w))
                {
                    String word = w;
                    i++;

                    while (m_POS.IsUnknowOneCharWord((String)words[i]))
                    {
                        word += (String)words[i];
                        i++;
                        if (i >= words.Count)
                        {
                            break;
                        }
                    }

                    retWords.Add(word);
                    continue;
                }
                else
                {
                    retWords.Add(w);
                }

                i++;
            }

            return retWords;
    
        }

        /// <summary>
        /// 分词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public ArrayList Segment(String str)
        {
            ArrayList preWords = PreSegment(str);
            ArrayList retWords = new ArrayList();

            int index = 0 ;
            while (index < preWords.Count)
            {
                int next = -1;
                foreach (IRule rule in m_Rules)
                {
                    if (!m_MatchName && rule is MatchName)
                    {
                        continue;
                    }

                    next = rule.ProcRule(preWords, index, retWords);
                    if (next > 0)
                    {
                        index = next;
                        break;
                    }
                }

                if (next > 0)
                {
                    continue;
                }

                retWords.Add(preWords[index]);
                index++;
            }

            //return retWords;
            return RecoverUnknowWord(retWords);
        }

        #endregion
    }
}
