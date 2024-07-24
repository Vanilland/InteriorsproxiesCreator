using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace InteriorsproxiesCreator
{
    public partial class Forms : Form
    {
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
            string directory = resourcefolder.Text;
            if (Directory.Exists(directory))
            {
                string interiorproxiesFile = Path.Combine(Directory.GetCurrentDirectory(), "interiorproxies.meta");
                using (StreamWriter sw = new StreamWriter(interiorproxiesFile, false))
                {
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    sw.WriteLine("<SInteriorOrderData>");
                    Random random = new Random();
                    sw.WriteLine("\t<startFrom value=\"" + random.Next(2000, 3000) + "\" />");
                    sw.WriteLine("\t<proxies>");
                    SearchFxmanifest(sw, directory);
                    sw.WriteLine("\t</proxies>");
                    sw.WriteLine("</SInteriorOrderData>");
                }
            }
            openDirDialog.Enabled = true;
            doStartButton.Enabled = true;
        }

        /// <summary>
        /// マニフェストがあるフォルダを探す
        /// </summary>
        /// <param name="dir"></param>
        private void SearchFxmanifest(StreamWriter sw, string dir)
        {
            string manifest = Path.Combine(dir, "fxmanifest.lua");
            if (File.Exists(manifest))
            {
                CreateInteriorproxies(sw, dir);
            }
            else
            {
                string[] subFolders = Directory.GetDirectories(dir, "*", System.IO.SearchOption.TopDirectoryOnly);
                foreach (string subFolder in subFolders)
                {
                    SearchFxmanifest(sw, subFolder);
                }
            }
            Application.DoEvents();
        }

        /// <summary>
        /// .ymapファイルがある場所をさがしてinteriorproxies.metaに追記する
        /// fxmanifestのfilesに追加する
        /// </summary>
        /// <param name="dir"></param>
        private void CreateInteriorproxies(StreamWriter sw, string dir)
        {
            bool findmilo = false;
            string[] files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
            List<string> miloList = new List<string>();
            foreach (string file in files)
            {
                if (file.EndsWith(".ymap"))
                {
                    miloList.Add(Path.GetFileNameWithoutExtension(file));
                    findmilo = true;
                }
            }

            if(findmilo)
            {
                sw.WriteLine("\t\t<!-- " + Path.GetFileName(dir) + " -->");
                foreach (string milo in miloList)
                {
                    sw.WriteLine("\t\t<Item>" + milo + "</Item>");
                }
            }
        }

        /// <summary>
        /// リソースフォルダ、ドラッグドロップでフォルダ名を入力できるように。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resourcefolder_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] dragFilePathArr = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            resourcefolder.Text = dragFilePathArr[0];
        }

        private void resourcefolder_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
    }
}
