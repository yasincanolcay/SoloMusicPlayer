using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoloMusicPlayer
{
    //24,30,54
    //46;51;73
    public partial class Form1 : Form
    {
        //----FORM BORDER RADIUS DESIGN---//
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int NLeftRect
          , int NRightRect, int NTopRect, int NBottomRect, int NWidthEllipse, int NHeightEllipse);

        //----------------------------------------//
        //FORM EKRANDA SURUKLEMEK ICIN INT KONUM DEGISKENLERI VE BOOL
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private string sqlDosyaYolu = "";
        bool isMaximized = false;
        Rectangle normalBounds;
        DatabaseHelper db = DatabaseHelper.Instance;
        bool isStart = false;
        bool isFavorite = false;
        int pageIndex = 0;
        public Form1()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));//form border yuvarla
            System.Windows.Forms.ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MusicsScreen musics = new MusicsScreen();
            musics.Dock = DockStyle.Fill;
            musics.TopLevel = false;
            musics.mediaPlayer = axWindowsMediaPlayer1;
            musics.favoritePictureBox = pictureBox14;
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
            if (!isMaximized)
            {
                normalBounds = this.Bounds;

                this.Bounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.Region = null; // Yuvarlaklığı kaldır
                isMaximized = true;
            }
            else
            {
                this.Bounds = normalBounds;

                // Yuvarlak formu geri yükle
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 25, 25));
                isMaximized = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (pageIndex != 0)
            {
                pageIndex = 0;
                MusicsScreen musics = new MusicsScreen();
                musics.Dock = DockStyle.Fill;
                musics.TopLevel = false;
                musics.mediaPlayer = axWindowsMediaPlayer1;
                musics.favoritePictureBox = pictureBox14;
                mainPanel.Controls.Clear();
                mainPanel.Controls.Add(musics);
                musics.Show();
                button1.BackColor = Color.FromArgb(46, 51, 73);
                button2.BackColor = Color.Transparent;
                button3.BackColor = Color.Transparent;
                button4.BackColor = Color.Transparent;
                button5.BackColor = Color.Transparent;
                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
                {
                    //sayfa değiştiğinde müzik kartlarındaki ikon güncel kalması için
                    //eğer müzik oynuyorsa,2 defa hızlıca tıklayıp, tetikliyoruz
                    pictureBox10_Click(pictureBox1, EventArgs.Empty);
                    pictureBox10_Click(pictureBox1, EventArgs.Empty);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pageIndex != 1)
            {
                pageIndex = 1;
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pageIndex != 2)
            {
                pageIndex = 2;
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
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pageIndex != 3)
            {
                pageIndex=3;
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pageIndex != 4)
            {
                pageIndex = 4;
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
                List<string> favorites = db.GetFavoriteSongs();
                if (favorites.Contains(axWindowsMediaPlayer1.URL))
                {
                    isFavorite = true;
                    pictureBox14.Image = Image.FromFile(@"icons/icons8-favorite-96 (1).png");

                }
                else
                {
                    isFavorite = false;
                    pictureBox14.Image = Image.FromFile(@"icons/icons8-favorite-96.png");

                }
            }
        }

        private void axWindowsMediaPlayer1_StatusChange(object sender, EventArgs e)
        {
            List<string> favorites = db.GetFavoriteSongs();
            if (favorites.Contains(axWindowsMediaPlayer1.URL))
            {
                isFavorite = true;
                pictureBox14.Image = Image.FromFile(@"icons/icons8-favorite-96 (1).png");

            }
            else
            {
                isFavorite = false;
                pictureBox14.Image = Image.FromFile(@"icons/icons8-favorite-96.png");

            }

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.URL.Trim() != "")
            {

                List<string> favorites = db.GetFavoriteSongs();
                if (!isFavorite)
                {
                    db.AddOrRemoveSongs(axWindowsMediaPlayer1.URL, false);
                    isFavorite = true;
                    pictureBox14.Image = Image.FromFile(@"icons/icons8-favorite-96 (1).png");

                }
                else
                {
                    db.AddOrRemoveSongs(axWindowsMediaPlayer1.URL, true);
                    isFavorite = false;
                    pictureBox14.Image = Image.FromFile(@"icons/icons8-favorite-96.png");

                }

            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            axWindowsMediaPlayer1.Dispose();
            pictureBox1.Dispose();
            pictureBox2.Dispose();
            pictureBox3.Dispose();
            pictureBox4.Dispose();
            pictureBox5.Dispose();
            pictureBox6.Dispose();
            pictureBox7.Dispose();
            pictureBox8.Dispose();
            pictureBox9.Dispose();
            pictureBox10.Dispose();
            pictureBox11.Dispose();
            pictureBox12.Dispose();
            pictureBox13.Dispose();
            pictureBox14.Dispose();
            pictureBox15.Dispose();
            pictureBox16.Dispose();
            button1.Dispose();
            button2.Dispose();
            button3.Dispose();
            button4.Dispose();
            button5.Dispose();
            panel1.Dispose();
            panel2.Dispose();
            panel3.Dispose();
            panel5.Dispose();
            panel7.Dispose();
            mainPanel.Dispose();
            label1.Dispose();
            this.Dispose();
            Debug.WriteLine("Ana Form Kapatıldı");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
