using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;//使用DllImport需要这个头文件

namespace ZonstGitDiff
{
    class Tool
    {
        [DllImport("LuaDecode.dll", EntryPoint = "stringcheck", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void stringcheck([MarshalAs(UnmanagedType.LPStr)]string x, [MarshalAs(UnmanagedType.LPStr)]string y, [Out, MarshalAs(UnmanagedType.LPStr)]StringBuilder z, int len);

        [DllImport("LuaDecode.dll", EntryPoint = "decodeluac", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int decodeluac([MarshalAs(UnmanagedType.LPStr)]string infile, [MarshalAs(UnmanagedType.LPStr)]string sign, [MarshalAs(UnmanagedType.LPStr)]string key, [Out, MarshalAs(UnmanagedType.LPStr)]StringBuilder z, int len);

        public static string DecodeLuac(string path)
        {

            if (File.Exists(path))
            {
                FileInfo fi1 = new FileInfo(path);
                if(fi1.Length == 0)
                {
                    return path;
                }

                int LEN = 4096;
                int times = 1;
                int ret = 0;
                StringBuilder _builder;
                do
                {
                    _builder = new StringBuilder(LEN * times);
                    ret = decodeluac(path, "XXTEA", "2dxLua", _builder, _builder.Capacity);
                    times++;
                } while (ret == 1);

                // Create a file to write to.
                string newPath = fi1.DirectoryName + "\\decode_" + fi1.Name;
                using (StreamWriter sw = File.CreateText(newPath))
                {
                    sw.Write(_builder);
                }

                //Console.WriteLine(_builder);
                return newPath;
            }
            return path;
        }

    }
}
