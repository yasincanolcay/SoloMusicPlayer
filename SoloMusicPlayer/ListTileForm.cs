using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoloMusicPlayer
{
    public partial class ListTileForm : Form
    {
        public string musicPath = "";
        public AxWindowsMediaPlayer mediaPlayer;
        bool isStart = false;

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
                    mediaPlayer.URL = musicPath;
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
                }
                else
                {
                    pictureBox3.Image = Image.FromFile(@"icons/icons8-play-96.png");
                }
            }
        }
    }
}
