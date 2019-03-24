using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Replacer
{
    struct DestFileInfo
    {
        public string FileName { get; set; }
        public bool FileExist { get; set; }

    };


    public partial class Form1 : Form
    {
        private AppConfig Config;
        private string FileName { get; set; }
        private string[] SourceContent { get; set; }
        

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            listBox1.Visible = false;
            listBox1.Items.Clear();
            if (!string.IsNullOrWhiteSpace(Config.SourcePlayListPath))
            {
                openDialog.InitialDirectory = Config.SourcePlayListPath;
            }
            if (DialogResult.OK == openDialog.ShowDialog())
            {
                if (string.IsNullOrWhiteSpace(openDialog.FileName))
                {
                    string message = "File name can not be empty";
                    string title = "Error";
                    MessageBox.Show(message, title);
                    return;
                }

                FileName = openDialog.FileName;
                var fileStream = openDialog.OpenFile();
                string fileContents;
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContents = reader.ReadToEnd();
                    SourceContent = fileContents.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                    for (int i = 0; i < SourceContent.Length; i++)
                    {

                        
                       
                        if (string.IsNullOrWhiteSpace(SourceContent[i])) continue;
                        if (SourceContent[i].StartsWith("#")) continue;

                        var lineItems = SourceContent[i].Split(';');

                        if (lineItems.Count() < 2) continue;
                        var srcFileName = lineItems[0].Trim();
                        if ((srcFileName.First()=='\"') && (srcFileName.Last() == '\"'))
                        {
                            srcFileName = srcFileName.Substring(1, srcFileName.Length - 2);
                        }
                        if (string.IsNullOrWhiteSpace(srcFileName)) continue;

                        var lowerFn = srcFileName.ToLowerInvariant();

                        bool bReplaced = false;
                        foreach(var ci in Config.Items)
                        {
                            
                            var From = ci.From;
                            if (From.Last() != '\\') From += "\\";

                            var To =  ci.To;
                            if (To.Last() != '\\') To += "\\";

                            if (lowerFn.StartsWith(From.ToLowerInvariant()))
                            {
                                var oldValue = srcFileName.Substring(0, ci.From.Length);
                                var newValue = ci.To;
                                SourceContent[i].Replace(oldValue, newValue);
                                bReplaced = true;
                                break;
                            }
                        }

                        if (!bReplaced)
                        {
                            listBox1.Items.Add(srcFileName);
                            listBox1.Visible = true;
                            
                        }


                    }
                }


            }
        }

        private void Configure()
        {
            using (var cfgDialog = new ConfigureDialog())
            {
                var res = cfgDialog.Edit(ref Config);
                if (res)
                {
                    string output = JsonConvert.SerializeObject(Config);
                    var path = AppDomain.CurrentDomain.BaseDirectory;
                    path += "\\config.json";

                    if (File.Exists(path))
                    {
                        if (File.Exists(path + ".bak"))
                        {
                            File.Delete(path + ".bak");
                        }

                        File.Move(path, path + ".bak");
                    }
                    using (FileStream fs = File.Create(path))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(output);
                        fs.Write(info, 0, info.Length);
                    }



                }
            }
        }

        private void configureBtn_Click(object sender, EventArgs e)
        {

            Configure();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Config = new AppConfig();
            //Load here

            var path = AppDomain.CurrentDomain.BaseDirectory;
            path += "\\config.json";

            if (File.Exists(path))
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        var fileContents = reader.ReadToEnd();
                        Config = JsonConvert.DeserializeObject<AppConfig>(fileContents);
                    }

                }
            }
            else
            {
                Configure();
            }

        }
    }
}
