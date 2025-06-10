using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace SoloMusicPlayer
{
    public partial class MusicsScreen : Form
    {
        public AxWindowsMediaPlayer mediaPlayer;
        public PictureBox favoritePictureBox;
        DatabaseHelper db = DatabaseHelper.Instance;
        AxWindowsMediaPlayer infoPlayer = new AxWindowsMediaPlayer();
        List<ListTileForm> musicCards=new List<ListTileForm>();


        public MusicsScreen()
        {
            InitializeComponent();
        }

        private void MusicsScreen_Load(object sender, EventArgs e)
        {

            List<string> paths = db.GetAllPaths();

            if (paths.Count != 0)
            {
                //müzikleri al
                getAllMusic(paths);
            }
            else
            {
                //selectpath bizden fonksiyon isteyecek
                panel1.Controls.Clear();
                SelectPath selectPathPopup = new SelectPath();
                selectPathPopup.musicScreen = this;
                selectPathPopup.TopLevel = false;
                selectPathPopup.Dock = DockStyle.Fill;
                panel1.Controls.Add(selectPathPopup);
                selectPathPopup.Show();
            }

        }

        public void refreshMusic()
        {
            List<string> paths = db.GetAllPaths();
            getAllMusic(paths);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void getAllMusic(List<string> paths)
        {
            //müzikleri alacagız
            List<string> musicPaths = new List<string>();
            List<string> checkedPaths = new List<string>();

            foreach (string path in paths)
            {
                // Seçilen klasör al
                if (checkedPaths.Contains(path)==false)
                {
                    string[] musicExtensions = { "*.mp3", "*.wav" }; //uzantılar
                    foreach (string extension in musicExtensions)
                    {
                        musicPaths.AddRange(Directory.GetFiles(path, extension, SearchOption.AllDirectories));
                    }
                    checkedPaths.Add(path);
                }
            }

            if (musicPaths.Count != 0)
            {
                panel1.Controls.Clear();
                FlowLayoutPanel layoutPanel = new FlowLayoutPanel();
                layoutPanel.Dock = DockStyle.Fill;
                layoutPanel.FlowDirection = FlowDirection.TopDown;
                layoutPanel.VerticalScroll.Enabled = true;
                layoutPanel.HorizontalScroll.Visible = false;
                layoutPanel.WrapContents = false;

                // FlowLayoutPanel boyutu değiştiğinde içindeki formları yeniden boyutlandır
                layoutPanel.SizeChanged += (s, e) =>
                {
                    foreach (Control control in layoutPanel.Controls)
                    {
                        if (control is ListTileForm tile)
                        {
                            tile.Width = layoutPanel.ClientSize.Width-10; // FlowLayoutPanel'in genişliğine göre ayarla
                        }
                    }
                    layoutPanel.HorizontalScroll.Visible = false;
                };
                mediaPlayer.settings.autoStart = false;
                infoPlayer.CreateControl();
                infoPlayer.settings.autoStart = false;
                foreach (string item in musicPaths)
                {

                    //burada müzik çalar duruyor çünkü url değişiyor
                    //sorun burada
                    List<string> favoriteList = db.GetFavoriteSongs();
                    infoPlayer.URL = item;
                    // Medya bilgilerini çekin
                    IWMPMedia mediaInfo = infoPlayer.Ctlcontrols.currentItem;
                    string songTitle = mediaInfo.getItemInfo("Title");
                    string artist = mediaInfo.getItemInfo("Author");
                    string album = mediaInfo.getItemInfo("Album");

                    ListTileForm tile = new ListTileForm();
                    if (favoriteList.Contains(item))
                    {
                        tile.isFavorite = true;
                    }
                    else
                    {
                        tile.isFavorite = false;
                    }
                    tile.musicPath = item;
                    tile.mediaPlayer = mediaPlayer;
                    tile.label1.Text = infoPlayer.Ctlcontrols.currentItem.name;
                    if (artist != "" && album != "")
                    {
                        tile.label2.Text = artist + " " + album;
                    }
                    else
                    {
                        tile.label2.Text = "Bilinmiyor";

                    }
                    tile.TopLevel = false;
                    tile.favoritePictureBox = favoritePictureBox;
                    tile.Width = layoutPanel.ClientSize.Width; // Başlangıçta FlowLayoutPanel genişliğine göre ayarla
                    musicCards.Add(tile);
                    layoutPanel.Controls.Add(tile);
                    tile.Show();
                }

                panel1.Controls.Add(layoutPanel);
                layoutPanel.PerformLayout();
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MusicsScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void MusicsScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            //herşeyi bellekten at
            infoPlayer.Dispose();
            for (int i = 0; i < musicCards.Count; i++)
            {
                musicCards[i].Dispose();
            }
            panel1.Dispose();
            this.Dispose();
            Debug.WriteLine("Müzik sayfası kapatıldı");
        }
    }
}
