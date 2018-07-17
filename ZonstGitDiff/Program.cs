using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZonstGitDiff
{
    class Program
    {
        //C:\Users\dall\AppData\Local\Temp\TortoiseGit\game_create_room_view-60e9c4e.002.luac C:\Users\dall\AppData\Local\Temp\TortoiseGit\game_create_room_view-74c1c2d.002.luac
        //C:\Users\dall\AppData\Local\Temp\TortoiseGit\game_create_room_view-74c1c2d.002.luac

        static void Main(string[] args)
        {
            if(args.Length < 2)
            {
                Console.WriteLine("args num error");
                return;
            }

            for(int i = 0; i < args.Length; ++i)
            {
                Console.WriteLine(args[i]);
            }

            // Create a file to write to.
            string tmpPath = System.IO.Path.GetTempPath();

            if (!Directory.Exists(tmpPath + "\\decode")){
                Directory.CreateDirectory(tmpPath + "\\decode");
            }

            string newPath0 = Tool.DecodeLuac(args[0]);
            string newPath1 = Tool.DecodeLuac(args[1]);

            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "TortoiseGitMerge.exe";
            process.StartInfo.Arguments = newPath0 + " " + newPath1;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            process.Start();
        }
    }
}
