using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace InteriorsproxiesCreator
{
    public partial class Forms : Form
    {
        private int proxy = 2000;
        public Forms()
        {
            InitializeComponent();
        }

        private void Forms_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// フォルダ選択画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openDirDialog_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = Path.GetDirectoryName(Application.ExecutablePath);
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string selectedPath = folderBrowserDialog.SelectedPath;

                if (Directory.Exists(selectedPath))
                {
                    resourcefolder.Text = selectedPath;
                }
            }
        }

        /// <summary>
        /// 開始ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doStartButton_Click(object sender, EventArgs e)
        {
            openDirDialog.Enabled = false;
            doStartButton.Enabled = false;
            proxy = 2000;
            string directory = resourcefolder.Text;
            if (Directory.Exists(directory))
            {
                SearchFxmanifest(directory);
            }
            openDirDialog.Enabled = true;
            doStartButton.Enabled = true;
        }

        /// <summary>
        /// マニフェストがあるフォルダを探す
        /// </summary>
        /// <param name="dir"></param>
        private void SearchFxmanifest(string dir)
        {
            string manifest = Path.Combine(dir, "fxmanifest.lua");
            if (File.Exists(manifest))
            {
                CreateInteriorproxies(dir);
            }
            else
            {
                string[] subFolders = Directory.GetDirectories(dir, "*", System.IO.SearchOption.TopDirectoryOnly);
                foreach (string subFolder in subFolders)
                {
                    SearchFxmanifest(subFolder);
                }
            }
            Application.DoEvents();
        }

        /// <summary>
        /// milo_.ymapファイルがある場所をさがしてinteriorproxies.metaを作成する
        /// fxmanifestのfilesに追加する
        /// </summary>
        /// <param name="dir"></param>
        private void CreateInteriorproxies(string dir)
        {
            bool findmilo = false;
            string[] files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
            List<string> miloList = new List<string>();
            foreach (string file in files)
            {
                if (file.EndsWith("milo_.ymap"))
                {
                    miloList.Add(Path.GetFileNameWithoutExtension(file));
                    findmilo = true;
                }
            }

            if(findmilo)
            {
                string interiorproxiesFile = Path.Combine(dir, "interiorproxies.meta");
                using (StreamWriter sw = new StreamWriter(interiorproxiesFile, false))
                {
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    sw.WriteLine("<SInteriorOrderData>");
                    sw.WriteLine("\t<startFrom value=\"" + proxy + "\" />");
                    sw.WriteLine("\t<proxies>");
                    foreach (string milo in miloList)
                    {
                        sw.WriteLine("\t\t<Item>" + milo + "</Item>");
                        proxy++;
                    }
                    sw.WriteLine("\t</proxies>");
                    sw.WriteLine("</SInteriorOrderData>");
                }

                string manifest = Path.Combine(dir, "fxmanifest.lua");
                string texts = File.ReadAllText(manifest);
                if (!texts.Contains("interiorproxies.meta"))
                {
                    string[] lines = File.ReadAllLines(manifest);
                    List<string> filenames = new List<string>();
                    using (StreamWriter sw = new StreamWriter(manifest, false))
                    {
                        bool writeproxies = false;
                        bool findfiles = false;
                        foreach (string line in lines)
                        {
                            if (line.StartsWith("files"))
                            {
                                findfiles = true;
                                //1行の場合
                                Match match = Regex.Match(line, @"'[\w\.]+'");
                                if (match.Success)
                                {
                                    filenames.Add(match.Value);

                                    sw.WriteLine("data_file 'INTERIOR_PROXY_ORDER_FILE' 'interiorproxies.meta'");
                                    sw.WriteLine("files {");
                                    sw.WriteLine("\t" + match.Value+",");
                                    sw.WriteLine("\t'interiorproxies.meta',");
                                    sw.WriteLine("}");
                                    writeproxies = true;
                                    findfiles = false;
                                    continue;
                                }
                            }
                            if (findfiles)
                            {
                                Match match = Regex.Match(line, @"'[\w\.]+'");
                                if (match.Success)
                                {
                                    filenames.Add(match.Value);
                                }

                                if (line.Contains("}"))
                                {
                                    sw.WriteLine("data_file 'INTERIOR_PROXY_ORDER_FILE' 'interiorproxies.meta'");
                                    sw.WriteLine("files {");
                                    foreach (string filename in filenames)
                                    {
                                        sw.WriteLine("\t" + filename + ",");
                                    }
                                    sw.WriteLine("\t'interiorproxies.meta',");
                                    sw.WriteLine("}");
                                    writeproxies = true;
                                    findfiles = false;
                                    continue;
                                }
                            }

                            if (!findfiles)
                            {
                                sw.WriteLine(line);
                            }
                        }
                        // filesが見つからなかったら一番下に書く
                        if (!writeproxies)
                        {
                            sw.WriteLine("data_file 'INTERIOR_PROXY_ORDER_FILE' 'interiorproxies.meta'");
                            sw.WriteLine("files {");
                            sw.WriteLine("\t'interiorproxies.meta',");
                            sw.WriteLine("}");
                            writeproxies = true;
                        }
                    }
                }
            }
        }
    }
}
