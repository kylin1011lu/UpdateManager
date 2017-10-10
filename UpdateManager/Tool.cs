using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;//使用DllImport需要这个头文件

namespace UpdateManager
{
    class Tool
    {
        [DllImport("LuaDecode.dll", EntryPoint = "stringcheck", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern void stringcheck([MarshalAs(UnmanagedType.LPStr)]string x, [MarshalAs(UnmanagedType.LPStr)]string y, [Out, MarshalAs(UnmanagedType.LPStr)]StringBuilder z, int len);

        [DllImport("LuaDecode.dll", EntryPoint = "decodeluac", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern int decodeluac([MarshalAs(UnmanagedType.LPStr)]string infile, [MarshalAs(UnmanagedType.LPStr)]string sign, [MarshalAs(UnmanagedType.LPStr)]string key, [Out, MarshalAs(UnmanagedType.LPStr)]StringBuilder z, int len);

        public static Dictionary<string, string> DecodeLuac(string platformPath)
        {
            string filePathc = platformPath + "/src/gameItem_config.luac";
            string filePath = platformPath + "/src/gameItem_config.lua";
            if (File.Exists(filePathc))
            {
                int LEN = 4096;
                int times = 1;
                int ret = 0;
                StringBuilder _builder;
                do
                {
                    _builder = new StringBuilder(LEN * times);
                    ret = decodeluac(filePathc, "XXTEA", "2dxLua", _builder, _builder.Capacity);
                    times++;
                } while (ret == 1);
                //Console.WriteLine(_builder);
                return ParseItemConfig(_builder.ToString());
            }
            else if(File.Exists(filePath))
            {
                string readText = File.ReadAllText(filePath, Encoding.UTF8);
                //Console.WriteLine(readText);
                return ParseItemConfig(readText);
            }
            return new Dictionary<string, string>();
        }

        public static Dictionary<string, string> ParseItemConfig(string content)
        {
            Dictionary<string, string> games = new Dictionary<string, string>();

            int namei = 0;
            int namej = 0;
            int gamei = 0;
            int gamej = 0;
            while(true)
            {
                namei = content.IndexOf("name",namei);
                if (namei == -1)
                    break;
                namei = content.IndexOf("\"", namei);
                namej = content.IndexOf("\"", namei+1);

                string name = content.Substring(namei + 1, namej - (namei + 1));
                //Console.WriteLine(name);

                gamei = content.IndexOf(".game.",gamei);
                gamej = content.IndexOf("\"",gamei);
                string game = content.Substring(gamei + 6, gamej - (gamei + 6));
                //Console.WriteLine(game);

                if(!games.ContainsKey(game.Trim()))
                    games.Add(game.Trim(), name.Trim());

                namei = namei + 10;
                gamei = gamei + 10;
            }

            return games;
        }
    }
}
