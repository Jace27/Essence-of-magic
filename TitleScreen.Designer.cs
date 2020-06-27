namespace EssenceOfMagic
{
    partial class TitleScreen
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TitleScreen));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NewGameBtn = new System.Windows.Forms.Button();
            this.LoadGameBtn = new System.Windows.Forms.Button();
            this.SettingsBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(235)))), ((int)(((byte)(215)))));
            this.label1.Font = new System.Drawing.Font("Segoe Script", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(530, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 88);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ролевая игра\r\nEssence of Magic";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(235)))), ((int)(((byte)(215)))));
            this.label2.Font = new System.Drawing.Font("Segoe Script", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(202, 375);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 88);
            this.label2.TabIndex = 1;
            this.label2.Text = "Выполнил: студент группы ПР-301 Шилов Д.А.\r\nПроверил: преподаватель Воропанова И." +
    "О.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NewGameBtn
            // 
            this.NewGameBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(235)))), ((int)(((byte)(215)))));
            this.NewGameBtn.Location = new System.Drawing.Point(558, 213);
            this.NewGameBtn.Name = "NewGameBtn";
            this.NewGameBtn.Size = new System.Drawing.Size(177, 39);
            this.NewGameBtn.TabIndex = 2;
            this.NewGameBtn.Text = "Новая игра";
            this.NewGameBtn.UseVisualStyleBackColor = false;
            this.NewGameBtn.Click += new System.EventHandler(this.NewGameBtn_Click);
            this.NewGameBtn.Paint += new System.Windows.Forms.PaintEventHandler(this.Btns_Paint);
            this.NewGameBtn.MouseEnter += new System.EventHandler(this.Btns_MouseEnter);
            this.NewGameBtn.MouseLeave += new System.EventHandler(this.Btns_MouseLeave);
            // 
            // LoadGameBtn
            // 
            this.LoadGameBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(235)))), ((int)(((byte)(215)))));
            this.LoadGameBtn.Location = new System.Drawing.Point(558, 258);
            this.LoadGameBtn.Name = "LoadGameBtn";
            this.LoadGameBtn.Size = new System.Drawing.Size(177, 39);
            this.LoadGameBtn.TabIndex = 3;
            this.LoadGameBtn.Text = "Загрузить игру";
            this.LoadGameBtn.UseVisualStyleBackColor = false;
            this.LoadGameBtn.Click += new System.EventHandler(this.LoadGameBtn_Click);
            this.LoadGameBtn.Paint += new System.Windows.Forms.PaintEventHandler(this.Btns_Paint);
            this.LoadGameBtn.MouseEnter += new System.EventHandler(this.Btns_MouseEnter);
            this.LoadGameBtn.MouseLeave += new System.EventHandler(this.Btns_MouseLeave);
            // 
            // SettingsBtn
            // 
            this.SettingsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(235)))), ((int)(((byte)(215)))));
            this.SettingsBtn.Location = new System.Drawing.Point(558, 303);
            this.SettingsBtn.Name = "SettingsBtn";
            this.SettingsBtn.Size = new System.Drawing.Size(177, 39);
            this.SettingsBtn.TabIndex = 4;
            this.SettingsBtn.Text = "Настройки";
            this.SettingsBtn.UseVisualStyleBackColor = false;
            this.SettingsBtn.Click += new System.EventHandler(this.SettingsBtn_Click);
            this.SettingsBtn.Paint += new System.Windows.Forms.PaintEventHandler(this.Btns_Paint);
            this.SettingsBtn.MouseEnter += new System.EventHandler(this.Btns_MouseEnter);
            this.SettingsBtn.MouseLeave += new System.EventHandler(this.Btns_MouseLeave);
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(235)))), ((int)(((byte)(215)))));
            this.ExitBtn.Location = new System.Drawing.Point(558, 348);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(177, 39);
            this.ExitBtn.TabIndex = 5;
            this.ExitBtn.Text = "Выйти из игры";
            this.ExitBtn.UseVisualStyleBackColor = false;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            this.ExitBtn.Paint += new System.Windows.Forms.PaintEventHandler(this.Btns_Paint);
            this.ExitBtn.MouseEnter += new System.EventHandler(this.Btns_MouseEnter);
            this.ExitBtn.MouseLeave += new System.EventHandler(this.Btns_MouseLeave);
            // 
            // TitleScreen
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(960, 640);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.SettingsBtn);
            this.Controls.Add(this.LoadGameBtn);
            this.Controls.Add(this.NewGameBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe Script", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "TitleScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TitleScreen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button NewGameBtn;
        private System.Windows.Forms.Button LoadGameBtn;
        private System.Windows.Forms.Button SettingsBtn;
        private System.Windows.Forms.Button ExitBtn;
    }
}