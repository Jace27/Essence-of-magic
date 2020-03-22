namespace EssenceOfMagic
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            try
            {
                base.Dispose(disposing);
            }
            catch { }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.graphicSurface1 = new EssenceOfMagic.GraphicSurface();
            this.SuspendLayout();
            // 
            // graphicSurface1
            // 
            this.graphicSurface1.BackColor = System.Drawing.Color.Black;
            this.graphicSurface1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphicSurface1.FPSlimit = 2147483647;
            this.graphicSurface1.Location = new System.Drawing.Point(0, 0);
            this.graphicSurface1.Name = "graphicSurface1";
            this.graphicSurface1.Size = new System.Drawing.Size(960, 640);
            this.graphicSurface1.TabIndex = 0;
            this.graphicSurface1.VSync = false;
            this.graphicSurface1.OnDraw += new EssenceOfMagic.GraphicSurface.DrawEventHandler(this.GraphicSurface1_OnDraw);
            this.graphicSurface1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Game_MouseMove);
            this.graphicSurface1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GraphicSurface1_MouseClick);
            // 
            // Game
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(960, 640);
            this.Controls.Add(this.graphicSurface1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Game_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Game_KeyUp);
            this.ResumeLayout(false);
        }

        #endregion

        private GraphicSurface graphicSurface1;
    }
}