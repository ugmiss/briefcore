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
using System.Collections;
using System.Text;
using System.Diagnostics;


namespace FTAlgorithm
{
    public enum T_Direction
    {
        /// <summary>
        /// 从左到右
        /// </summary>
        LeftToRight = 0,

        /// <summary>
        /// 从右到左
        /// </summary>
        RightToLeft = 1,
    }

    /// <summary>
    /// 单词信息
    /// </summary>
    public class T_WordInfo
    {
        /// <summary>
        /// 单词
        /// </summary>
        public String Word;

        /// <summary>
        /// 单词首字符在全文中的位置
        /// </summary>
        public int Position;

        /// <summary>
        /// 单词的权重级别
        /// </summary>
        public int Rank;

        /// <summary>
        /// 单词对应的标记
        /// </summary>
        public object Tag;
    }

    public delegate bool CompareByPosFunc(ArrayList words, ArrayList pre, ArrayList cur);

    /// <summary>
    /// 从全文中提取指定的单词，及其位置
    /// </summary>
    public class CExtractWords
    {
        CWordDfa m_WordDfa;
        ArrayList m_GameNodes;
        int m_MinSpace;
        int m_MinDeep;

        T_Direction m_MatchDirection;
        CompareByPosFunc m_CompareByPos;

        public CompareByPosFunc CompareByPosEvent
        {
            get
            {
                return m_CompareByPos;
            }

            set
            {
                m_CompareByPos = value;
            }
        }

        /// <summary>
        /// 匹配方向
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

        public CExtractWords()
        {
            m_MatchDirection = T_Direction.LeftToRight;
            m_WordDfa = new CWordDfa();
        }

        public void InsertWordToDfa(String word)
        {
            m_WordDfa.InsertWordToDfa(word);
        }

        private bool CompareGroup(ArrayList words, ArrayList pre, ArrayList cur, T_Direction direction)
        {
            int i ;

            if (direction == T_Direction.LeftToRight)
            {
                i = 0;
            }
            else
            {
                i = cur.Count - 1;
            }


            while ((direction == T_Direction.LeftToRight && i < cur.Count) ||
                (direction == T_Direction.RightToLeft && i >= 0))

            {
                if (i >= pre.Count)
                {
                    break;
                }

                int preId = (int)pre[i];
                int curId = (int)cur[i];

                if (((T_WordInfo)words[curId]).Word.Length > ((T_WordInfo)words[preId]).Word.Length)
                {
                    return true;
                }
                else if (((T_WordInfo)words[curId]).Word.Length < ((T_WordInfo)words[preId]).Word.Length)
                {
                    return false;
                }

                if (direction == T_Direction.LeftToRight)
                {
                    i++;
                }
                else
                {
                    i--;
                }
            }

            return false;
        }

        /// <summary>
        /// 博弈树
        /// </summary>
        /// <param name="words"></param>
        /// <param name="nodes"></param>
        /// <param name="init"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="spaceNum"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        private ArrayList GameTree(ArrayList words, ArrayList nodes, bool init, int begin, int end, ref int spaceNum, ref int deep)
        {
            if (init)
            {
                int startPos = ((T_WordInfo)words[begin]).Position;
                for (int i = begin; i <= end ; i++) 
                {
                    T_WordInfo wordInfo = (T_WordInfo)words[i];
                    spaceNum = wordInfo.Position - startPos;
                    deep = 0;
                    ArrayList oneNodes;

                    if (i == end)
                    {
                        oneNodes = new ArrayList();
                        oneNodes.Add(i);
                        deep++;
                    }
                    else
                    {
                        oneNodes = GameTree(words, nodes, false, i, end, ref spaceNum, ref deep);
                    }

                    if (oneNodes != null)
                    {
                        bool select = false;

                        if (m_MinSpace > spaceNum ||
                            (m_MinSpace == spaceNum && deep < m_MinDeep))
                        {
                            select = true;
                        }
                        else if (m_MinDeep == deep && m_MinSpace == spaceNum)
                        {
                            if (m_CompareByPos != null && m_MinSpace == 0)
                            {
                                select = m_CompareByPos(words, m_GameNodes, oneNodes);
                            }
                            else
                            {
                                select = CompareGroup(words, m_GameNodes, oneNodes, MatchDirection);
                            }
                        }


                        if (select)
                        {
                            m_MinDeep = deep;
                            m_MinSpace = spaceNum;
                            m_GameNodes.Clear();
                            foreach (object obj in oneNodes)
                            {
                                m_GameNodes.Add(obj);
                            }
                        }
                    }
                    deep = 0;
                    nodes.Clear();
                }
            }
            else
            {
                nodes.Add(begin);
                deep++;

                T_WordInfo last = (T_WordInfo)words[begin];

                bool nextStep = false;
                bool reach = false;
                int endPos = last.Position + last.Word.Length - 1;

                int oldDeep = deep;
                int oldSpace = spaceNum;

                for (int i = begin + 1; i <= end; i++)
                {
                    T_WordInfo cur = (T_WordInfo)words[i];

                    if (endPos < cur.Position + cur.Word.Length - 1)
                    {
                        endPos = cur.Position + cur.Word.Length - 1;
                    }


                    if (last.Position + last.Word.Length <= cur.Position)
                    {

                        nextStep = true;

                        if (reach)
                        {
                            reach = false;
                            spaceNum = oldSpace;
                            deep = oldDeep;
                            nodes.RemoveAt(nodes.Count - 1);
                        }

                        spaceNum += cur.Position - (last.Position + last.Word.Length);
                        ArrayList oneNodes;
                        oneNodes = GameTree(words, nodes, false, i, end, ref spaceNum, ref deep);

                        if (oneNodes != null)
                        {
                            bool select = false;

                            if (m_MinSpace > spaceNum ||
                                (m_MinSpace == spaceNum && deep < m_MinDeep))
                            {
                                select = true;
                            }
                            else if (m_MinDeep == deep && m_MinSpace == spaceNum)
                            {
                                if (m_CompareByPos != null && m_MinSpace == 0)
                                {
                                    select = m_CompareByPos(words, m_GameNodes, oneNodes);
                                }
                                else
                                {
                                    select = CompareGroup(words, m_GameNodes, oneNodes, MatchDirection);
                                }
                            }


                            if (select)
                            {
                                reach = true;
                                nextStep = false;
                                m_MinDeep = deep;
                                m_MinSpace = spaceNum;
                                m_GameNodes.Clear();
                                foreach (object obj in oneNodes)
                                {
                                    m_GameNodes.Add(obj);
                                }
                            }
                            else
                            {
                                spaceNum = oldSpace;
                                deep = oldDeep;
                                nodes.RemoveRange(deep, nodes.Count - deep);
                            }
                        }
                        else
                        {
                            spaceNum = oldSpace;
                            deep = oldDeep;
                            nodes.RemoveRange(deep , nodes.Count - deep);
                        }
                    }
                }

                if (!nextStep)
                {
                    spaceNum += endPos - (last.Position + last.Word.Length-1);

                    ArrayList ret = new ArrayList();

                    foreach (object obj in nodes)
                    {
                        ret.Add(obj);
                    }

                    return ret;
                }


            }

            return null;
        }

        /// <summary>
        /// 最大匹配提取全文中所有匹配的单词
        /// </summary>
        /// <param name="fullText">全文</param>
        /// <returns>返回T_WordInfo[]数组，如果没有找到一个匹配的单词，返回长度为0的数组</returns>
        public ArrayList ExtractFullTextMaxMatch(String fullText)
        {
            ArrayList retWords = new ArrayList();
            ArrayList words = ExtractFullText(fullText);

            int i = 0;

            while (i < words.Count)
            {
                T_WordInfo wordInfo = (T_WordInfo)words[i];

                int j;

                int rangeEndPos = 0;

                for (j = i; j < words.Count-1; j++)
                {
                    if (j - i > 16)
                    {
                        //嵌套太多的情况一般很少发生，如果发生，强行中断，以免造成博弈树遍历层次过多
                        //降低系统效率
                        break;
                    }

                    if (rangeEndPos < ((T_WordInfo)words[j]).Position + ((T_WordInfo)words[j]).Word.Length -1)
                    {
                        rangeEndPos = ((T_WordInfo)words[j]).Position + ((T_WordInfo)words[j]).Word.Length - 1;
                    }

                    if (rangeEndPos <
                        ((T_WordInfo)words[j + 1]).Position)  
                    {
                        break;
                    }
                }

                if (j > i)
                {
                    int spaceNum = 0;
                    int deep = 0;
                    m_GameNodes = new ArrayList();
                    m_MinDeep = 65535;
                    m_MinSpace = 65535 * 256;

                    GameTree(words, new ArrayList(), true, i, j, ref spaceNum, ref deep);

                    foreach (int index in m_GameNodes)
                    {
                        T_WordInfo info = (T_WordInfo)words[index];
                        retWords.Add(info);
                    }

                    i = j + 1;
                    continue;
                }
                else
                {
                    retWords.Add(wordInfo);
                    i++;
                }

                
            }

            return retWords;
        }


        /// <summary>
        /// 提取全文
        /// </summary>
        /// <param name="fullText">全文</param>
        /// <returns>返回T_WordInfo[]数组，如果没有找到一个匹配的单词，返回长度为0的数组</returns>
        public ArrayList ExtractFullText(String fullText)
        {
            ArrayList words = new ArrayList();

            if (fullText == null || fullText == "")
            {
                return words;
            }

            T_DfaUnit cur = null;
            bool find = false;
            int pos = 0;
            int i = 0;

            while (i < fullText.Length)
            {
                cur = m_WordDfa.Next(cur, fullText[i]);
                if (cur != null && !find)
                {
                    pos = i;
                    find = true;
                }

                if (find)
                {
                    if (cur == null)
                    {
                        find = false;
                        i = pos + 1; //有可能存在包含关系的词汇，所以需要回溯
                        continue;
                    }
                    else if (cur.QuitWord != null)
                    {
                        T_WordInfo wordInfo = new T_WordInfo();
                        wordInfo.Word = cur.QuitWord;
                        wordInfo.Position = pos;
                        wordInfo.Rank = m_WordDfa.GetRank(wordInfo.Word);
                        wordInfo.Tag = cur.Tag;
                        words.Add(wordInfo);

                        if (cur.Childs == null)
                        {
                            find = false;
                            cur = null;
                            i = pos + 1; //有可能存在包含关系的词汇，所以需要回溯
                            continue;
                        }
                    }
                }

                i++;
            }

            return words;
        }



    }
}
