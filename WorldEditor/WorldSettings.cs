using System;
using System.Drawing;
using System.Windows.Forms;

namespace WorldEditor
{
    public partial class WorldSettings : Form
    {
        public WorldSettings()
        {
            InitializeComponent();

            WorldWidth.Value = WorldSize.Width;
            WorldHeight.Value = WorldSize.Height;
        }

        public static Size WorldSize = new Size(0, 0);

        new private static Point Anchor = new Point(1, 1);
        private void DrawAnchorPanel()
        {
            Graphics e = panel1.CreateGraphics();
            e.Clear(panel1.BackColor);
            e.DrawRectangles(Pens.Black, new Rectangle[]
            {
                new Rectangle(0, 0, 16, 16), new Rectangle(16, 0, 16, 16), new Rectangle(32, 0, 16, 16),
                new Rectangle(0, 16, 16, 16), new Rectangle(16, 16, 16, 16), new Rectangle(32, 16, 16, 16),
                new Rectangle(0, 32, 16, 16), new Rectangle(16, 32, 16, 16), new Rectangle(32, 32, 16, 16)
            });
            e.FillEllipse(Brushes.Black, Anchor.X * 16 + 4, Anchor.Y * 16 + 4, 8, 8);
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawAnchorPanel();
        }

        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Anchor = new Point(e.X / 16, e.Y / 16);
            DrawAnchorPanel();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            WorldEditor.ResizeWorld(new Size(Convert.ToInt32(WorldWidth.Value), Convert.ToInt32(WorldHeight.Value)), Anchor);
            DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
