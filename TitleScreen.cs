using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EssenceOfMagic
{
    public partial class TitleScreen : Form
    {
        public TitleScreen()
        {
            InitializeComponent();
        }

        TextFormatFlags tff = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
        private void Btns_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(((Button)sender).BackColor);
            e.Graphics.DrawImage(Image.FromFile(GameData.TextureFolder + "\\btn.png"), 0, 0, ((Button)sender).Width, ((Button)sender).Height);
            TextRenderer.DrawText(e.Graphics, ((Button)sender).Text, ((Button)sender).Font, new Rectangle(0, 0, ((Button)sender).Width, ((Button)sender).Height), ((Button)sender).ForeColor, tff);
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
    }
}
