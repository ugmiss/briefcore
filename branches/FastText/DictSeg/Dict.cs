using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using General;

namespace KTDictSeg
{
    [Serializable]
    public class T_DictFile
    {
        public ArrayList Dicts = new ArrayList();
    }

    [Serializable]
    public class T_DictStruct
    {
        /// <summary>
        /// 单词
        /// </summary>
        public String Word;

        /// <summary>
        /// 词性
        /// </summary>
        public int Pos;
    }

    public class Dict
    {
        /// <summary>
        /// 从文本文件读取字典
        /// </summary>
        /// <param name="fileName"></param>
        public T_DictFile LoadFromTextDict(String fileName)
        {
            T_DictFile dictFile = new T_DictFile();

            String dictStr = CFile.ReadFileToString(fileName, "utf-8");

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

                T_DictStruct dict = new T_DictStruct();
                dict.Word = wp[0];
                dict.Pos = pos;

                if (dict.Word.Contains("一") || dict.Word.Contains("二") ||
                    dict.Word.Contains("三") || dict.Word.Contains("四") ||
                    dict.Word.Contains("五") || dict.Word.Contains("六") ||
                    dict.Word.Contains("七") || dict.Word.Contains("八") ||
                    dict.Word.Contains("九") || dict.Word.Contains("十"))
                {
                    dict.Pos |= (int)T_POS.POS_A_M;
                }

                if (dict.Word == "字典")
                {
                    dict.Pos = (int)T_POS.POS_D_N;
                }
            
                dictFile.Dicts.Add(dict);
            }

            return dictFile;
        }

        public void SaveToBinFile(String fileName, T_DictFile dictFile)
        {
            Stream s = CSerialization.SerializeBinary(dictFile);
            s.Position = 0;
            CFile.WriteStream(fileName, (MemoryStream)s);
        }

        public T_DictFile LoadFromBinFile(String fileName)
        {
            MemoryStream s = CFile.ReadFileToStream(fileName);
            s.Position = 0;
            object obj;
            CSerialization.DeserializeBinary(s, out obj);
            return (T_DictFile)obj;
        }
    }
}
