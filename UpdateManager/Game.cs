using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateManager
{
    public class Game
    {
        //发布的名字 GameShangRaoMJ
        private string releasename;

        public string Releasename
        {
            get { return releasename; }
            set { releasename = value; }
        }
        //资源名字 shangraomj
        private string gamename = "UNKNOWN";

        public string Gamename
        {
            get { return gamename; }
            set { gamename = value; }
        }
        //游戏最新版本
        private string latestversion = "UNKNOWN";

        public string Latestversion
        {
            get { return latestversion; }
            set { latestversion = value; }
        }
        //游戏最新版本修改时间
        private DateTime latesttime;

        public DateTime Latesttime
        {
            get { return latesttime; }
            set { latesttime = value; }
        }
        //发布目录路径
        private string releasepath;

        public string Releasepath
        {
            get { return releasepath; }
            set { releasepath = value; }
        }

        public string[] Gameversions
        {
            get
            {
                return gameversions;
            }

            set
            {
                gameversions = value;
            }
        }
        //游戏所有版本
        private string[] gameversions = new string[0];

        public string[] GetVersions()
        {
            if (gameversions.Length <= 0)
                return null;

            string[] versions = new string[gameversions.Length];
            for(int i=0;i<gameversions.Length;i++)
            {
                int index = gameversions[i].LastIndexOf("\\");
                versions[i] = gameversions[i].Substring(index + 1);
            }
            return versions;
        }

    }
}
