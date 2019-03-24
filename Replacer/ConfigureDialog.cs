using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Replacer
{

    public partial class ConfigureDialog : Form
    {


        public ConfigureDialog()
        {
            InitializeComponent();
        }


        public bool Edit(ref AppConfig appCfg)
        {
            using (var appCfgLocal = new AppConfig())
            {
                foreach (var itm in appCfg.Items)
                {
                    appCfgLocal.Items.Add(itm);
                }
                appCfgLocal.SourcePlayListPath = appCfg.SourcePlayListPath;
                appCfgLocal.DestPlayListPath = appCfg.DestPlayListPath;

                this.propertyGrid1.SelectedObject = appCfgLocal;
                if (DialogResult.OK == this.ShowDialog())
                {
                    appCfg.Items.Clear();

                    foreach (var itm in appCfgLocal.Items)
                    {
                        if (string.IsNullOrWhiteSpace(itm.From)) itm.From = "\\";
                        if (string.IsNullOrWhiteSpace(itm.To)) itm.To = "\\";
                        appCfg.Items.Add(itm);
                    }
                    appCfg.SourcePlayListPath = string.IsNullOrWhiteSpace(appCfgLocal.SourcePlayListPath) ? "\\" : appCfgLocal.SourcePlayListPath;
                    appCfg.DestPlayListPath = string.IsNullOrWhiteSpace(appCfgLocal.DestPlayListPath) ? "\\" : appCfgLocal.DestPlayListPath;

                    return true;
                }
                return false;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
