namespace WorldEditor
{
    partial class WorldSettings
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
            this.WorldSizeGroupBox = new System.Windows.Forms.GroupBox();
            this.WorldWidth = new System.Windows.Forms.NumericUpDown();
            this.WorldHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.WorldSizeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WorldWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorldHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // WorldSizeGroupBox
            // 
            this.WorldSizeGroupBox.Controls.Add(this.panel1);
            this.WorldSizeGroupBox.Controls.Add(this.label2);
            this.WorldSizeGroupBox.Controls.Add(this.label1);
            this.WorldSizeGroupBox.Controls.Add(this.WorldHeight);
            this.WorldSizeGroupBox.Controls.Add(this.WorldWidth);
            this.WorldSizeGroupBox.Location = new System.Drawing.Point(12, 12);
            this.WorldSizeGroupBox.Name = "WorldSizeGroupBox";
            this.WorldSizeGroupBox.Size = new System.Drawing.Size(245, 81);
            this.WorldSizeGroupBox.TabIndex = 0;
            this.WorldSizeGroupBox.TabStop = false;
            this.WorldSizeGroupBox.Text = "Размер мира";
            // 
            // WorldWidth
            // 
            this.WorldWidth.Location = new System.Drawing.Point(57, 19);
            this.WorldWidth.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.WorldWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.WorldWidth.Name = "WorldWidth";
            this.WorldWidth.Size = new System.Drawing.Size(120, 20);
            this.WorldWidth.TabIndex = 0;
            this.WorldWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // WorldHeight
            // 
            this.WorldHeight.Location = new System.Drawing.Point(57, 45);
            this.WorldHeight.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.WorldHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.WorldHeight.Name = "WorldHeight";
            this.WorldHeight.Size = new System.Drawing.Size(120, 20);
            this.WorldHeight.TabIndex = 1;
            this.WorldHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ширина";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Высота";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(183, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(49, 49);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseClick);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(12, 99);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(119, 23);
            this.SaveBtn.TabIndex = 1;
            this.SaveBtn.Text = "Сохранить";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(138, 99);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(119, 23);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "Отменить";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // WorldSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(273, 135);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.WorldSizeGroupBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorldSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки мира";
            this.WorldSizeGroupBox.ResumeLayout(false);
            this.WorldSizeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WorldWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WorldHeight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox WorldSizeGroupBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown WorldHeight;
        private System.Windows.Forms.NumericUpDown WorldWidth;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button CancelBtn;
    }
}