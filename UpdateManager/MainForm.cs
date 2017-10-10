using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace UpdateManager
{
    public partial class MainForm : Form
    {
        Dictionary<string, Game> gamedict = new Dictionary<string, Game>();
        ListViewCombox viewCombox;
        private bool bInit = false;

        public void SaveConfig()
        {
            string releasepath = TbRelease.Text;
            if(Directory.Exists(releasepath))
            {
                ConfigData.SetStringForKey(ConfigData.KEY_RELEASEPATH, releasepath);
            }

            string platformpath = TbPlatform.Text;
            if (Directory.Exists(platformpath))
            {
                ConfigData.SetStringForKey(ConfigData.KEY_PLATFORMPATH, platformpath);
            }

            string framepaht = TbFrame.Text;
            if (Directory.Exists(framepaht))
            {
                ConfigData.SetStringForKey(ConfigData.KEY_FRAMEPATH, framepaht);
            }

            string platforms = "";
            foreach(var item in LBPlatforms.Items)
            {
                platforms += item.ToString() + "|";
                Console.WriteLine(item.ToString());
            }
            ConfigData.SetStringForKey(ConfigData.KEY_PLATFORMLIST, platforms);
        }

        public void LoadConfig()
        {
            string content = ConfigData.GetStringForKey(ConfigData.KEY_RELEASEPATH, "请选择发布目录");
            TbRelease.Text = content;

            content = ConfigData.GetStringForKey(ConfigData.KEY_PLATFORMPATH, "请选择平台目录");
            TbPlatform.Text = content;

            content = ConfigData.GetStringForKey(ConfigData.KEY_FRAMEPATH, "请选择框架目录");
            TbFrame.Text = content;

            content = ConfigData.GetStringForKey(ConfigData.KEY_PLATFORMLIST, "");
            string[] split = content.Split(new Char[] { '|'});
            LBPlatforms.BeginUpdate();
            foreach (string item in split)
            {
                if (item.Trim() != "" && !LBPlatforms.Items.Contains(item))
                {
                    LBPlatforms.Items.Add(item);
                }
            }
            LBPlatforms.EndUpdate();

        }

        public MainForm()
        {
            InitializeComponent();

            //加载默认配置
            LoadConfig();
            InitializeMyComponent();

            //加载release游戏
            LoadReleaseGames();
            //加载platform游戏
            LoadPlatormGames();
            bInit = true;

        }

        private void InitializeMyComponent()
        {
            LVGames.Columns.Add("游戏名字", 200, HorizontalAlignment.Center);
            LVGames.Columns.Add("最新版本", 100, HorizontalAlignment.Center);
            LVGames.Columns.Add("更新日期", 120, HorizontalAlignment.Center);
            this.LVGames.ColumnClick += new ColumnClickEventHandler(LVRelease_ColumnClick);

            //ImageList imgList = new ImageList();
            //imgList.ImageSize = new Size(1, 28);
            //LVGames.SmallImageList = imgList;

            LVPlatGames.MouseUp += new MouseEventHandler(listview_MouseUp);
            //LVPlatGames.MouseMove += new MouseEventHandler(listview_MouseUp);
            LVPlatGames.ColumnWidthChanging += new ColumnWidthChangingEventHandler(listView_ColumnWidthChanging);
            this.LVPlatGames.ColumnClick += new ColumnClickEventHandler(LVPlatform_ColumnClick);

            LVPlatGames.Columns.Add("游戏名字", 210, HorizontalAlignment.Center);
            LVPlatGames.Columns.Add("匹配游戏", 180, HorizontalAlignment.Center);
            LVPlatGames.Columns.Add("版本状态", 120, HorizontalAlignment.Center);
            LVPlatGames.Columns.Add("更新版本", 100, HorizontalAlignment.Center);

            viewCombox = new ListViewCombox(LVPlatGames, CBVersionSelect, 3);

            loadwork.DoWork += new DoWorkEventHandler(BGWorker_DoWork);
            loadwork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGWorker_RunWorkerCompleted);
            CopyCheckWork.DoWork += new DoWorkEventHandler(CopyBGWorker_DoWork);
            CopyCheckWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OutputBGWorker_RunWorkerCompleted);

            CommitWork.DoWork += new DoWorkEventHandler(CommitBGWorker_DoWork);
            CommitWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OutputBGWorker_RunWorkerCompleted);
            CommitWork.ProgressChanged += new ProgressChangedEventHandler(CommitBGWorker_UpdateProgress);
        }

        public void LoadReleaseGames()
        {     
            LVGames.Items.Clear();
            gamedict.Clear();
            string releasepath = TbRelease.Text;
            if (Directory.Exists(releasepath))
            {
                string[] games = Directory.GetDirectories(releasepath);                
                foreach (string entry in games)
                {
                    FileInfo file = new FileInfo(entry);
                    if (file.Name.IndexOf('.') == -1)
                    {
                        Game game = new Game();
                        game.Releasename = file.Name;
                        game.Releasepath = entry;

                        SetGameInfo(entry,game);

                        if(!gamedict.ContainsKey(game.Gamename))
                            gamedict.Add(game.Gamename, game);

                        ListViewItem item1 = new ListViewItem(file.Name, 0);
                        //item1.UseItemStyleForSubItems = false;
                        ListViewItem.ListViewSubItem subitem = item1.SubItems.Add(game.Latestversion);
                        //subitem.BackColor = Color.Red;
                        item1.SubItems.Add(string.Format("{0:D2}/{1:D2} {2:D2}:{3:D2}", game.Latesttime.Month, game.Latesttime.Day, game.Latesttime.Hour, game.Latesttime.Minute));
                        
                        LVGames.Items.Add(item1);
                    }
                    Console.WriteLine(entry.ToString() + " " + file.Name);
                }
            }

        }
        private void LVRelease_ColumnClick(object o, ColumnClickEventArgs e)
        {
            this.LVGames.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }
        private void LVPlatform_ColumnClick(object o, ColumnClickEventArgs e)
        {
            this.LVPlatGames.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }
        //获取当前游戏的最新版本
        public void SetGameInfo(string gamepath,Game game)
        {
            string[] games = Directory.GetDirectories(gamepath);
            List<string> gamelist = new List<string>();
            foreach(string item in games)
            {
                if(Directory.Exists(item+"/res") && Directory.Exists(item+"/src") &&Directory.Exists(item+"/src/game/game"))
                {
                    gamelist.Add(item);
                }
            }
            if (gamelist.Count <= 0)
            {
                return;
            }

            gamelist.Sort(delegate(string x, string y)
            {
                FileInfo infox = new FileInfo(x.ToString());
                FileInfo infoy = new FileInfo(y.ToString());
                return DateTime.Compare(infox.LastWriteTime, infoy.LastWriteTime);
            });

            FileInfo file = new FileInfo(gamelist[gamelist.Count-1]);

            string[] names = Directory.GetDirectories(gamelist[gamelist.Count - 1] + "/src/game/game");
            if(names.Length > 0)
            {
                game.Gamename = new FileInfo(names[0]).Name;
            }
            game.Gameversions = gamelist.ToArray();
            game.Latesttime = Constant.GetLatestWriteTime(gamelist[gamelist.Count - 1], file.LastWriteTime);
            game.Latestversion = file.Name;
        }

        public void LoadPlatormGames()
        {
            LVPlatGames.Items.Clear();

            string platformpath = TbPlatform.Text;
            if (Directory.Exists(platformpath) && Directory.Exists(platformpath + "\\src\\game\\game"))
            {
                Dictionary<string, string> gamenames = Tool.DecodeLuac(platformpath);
                string[] games = Directory.GetDirectories(platformpath+"\\src\\game\\game");
                foreach (string entry in games)
                {
                    FileInfo file = new FileInfo(entry);
                    string gameName = file.Name;
                    if(gamenames.ContainsKey(file.Name))
                    {
                        gameName = gameName + "[" + gamenames[file.Name] + "]";
                    }
                    ListViewItem item1 = new ListViewItem(gameName, 0);
                    item1.Tag = file.Name;
                    item1.UseItemStyleForSubItems = false;
                    string matchname = Constant.MATCH_NAME_FAIL;
                    string matchversion = "";
                    bool bmatch = false;
                    string latestversion = "";
                    Game game = null;
                    if (gamedict.ContainsKey(file.Name))
                    {
                        game = gamedict[file.Name];
                        matchname = game.Releasename;
                        matchversion = Constant.MATCH_VERSION_FAIL;
                        bmatch = MatchLatestVersion(platformpath, game);
                        if(bmatch)
                        {
                            matchversion = "最新[" + game.Latestversion + "]";
                        }

                        latestversion = game.Latestversion;
                    }
                    ListViewItem.ListViewSubItem subitem = item1.SubItems.Add(matchname);
                    if(matchname == Constant.MATCH_NAME_FAIL)
                    {
                        subitem.BackColor = Color.Red;
                    }
                    subitem = item1.SubItems.Add(matchversion);
                    if (bmatch)
                    {
                        subitem.BackColor = Color.YellowGreen;
                    }

                    subitem = item1.SubItems.Add(latestversion);
                    if(game != null)
                        subitem.Tag = game.GetVersions();

                    LVPlatGames.Items.Add(item1);
                    Console.WriteLine(entry.ToString() + " " + file.Name);
                }
            }

        }
        private void listview_MouseUp(object sender,MouseEventArgs e)
        {
            viewCombox.Location(e.X, e.Y);
        }

        private void listView_ColumnWidthChanging(object sender,ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = LVPlatGames.Columns[e.ColumnIndex].Width;
        }
        //匹配版本号
        private bool MatchLatestVersion(string platformPath,Game game)
        {
            //string[] versions = game.Gameversions;
            //for(int i=versions.Length-1;i>=0;i--)
            //{
            //    string destsrcname = versions[i] + "/src/game/game/" + game.Gamename;
            //    string srcsrcname = platformPath+ "/src/game/game/" + game.Gamename;
            //    bool result = MatchDirectory(destsrcname, srcsrcname);
            //    Console.WriteLine(destsrcname + " " + srcsrcname + " " + result.ToString());
            //    if (result)
            //        break;
            //}

            string destsrcname = game.Releasepath+"/"+game.Latestversion + "/src/game/game/" + game.Gamename;
            string srcsrcname = platformPath+ "/src/game/game/" + game.Gamename;
            bool result = MatchDirectory(destsrcname, srcsrcname);
            Console.WriteLine(destsrcname + " " + srcsrcname + " " + result.ToString());

            if(result)
            {
                destsrcname = game.Releasepath + "/" + game.Latestversion + "/res/game/" + game.Gamename;
                srcsrcname = platformPath + "/res/game/" + game.Gamename;
                result = MatchDirectory(destsrcname, srcsrcname);
                Console.WriteLine(destsrcname + " " + srcsrcname + " " + result.ToString());
            }
            return result;      
        }

        private bool MatchDirectory(string destPath,string srcPath)
        {
            bool matchResult = true;
            if (Directory.Exists(destPath) && Directory.Exists(srcPath))
            {                
                string[] directories = Directory.GetDirectories(destPath);
                foreach (string entry in directories)
                {
                    FileInfo file = new FileInfo(entry);
                    if (Directory.Exists(destPath+"/"+file.Name))
                    {
                        if (Directory.Exists(srcPath + "/" + file.Name))
                        {
                            matchResult = MatchDirectory(destPath + "/" + file.Name, srcPath + "/" + file.Name);
                            if (!matchResult)
                                Console.WriteLine("*****NO_MATCH_DIRECTORY****" + srcPath + "/" + file.Name);
                        }
                        else
                        {
                            matchResult = false;
                            Console.WriteLine("*****NO_EXIST_DIRECTORY****" + srcPath + "/" + file.Name);
                        }
                    }

                    if (!matchResult)
                        return matchResult;
                }

                string[] files = Directory.GetFiles(destPath);
                foreach(string entry in files)
                {
                    FileInfo file = new FileInfo(entry);
                    if (File.Exists(destPath + "/" + file.Name))
                    {
                        if (File.Exists(srcPath + "/" + file.Name))
                        {
                            FileInfo filex = new FileInfo(destPath + "/" + file.Name);
                            FileInfo filey = new FileInfo(srcPath + "/" + file.Name);

                            if (DateTime.Compare(filex.LastWriteTime, filey.LastWriteTime) != 0)
                            {
                                matchResult = false;
                            }
                            if (!matchResult)
                                Console.WriteLine("*****NO_MATCH_FILE****" + srcPath + "/" + file.Name);
                        }
                        else
                        {
                            matchResult = false;
                            Console.WriteLine("*****NO_EXIST_FILE****" + srcPath + "/" + file.Name);
                        }
                    }
                    if (!matchResult)
                        return matchResult;
                }
            }

            return matchResult;
        }

        private void BtnSelectPlatform_Click(object sender, EventArgs e)
        {
            var platformfolder = new FolderBrowserDialog();
            string releasepath = TbPlatform.Text;
            if (Directory.Exists(releasepath))
            {
                DirectoryInfo info = Directory.GetParent(releasepath);
                platformfolder.SelectedPath = info.FullName;
            }
            DialogResult result = platformfolder.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = platformfolder.SelectedPath;
                this.TbPlatform.Text = folderName;
                Console.WriteLine(folderName);
                LoadPlatormGames();
            }
        }

        private void BtnSelectRelease_Click(object sender, EventArgs e)
        {
            var platformfolder = new FolderBrowserDialog();
            string releasepath = TbRelease.Text;
            if (Directory.Exists(releasepath))
            {
                DirectoryInfo info = Directory.GetParent(releasepath);
                platformfolder.SelectedPath = info.FullName;
            }
            DialogResult result = platformfolder.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = platformfolder.SelectedPath;
                this.TbRelease.Text = folderName;
                Console.WriteLine(folderName);

                //加载release游戏
                LoadReleaseGames();
            }
        }

        private void TbRelease_TextChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            //加载release游戏
            LoadReleaseGames();
            LoadPlatormGames();
        }

        private void TbPlatform_TextChanged(object sender, EventArgs e)
        {
            if (!bInit)
                return;
            LoadPlatormGames();
        }

        private void BtnAddPlat_Click(object sender, EventArgs e)
        {
            string platformpath = TbPlatform.Text.Trim();
            if (Directory.Exists(platformpath))
            {
                if(!LBPlatforms.Items.Contains(platformpath))
                {
                    LBPlatforms.BeginUpdate();
                    LBPlatforms.Items.Add(platformpath);
                    LBPlatforms.EndUpdate();
                }
            }
            else
            {
                MessageBox.Show(platformpath+"不是有效文件夹","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDeletePlat_Click(object sender, EventArgs e)
        {
            if(LBPlatforms.SelectedItem ==null)
            {
                MessageBox.Show("请先选择一个平台", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                LBPlatforms.BeginUpdate();
                LBPlatforms.Items.Remove(LBPlatforms.SelectedItem.ToString());
                LBPlatforms.EndUpdate();
            }
        }

        private void LBPlatforms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(LBPlatforms.SelectedItem!=null)
                TbPlatform.Text = LBPlatforms.SelectedItem.ToString();
        }

        private void BtnUpdatePlat_Click(object sender, EventArgs e)
        {
            string platformpath = TbPlatform.Text.Trim();
            if (Directory.Exists(platformpath))
            {
                if (!loadwork.IsBusy)
                {
                    BtnUpdatePlat.Enabled = false;
                    OutputToTextBox("开始更新" + platformpath);
                    loadwork.RunWorkerAsync(new MyArgument(platformpath, MyArgument.WorkType.PLATFORM_UPDATE));
                }
                else
                {
                    OutputToTextBox("后台处于繁忙状态,请等待");
                }
            }
            else
            {
                OutputToTextBox(platformpath + "不是有效目录");
            }
        }

        private void BtnUpdateRelease_Click(object sender, EventArgs e)
        {
            string releasePath = TbRelease.Text.Trim();
            if (Directory.Exists(releasePath))
            {             
                if(!loadwork.IsBusy)
                {
                    BtnUpdateRelease.Enabled = false;   
                    OutputToTextBox("开始更新" + releasePath);
                    loadwork.RunWorkerAsync(new MyArgument(releasePath,MyArgument.WorkType.RELEASE_UPDATE));
                }
                else
                {
                    OutputToTextBox("后台处于繁忙状态,请等待");
                }                    
            }
            else
            {
                OutputToTextBox(releasePath+"不是有效目录");
            }
        }
        #region BGWorkEvent
        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MyArgument arg = (MyArgument)e.Argument;
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "git.exe";
            process.StartInfo.WorkingDirectory = arg.StrArg;
            process.StartInfo.Arguments = "pull";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            //process.StartInfo.CreateNoWindow = true;
            process.Start();

            StreamReader reader = process.StandardOutput;
            string output = reader.ReadToEnd();
            output = output.Trim();
            output = output.Replace("\n", "\r\n");
            //MessageBox.Show(output, "提示", MessageBoxButtons.OK);
            e.Result = new MyArgument(output, arg.TypeArg);
            //Console.WriteLine(output);
            process.WaitForExit();
            process.Close();
        }
        private void BGWorker_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || e.Cancelled)
                return;
            MyArgument result = (MyArgument)e.Result;
            if (result.TypeArg == MyArgument.WorkType.RELEASE_UPDATE)
            {
                BtnUpdateRelease.Enabled = true;
                LoadReleaseGames();
                LoadPlatormGames();
            }
            else if (result.TypeArg == MyArgument.WorkType.PLATFORM_UPDATE)
            {
                BtnUpdatePlat.Enabled = true;
                LoadPlatormGames();
            }
            else if (result.TypeArg == MyArgument.WorkType.FRAME_UPDATE)
            {
                BtnUpdateFrame.Enabled = true;
            }    
            OutputToTextBox(result.StrArg);
        }
        private void CopyBGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MyArgument arg = (MyArgument)e.Argument;
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "git.exe";
            process.StartInfo.WorkingDirectory = arg.StrArg;
            process.StartInfo.Arguments = "status";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            StreamReader reader = process.StandardOutput;
            string output = reader.ReadToEnd();
            output = output.Trim();
            output = output.Replace("\n", "\r\n");
            e.Result = new MyArgument(output,arg.TypeArg,arg.StrArg2);
            process.WaitForExit();
            process.Close();
        }
        private void OutputBGWorker_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null || e.Cancelled)
                return;
            MyArgument result = (MyArgument)e.Result;
            OutputToTextBox(result.StrArg);

            if (result.TypeArg == MyArgument.WorkType.COPY_CHECK && CBCopyPush.Checked == true)
            {
                if (!CommitWork.IsBusy)
                {
                    OutputToTextBox("开始提交");
                    CommitWork.RunWorkerAsync(new MyArgument(TbPlatform.Text.Trim(),MyArgument.WorkType.COMMIT_WORK,result.StrArg2));
                }
                else
                {
                    OutputToTextBox("提交后台任务繁忙中，请等待");
                }
            }
        }
        private void CommitBGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            MyArgument arg = (MyArgument)e.Argument;
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "git.exe";
            process.StartInfo.WorkingDirectory = arg.StrArg;
            process.StartInfo.Arguments = "add res/game/. src/game/game/.";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
            process.Close();

            process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "git.exe";
            process.StartInfo.WorkingDirectory = arg.StrArg;
            process.StartInfo.Arguments = "commit -m \"" + arg.StrArg2 + "\"";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            StreamReader reader = process.StandardOutput;
            string commitoutput = reader.ReadToEnd();
            commitoutput = commitoutput.Replace("\n", "\r\n");
            process.WaitForExit();
            process.Close();

            process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "git.exe";
            process.StartInfo.WorkingDirectory = arg.StrArg;
            process.StartInfo.Arguments = "push";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            process.StartInfo.CreateNoWindow = true;
            process.Start();

            reader = process.StandardOutput;
            string pushoutput = reader.ReadToEnd();
            pushoutput = pushoutput.Replace("\n", "\r\n");
            process.WaitForExit();
            process.Close();

            e.Result = new MyArgument(commitoutput+pushoutput);
        }

        private void CommitBGWorker_UpdateProgress(object sender, ProgressChangedEventArgs e)
        {
            OutputToTextBox(e.UserState.ToString());
        }
        #endregion
        private void BtnCopy_Click(object sender, EventArgs e)
        {
            string framePath = TbFrame.Text.Trim();
            if(CBFrameCode.Checked || CBFrameSource.Checked)
            {
                if (!Directory.Exists(framePath))
                {
                    MessageBox.Show("框架目录不是有效文件夹", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            TBLog.Text = "";
            TBLog.Refresh();
            OutputToTextBox("===================================");
            OutputToTextBox("开始拷贝");

            string platformPath = TbPlatform.Text.Trim();
            BtnCopy.Enabled = false;
            if(CBFrameSource.Checked)
            {
                OutputToTextBox("开始框架资源拷贝");
                //string srcpath = platformPath + "\\gameFrameworks\\src";
                //Directory.Delete(srcpath, true);
                //OutputToTextBox("删除目录：" + srcpath);
                //string respath = platformPath + "\\gameFrameworks\\res";
                //Directory.Delete(respath, true);
                //OutputToTextBox("删除目录：" + respath);

                string fromsrcpath = framePath + "\\src";
                string tosrcpath = platformPath + "\\gameFrameworks\\src";
                Constant.CopyDir(fromsrcpath, tosrcpath);
                OutputToTextBox("拷贝目录：" + fromsrcpath);

                string fromrespath = framePath + "\\res";
                string torespath = platformPath + "\\gameFrameworks\\res";
                Constant.CopyDir(fromrespath, torespath);
                OutputToTextBox("拷贝目录：" + fromrespath);
                OutputToTextBox("框架资源拷贝完成");
            }

            if(CBFrameCode.Checked)
            {
                OutputToTextBox("开始框架c++拷贝");
                string fromrespath = framePath + "\\c\\frameworks";
                string torespath = platformPath + "\\frameworks";
                Constant.CopyDir(fromrespath, torespath);
                OutputToTextBox("拷贝目录：" + fromrespath);
                OutputToTextBox("框架c++拷贝完成");
            }

            BtnCopy.Enabled = true;

            ListView.CheckedListViewItemCollection checkedItems = LVPlatGames.CheckedItems;
            if (checkedItems == null || checkedItems.Count == 0)
            {
                return;
            }

            string releasePath = TbRelease.Text.Trim();
            if (!Directory.Exists(releasePath))
            {
                OutputToTextBox("发布目录不是有效文件夹,游戏拷贝失败");
                return;
            }

            List<int> copys = new List<int>();
            OutputToTextBox("检测拷贝的游戏");
            foreach (ListViewItem item in checkedItems)
            {                    
                if(item.SubItems[1].Text == Constant.MATCH_NAME_FAIL)
                {
                    OutputToTextBox(item.SubItems[0].Text + "没有匹配游戏");
                    continue;
                }
                if (item.SubItems[3].Text == "")
                {
                    OutputToTextBox(item.SubItems[0].Text + "没有选择更新版本");
                    continue;
                }

                if(!Directory.Exists(releasePath+"\\"+ item.SubItems[1].Text+"\\"+ item.SubItems[3].Text))
                {
                    OutputToTextBox(item.SubItems[0].Text + "没有对应版本目录"+ releasePath + "\\" + item.SubItems[1].Text + "\\" + item.SubItems[3].Text);
                    continue;
                }
                copys.Add(item.Index);
                Console.WriteLine(item.SubItems[0].Text +" "+ item.SubItems[1].Text+" " + item.SubItems[2].Text+ " " + item.SubItems[3].Text);
            }

            if(copys.Count ==0)
            {
                OutputToTextBox("有效游戏数量为0");
                return;
            }

            BtnCopy.Enabled = false;
            OutputToTextBox("进行游戏拷贝");

            int processvalue = 0;
            string autoPushCommit = "更新游戏";
            foreach(int index in copys)
            {
                ListViewItem item = LVPlatGames.Items[index];
                string srcpath = platformPath + "\\src\\game\\game\\" + item.Tag;
                Directory.Delete(srcpath,true);
                OutputToTextBox("删除目录：" + srcpath);
                string respath = platformPath + "\\res\\game\\" + item.Tag;
                Directory.Delete(respath, true);
                OutputToTextBox("删除目录：" + respath);

                string fromsrcpath = releasePath + "\\" + item.SubItems[1].Text + "\\" + item.SubItems[3].Text + "\\src";
                string tosrcpath = platformPath + "\\src";
                Constant.CopyDir(fromsrcpath, tosrcpath);
                OutputToTextBox("拷贝目录：" + fromsrcpath);

                string fromrespath = releasePath + "\\" + item.SubItems[1].Text + "\\" + item.SubItems[3].Text + "\\res";
                string torespath = platformPath + "\\res";
                Constant.CopyDir(fromrespath, torespath);
                OutputToTextBox("拷贝目录：" + fromrespath);
                OutputToTextBox(item.SubItems[1].Text+"拷贝成功");
                processvalue++;
                PBCopy.Value = 100 * processvalue / copys.Count;

                string commit = item.SubItems[0].Text;
                commit = commit.Substring(commit.IndexOf('[') + 1, commit.IndexOf(']')- commit.IndexOf('[')-1);
                autoPushCommit += " " + commit;
            }
            OutputToTextBox("游戏拷贝完成");
            BtnCopy.Enabled = true;
            LoadPlatormGames();

            if (!CopyCheckWork.IsBusy)
                CopyCheckWork.RunWorkerAsync(new MyArgument(platformPath, MyArgument.WorkType.COPY_CHECK, autoPushCommit + "[本次提交由-" + this.Text + "-自动完成]"));
        }

        private void OutputToTextBox(string log)
        {
            //if (TBLog.GetLineFromCharIndex(TBLog.Text.Length) > 100)
            //    TBLog.Text = "";

            TBLog.AppendText(DateTime.Now.ToString("HH:mm:ss  ") + log + "\r\n");
        }

        private void BtnSelectFrame_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            string framepath = TbFrame.Text;
            if (Directory.Exists(framepath))
            {
                DirectoryInfo info = Directory.GetParent(framepath);
                dialog.SelectedPath = info.FullName;
            }
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = dialog.SelectedPath;
                this.TbFrame.Text = folderName;
            }
        }

        private void BtnUpdateFrame_Click(object sender, EventArgs e)
        {
            string path = TbFrame.Text.Trim();
            if (Directory.Exists(path))
            {
                if (!loadwork.IsBusy)
                {
                    BtnUpdateFrame.Enabled = false;
                    OutputToTextBox("开始更新" + path);
                    loadwork.RunWorkerAsync(new MyArgument(path, MyArgument.WorkType.FRAME_UPDATE));
                }
                else
                {
                    OutputToTextBox("后台处于繁忙状态,请等待");
                }
            }
            else
            {
                OutputToTextBox(path + "不是有效目录");
            }
        }

        private void CBFrameSource_CheckedChanged(object sender, EventArgs e)
        {
            if (CBFrameSource.Checked)
            {
                CBCopyPush.Checked = false;
                CBCopyPush.Enabled = false;
            }
            else if(!CBFrameCode.Checked)
                CBCopyPush.Enabled = true;
        }

        private void CBFrameCode_CheckedChanged(object sender, EventArgs e)
        {
            if (CBFrameCode.Checked)
            {
                CBCopyPush.Checked = false;
                CBCopyPush.Enabled = false;
            }
            else if (!CBFrameSource.Checked)
                CBCopyPush.Enabled = true;
        }
    }
}
