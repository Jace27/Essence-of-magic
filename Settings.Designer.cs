namespace EssenceOfMagic
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.ShowInfo_CheckBox = new System.Windows.Forms.CheckBox();
            this.ShowHitbox_CheckBox = new System.Windows.Forms.CheckBox();
            this.ShowInterface_CheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MusicVolume_TrackBar = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MusicVolume_TrackBar)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ShowInfo_CheckBox
            // 
            this.ShowInfo_CheckBox.AutoSize = true;
            this.ShowInfo_CheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(235)))), ((int)(((byte)(215)))));
            this.ShowInfo_CheckBox.Location = new System.Drawing.Point(527, 367);
            this.ShowInfo_CheckBox.Name = "ShowInfo_CheckBox";
            this.ShowInfo_CheckBox.Size = new System.Drawing.Size(264, 54);
            this.ShowInfo_CheckBox.TabIndex = 0;
            this.ShowInfo_CheckBox.Text = "отображение технической\r\nинформации";
            this.ShowInfo_CheckBox.UseVisualStyleBackColor = false;
            this.ShowInfo_CheckBox.CheckedChanged += new System.EventHandler(this.ShowInfo_CheckBox_CheckedChanged);
            // 
            // ShowHitbox_CheckBox
            // 
            this.ShowHitbox_CheckBox.AutoSize = true;
            this.ShowHitbox_CheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(235)))), ((int)(((byte)(215)))));
            this.ShowHitbox_CheckBox.Location = new System.Drawing.Point(527, 427);
            this.ShowHitbox_CheckBox.Name = "ShowHitbox_CheckBox";
            this.ShowHitbox_CheckBox.Size = new System.Drawing.Size(243, 29);
            this.ShowHitbox_CheckBox.TabIndex = 1;
            this.ShowHitbox_CheckBox.Text = "отображение хитбоксов";
            this.ShowHitbox_CheckBox.UseVisualStyleBackColor = false;
            this.ShowHitbox_CheckBox.CheckedChanged += new System.EventHandler(this.ShowHitbox_CheckBox_CheckedChanged);
            // 
            // ShowInterface_CheckBox
            // 
            this.ShowInterface_CheckBox.AutoSize = true;
            this.ShowInterface_CheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(235)))), ((int)(((byte)(215)))));
            this.ShowInterface_CheckBox.Location = new System.Drawing.Point(527, 332);
            this.ShowInterface_CheckBox.Name = "ShowInterface_CheckBox";
            this.ShowInterface_CheckBox.Size = new System.Drawing.Size(258, 29);
            this.ShowInterface_CheckBox.TabIndex = 2;
            this.ShowInterface_CheckBox.Text = "отображение интерфейса";
            this.ShowInterface_CheckBox.UseVisualStyleBackColor = false;
            this.ShowInterface_CheckBox.CheckedChanged += new System.EventHandler(this.ShowInterface_CheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(522, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Громкость музыки:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(522, 304);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Техническое:";
            // 
            // MusicVolume_TrackBar
            // 
            this.MusicVolume_TrackBar.LargeChange = 1;
            this.MusicVolume_TrackBar.Location = new System.Drawing.Point(527, 98);
            this.MusicVolume_TrackBar.Maximum = 100;
            this.MusicVolume_TrackBar.Name = "MusicVolume_TrackBar";
            this.MusicVolume_TrackBar.Size = new System.Drawing.Size(232, 45);
            this.MusicVolume_TrackBar.TabIndex = 5;
            this.MusicVolume_TrackBar.Value = 100;
            this.MusicVolume_TrackBar.ValueChanged += new System.EventHandler(this.MusicVolume_TrackBar_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(198, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(68, 35);
            this.panel1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Назад";
            this.label3.Click += new System.EventHandler(this.Label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(184, 315);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Управление:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(184, 340);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(292, 125);
            this.label5.TabIndex = 8;
            this.label5.Text = "WASD - движение\r\nEsc - выход в меню или из окон \r\nинтерфейса\r\nE - открыть или зак" +
    "рыть \r\nинвентарь";
            // 
            // Settings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(235)))), ((int)(((byte)(215)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(960, 640);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MusicVolume_TrackBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ShowInterface_CheckBox);
            this.Controls.Add(this.ShowHitbox_CheckBox);
            this.Controls.Add(this.ShowInfo_CheckBox);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Settings_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Settings_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Settings_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Settings_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.MusicVolume_TrackBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ShowInfo_CheckBox;
        private System.Windows.Forms.CheckBox ShowHitbox_CheckBox;
        private System.Windows.Forms.CheckBox ShowInterface_CheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar MusicVolume_TrackBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}