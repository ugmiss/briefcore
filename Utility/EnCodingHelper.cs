using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utility
{
    public class EnCodingHelper
    {
        public static Encoding GetEncoding(string file, Encoding defEnc)
        {
            using (var stream = File.OpenRead(file))
            {
                //判断流可读？
                if (!stream.CanRead)
                    return null;
                //字节数组存储BOM
                var bom = new byte[4];
                //实际读入的长度
                int readc;
                readc = stream.Read(bom, 0, 4);
                if (readc >= 2)
                {
                    if (readc >= 4)
                    {
                        //UTF32，Big-Endian
                        if (CheckBytes(bom, 4, 0x00, 0x00, 0xFE, 0xFF))
                            return new UTF32Encoding(true, true);
                        //UTF32，Little-Endian

                        if (CheckBytes(bom, 4, 0xFF, 0xFE, 0x00, 0x00))
                            return new UTF32Encoding(false, true);
                    }
                    //UTF8
                    if (readc >= 3 && CheckBytes(bom, 3, 0xEF, 0xBB, 0xBF))
                        return new UTF8Encoding(true);
                    //UTF16，Big-Endian
                    if (CheckBytes(bom, 2, 0xFE, 0xFF))
                        return new UnicodeEncoding(true, true);
                    //UTF16，Little-Endian
                    if (CheckBytes(bom, 2, 0xFF, 0xFE))
                        return new UnicodeEncoding(false, true);
                }
                return defEnc;
            }
        }
        //辅助函数，判断字节中的值
        static bool CheckBytes(byte[] bytes, int count, params int[] values)
        {
            for (int i = 0; i < count; i++)
                if (bytes[i] != values[i])
                    return false;
            return true;
        }
    }
}
