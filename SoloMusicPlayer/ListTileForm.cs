using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace SoloMusicPlayer
{
    public partial class ListTileForm : Form
    {
        public string musicPath = "";
        public AxWindowsMediaPlayer mediaPlayer;
        bool isStart = false;
        public bool isFavorite = false;
        public PictureBox favoritePictureBox;
        DatabaseHelper db = DatabaseHelper.Instance;


        public ListTileForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            mediaPlayer.settings.autoStart = false;

            if (mediaPlayer.URL==musicPath)
            {
                if (mediaPlayer.playState != WMPLib.WMPPlayState.wmppsPlaying)
                {
                    mediaPlayer.Ctlcontrols.play();
                   
                }
                else
                {
                    mediaPlayer.Ctlcontrols.pause();
                }
            }
            else
            {
                mediaPlayer.URL = musicPath;
                mediaPlayer.Ctlcontrols.play();
            }

        }

        private void ListTileForm_Load(object sender, EventArgs e)
        {
            mediaPlayer.PlayStateChange += MediaPlayer_PlayStateChange;
            isStart = true;
            if (isFavorite)
            {
                pictureBox4.Image = Image.FromFile(@"icons/icons8-favorite-96 (1).png");
            }
        }

        private void MediaPlayer_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (isStart)
            {
                if (mediaPlayer.URL==musicPath)
                {
                    if (pictureBox3.Image != null)
                    {
                        pictureBox3.Image.Dispose();
                    }
                    if (e.newState == 3)
                    {
                        pictureBox3.Image = Image.FromFile(@"icons/icons8-pause-96.png");
                    }
                    else
                    {
                        pictureBox3.Image = Image.FromFile(@"icons/icons8-play-96.png");

                    }
                    List<string> favorites = db.GetFavoriteSongs();
                    if (favorites.Contains(mediaPlayer.URL))
                    {
                        isFavorite = true;
                        pictureBox4.Image = Image.FromFile(@"icons/icons8-favorite-96 (1).png");

                    }
                    else
                    {
                        isFavorite = false;
                        pictureBox4.Image = Image.FromFile(@"icons/icons8-favorite-96.png");

                    }
                }
                else
                {
                    pictureBox3.Image = Image.FromFile(@"icons/icons8-play-96.png");
                }
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //favorilere ekle veya çıkar
            if (isFavorite)
            {
                isFavorite = false;
                //çıkar
                db.AddOrRemoveSongs(musicPath,true);
                pictureBox4.Image = Image.FromFile(@"icons/icons8-favorite-96.png");
                if (mediaPlayer.URL == musicPath)
                {
                    favoritePictureBox.Image = Image.FromFile(@"icons/icons8-favorite-96.png");
                }

            }
            else
            {
                isFavorite = true;
                //ekle
                db.AddOrRemoveSongs(musicPath, false);
                pictureBox4.Image = Image.FromFile(@"icons/icons8-favorite-96 (1).png");
                if (mediaPlayer.URL == musicPath)
                {
                    favoritePictureBox.Image = Image.FromFile(@"icons/icons8-favorite-96 (1).png");
                }

            }

        }

        private void ListTileForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            pictureBox1.Dispose();
            pictureBox1.Dispose();
            pictureBox3.Dispose();
            pictureBox4.Dispose();
            panel1.Dispose();
            panel2.Dispose();
            label1.Dispose();
            label2.Dispose();
            Debug.WriteLine("Müzik Kartları Kapatıldı");
            this.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MouseClicker.RightClickAtCursor();
        }
    }
}
