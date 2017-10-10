using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateManager
{
    class Constant
    {
        static public string MATCH_NAME_FAIL = "匹配失败";
        static public string MATCH_VERSION_FAIL = "有新版本";


        public static void CopyDir(string fromDir, string toDir)
        {
            if (!Directory.Exists(fromDir))
                return;

            if (!Directory.Exists(toDir))
            {
                Directory.CreateDirectory(toDir);
            }

            string[] files = Directory.GetFiles(fromDir);
            foreach (string formFileName in files)
            {
                string fileName = Path.GetFileName(formFileName);
                string toFileName = Path.Combine(toDir, fileName);
                File.Copy(formFileName, toFileName,true);
            }
            string[] fromDirs = Directory.GetDirectories(fromDir);
            foreach (string fromDirName in fromDirs)
            {
                string dirName = Path.GetFileName(fromDirName);
                string toDirName = Path.Combine(toDir, dirName);
                CopyDir(fromDirName, toDirName);
            }
        }

        public static DateTime GetLatestWriteTime(string directory,DateTime latest)
        {
            if (Directory.Exists(directory))
            {
                string[] files = Directory.GetFiles(directory);
                foreach (string filename in files)
                {
                    FileInfo info = new FileInfo(filename);
                    if (info.LastWriteTime > latest)
                        latest = info.LastWriteTime;
                }

                string[] dirs = Directory.GetDirectories(directory);
                foreach (string dirname in dirs)
                {
                    DateTime latesttime = GetLatestWriteTime(dirname, latest);
                    if (latesttime > latest)
                        latest = latesttime;
                }
            }
            return latest;
        }
    }

    // Implements the manual sorting of items by columns.
    class ListViewItemComparer : IComparer
    {
        private int col;
        public static bool reverse;
        public ListViewItemComparer()
        {
            col = 0;
            reverse = false;
        }
        public ListViewItemComparer(int column)
        {
            col = column;
            reverse = !reverse;
        }
        public int Compare(object x, object y)
        {
            if(reverse)
            {
                return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
            }
            else
            {
                return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }
    }

    class MyArgument
    {
        public enum WorkType
        {
            RELEASE_UPDATE,
            PLATFORM_UPDATE,
            FRAME_UPDATE,
            COPY_CHECK,
            COMMIT_WORK,
        }
        public string StrArg;
        public WorkType TypeArg;
        public string StrArg2;

        public MyArgument(string StrArg, WorkType IntArg)
        {
            this.StrArg = StrArg;
            this.TypeArg = IntArg;
        }

        public MyArgument(string StrArg)
        {
            this.StrArg = StrArg;
            this.TypeArg = WorkType.RELEASE_UPDATE;
        }
        public MyArgument(string StrArg,WorkType IntArg,string StrArg2)
        {
            this.StrArg = StrArg;
            this.TypeArg = IntArg;
            this.StrArg2 = StrArg2;
        }

    }
}
