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

namespace SoloMusicPlayer
{
    public partial class SelectPath : Form
    {

        public MusicsScreen musicScreen;
        DatabaseHelper db = DatabaseHelper.Instance;

        public SelectPath()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderBrowser.SelectedPath;
                    //veritabanına path kaydet
                    bool response = db.AddPath(selectedPath);
                    //istediğimiz press fonksiyonunu tetikle
                    musicScreen.refreshMusic();
                }
            }

        }
    }
}
