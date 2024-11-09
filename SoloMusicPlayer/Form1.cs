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
    //24,30,54
    //46;51;73
    public partial class Form1 : Form
    {
        bool isStart = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MusicsScreen musics = new MusicsScreen();
            musics.Dock = DockStyle.Fill;
            musics.TopLevel = false;
            musics.mediaPlayer = axWindowsMediaPlayer1;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(musics);
            musics.Show();
            isStart = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MusicsScreen musics = new MusicsScreen();
            musics.Dock = DockStyle.Fill;
            musics.TopLevel = false;
            musics.mediaPlayer = axWindowsMediaPlayer1;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(musics);
            musics.Show();
            button1.BackColor = Color.FromArgb(46, 51, 73);
            button2.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;
            button4.BackColor = Color.Transparent;
            button5.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlaylistScreen playLists = new PlaylistScreen();
            playLists.Dock = DockStyle.Fill;
            playLists.TopLevel = false;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(playLists);
            playLists.Show();
            button2.BackColor = Color.FromArgb(46, 51, 73);
            button1.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;
            button4.BackColor = Color.Transparent;
            button5.BackColor = Color.Transparent;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HistoryScreen historyScreen = new HistoryScreen();
            historyScreen.Dock = DockStyle.Fill;
            historyScreen.TopLevel = false;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(historyScreen);
            historyScreen.Show();
            button3.BackColor = Color.FromArgb(46, 51, 73);
            button1.BackColor = Color.Transparent;
            button2.BackColor = Color.Transparent;
            button4.BackColor = Color.Transparent;
            button5.BackColor = Color.Transparent;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FavoritesScreen favoritesScreen = new FavoritesScreen();
            favoritesScreen.Dock = DockStyle.Fill;
            favoritesScreen.TopLevel = false;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(favoritesScreen);
            favoritesScreen.Show();
            button5.BackColor = Color.FromArgb(46, 51, 73);
            button1.BackColor = Color.Transparent;
            button2.BackColor = Color.Transparent;
            button4.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SettingsScreen settingsScreen = new SettingsScreen();
            settingsScreen.Dock = DockStyle.Fill;
            settingsScreen.TopLevel = false;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(settingsScreen);
            settingsScreen.Show();
            button4.BackColor = Color.FromArgb(46, 51, 73);
            button1.BackColor = Color.Transparent;
            button2.BackColor = Color.Transparent;
            button5.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (isStart)
            {
                if (pictureBox10.Image != null)
                {
                    pictureBox10.Image.Dispose();
                }
                if (e.newState == 3)
                {
                    pictureBox10.Image = Image.FromFile(@"icons/icons8-pause-96.png");
                }
                else
                {
                    pictureBox10.Image = Image.FromFile(@"icons/icons8-play-96.png");

                }
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if(axWindowsMediaPlayer1.URL != "")
            {
                if (axWindowsMediaPlayer1.playState != WMPLib.WMPPlayState.wmppsPlaying)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
                else
                {
                    axWindowsMediaPlayer1.Ctlcontrols.pause();
                }
            }
        }

        private void axWindowsMediaPlayer1_StatusChange(object sender, EventArgs e)
        {

        }
    }
}
