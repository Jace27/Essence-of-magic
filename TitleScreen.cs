using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;

namespace EssenceOfMagic
{
    public partial class TitleScreen : Form
    {
        public TitleScreen()
        {
            InitializeComponent();
            GameData.TitleScreen = this;
            Settings.Read();

            afr = new AudioFileReader(GameData.SoundFolder + "\\World of Warcraft Soundtrack Tavern (Human).mp3");
            woe = new WaveOutEvent();
            woe.Init(afr);
            woe.PlaybackStopped += Woe_PlaybackStopped;
            woe.Play();
        }

        private void Woe_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            woe.Dispose();
            woe = new WaveOutEvent();
            afr.CurrentTime = new TimeSpan(0, 0, 0);
            woe.Init(afr);
            woe.Play();
        }

        public static AudioFileReader afr;
        public static WaveOutEvent woe;

        TextFormatFlags tff = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
        private void Btns_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(((Button)sender).BackColor);
            Image img = Image.FromFile(GameData.TextureFolder + "\\btn.png");
            e.Graphics.DrawImage(img, 0, 0, ((Button)sender).Width, ((Button)sender).Height);
            TextRenderer.DrawText(e.Graphics, ((Button)sender).Text, ((Button)sender).Font, new Rectangle(0, 0, ((Button)sender).Width, ((Button)sender).Height), ((Button)sender).ForeColor, tff);
            img.Dispose();
        }

        private void Btns_MouseEnter(object sender, EventArgs e)
        {
            Color old = ((Button)sender).ForeColor;
            ((Button)sender).ForeColor = Color.FromArgb(255 - old.R, 255 - old.G, 255 - old.B);
        }

        private void Btns_MouseLeave(object sender, EventArgs e)
        {
            Color old = ((Button)sender).ForeColor;
            ((Button)sender).ForeColor = Color.FromArgb(255 - old.R, 255 - old.G, 255 - old.B);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void NewGameBtn_Click(object sender, EventArgs e)
        {
            Game Game = new Game();
            Game.Owner = this;
            Game.Show();
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            Settings Settings = new Settings(this);
            Settings.ShowDialog();
        }

        private void LoadGameBtn_Click(object sender, EventArgs e)
        {
            GameData.Load();
        }
    }
}
