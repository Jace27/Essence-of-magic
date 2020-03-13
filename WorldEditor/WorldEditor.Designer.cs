namespace WorldEditor
{
    partial class WorldEditor
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.Object_GroupBox = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ObjectIndex = new System.Windows.Forms.NumericUpDown();
            this.Brush_GroupBox = new System.Windows.Forms.GroupBox();
            this.Textures_GroupBox = new System.Windows.Forms.GroupBox();
            this.Textures_List = new System.Windows.Forms.ListBox();
            this.Brush_ComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Layers_BtnAdd = new System.Windows.Forms.Button();
            this.Layers_BtnDelete = new System.Windows.Forms.Button();
            this.Layers_BtnMoveDown = new System.Windows.Forms.Button();
            this.Layers_BtnMoveUp = new System.Windows.Forms.Button();
            this.Layers_List = new System.Windows.Forms.ListBox();
            this.HoverBlockCoords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BlockCoords = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выйтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиМираToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.функцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.найтиКоллизииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.Object_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectIndex)).BeginInit();
            this.Brush_GroupBox.SuspendLayout();
            this.Textures_GroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.Object_GroupBox);
            this.panel1.Controls.Add(this.Brush_GroupBox);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.HoverBlockCoords);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.BlockCoords);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(960, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 640);
            this.panel1.TabIndex = 0;
            // 
            // Object_GroupBox
            // 
            this.Object_GroupBox.Controls.Add(this.label5);
            this.Object_GroupBox.Controls.Add(this.label4);
            this.Object_GroupBox.Controls.Add(this.label3);
            this.Object_GroupBox.Controls.Add(this.ObjectIndex);
            this.Object_GroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.Object_GroupBox.Location = new System.Drawing.Point(14, 539);
            this.Object_GroupBox.Name = "Object_GroupBox";
            this.Object_GroupBox.Size = new System.Drawing.Size(294, 74);
            this.Object_GroupBox.TabIndex = 7;
            this.Object_GroupBox.TabStop = false;
            this.Object_GroupBox.Text = "Объект";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(131, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Координаты: -1:-1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Всего: 0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Индекс:";
            // 
            // ObjectIndex
            // 
            this.ObjectIndex.Enabled = false;
            this.ObjectIndex.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.ObjectIndex.Location = new System.Drawing.Point(75, 22);
            this.ObjectIndex.Name = "ObjectIndex";
            this.ObjectIndex.Size = new System.Drawing.Size(206, 23);
            this.ObjectIndex.TabIndex = 0;
            this.ObjectIndex.ValueChanged += new System.EventHandler(this.ObjectIndex_ValueChanged);
            // 
            // Brush_GroupBox
            // 
            this.Brush_GroupBox.Controls.Add(this.Textures_GroupBox);
            this.Brush_GroupBox.Controls.Add(this.Brush_ComboBox);
            this.Brush_GroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.Brush_GroupBox.Location = new System.Drawing.Point(14, 161);
            this.Brush_GroupBox.Name = "Brush_GroupBox";
            this.Brush_GroupBox.Size = new System.Drawing.Size(294, 372);
            this.Brush_GroupBox.TabIndex = 6;
            this.Brush_GroupBox.TabStop = false;
            this.Brush_GroupBox.Text = "Кисть";
            // 
            // Textures_GroupBox
            // 
            this.Textures_GroupBox.Controls.Add(this.Textures_List);
            this.Textures_GroupBox.Location = new System.Drawing.Point(6, 53);
            this.Textures_GroupBox.Name = "Textures_GroupBox";
            this.Textures_GroupBox.Size = new System.Drawing.Size(275, 314);
            this.Textures_GroupBox.TabIndex = 1;
            this.Textures_GroupBox.TabStop = false;
            this.Textures_GroupBox.Text = "Текстура";
            // 
            // Textures_List
            // 
            this.Textures_List.FormattingEnabled = true;
            this.Textures_List.ItemHeight = 17;
            this.Textures_List.Location = new System.Drawing.Point(6, 19);
            this.Textures_List.Name = "Textures_List";
            this.Textures_List.Size = new System.Drawing.Size(263, 276);
            this.Textures_List.TabIndex = 5;
            // 
            // Brush_ComboBox
            // 
            this.Brush_ComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.Brush_ComboBox.FormattingEnabled = true;
            this.Brush_ComboBox.Items.AddRange(new object[] {
            "Выбрать блок",
            "Очистить блок",
            "Рука",
            "Удалить слой",
            "Добавить слой",
            "Выбрать объект",
            "Добавить объект",
            "Установить непроходимость",
            "Удалить непроходимость"});
            this.Brush_ComboBox.Location = new System.Drawing.Point(6, 19);
            this.Brush_ComboBox.Name = "Brush_ComboBox";
            this.Brush_ComboBox.Size = new System.Drawing.Size(275, 28);
            this.Brush_ComboBox.TabIndex = 0;
            this.Brush_ComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            this.Brush_ComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBox1_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Layers_BtnAdd);
            this.groupBox1.Controls.Add(this.Layers_BtnDelete);
            this.groupBox1.Controls.Add(this.Layers_BtnMoveDown);
            this.groupBox1.Controls.Add(this.Layers_BtnMoveUp);
            this.groupBox1.Controls.Add(this.Layers_List);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.groupBox1.Location = new System.Drawing.Point(14, 53);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 102);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Слои:";
            // 
            // Layers_BtnAdd
            // 
            this.Layers_BtnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Layers_BtnAdd.Location = new System.Drawing.Point(249, 22);
            this.Layers_BtnAdd.Name = "Layers_BtnAdd";
            this.Layers_BtnAdd.Size = new System.Drawing.Size(32, 32);
            this.Layers_BtnAdd.TabIndex = 9;
            this.Layers_BtnAdd.Tag = "layers_BtnAdd.png";
            this.Layers_BtnAdd.UseVisualStyleBackColor = true;
            this.Layers_BtnAdd.Click += new System.EventHandler(this.Layers_BtnAdd_Click);
            this.Layers_BtnAdd.Paint += new System.Windows.Forms.PaintEventHandler(this.Layers_Btns_Paint);
            this.Layers_BtnAdd.MouseEnter += new System.EventHandler(this.Layers_Btns_MouseEnter);
            this.Layers_BtnAdd.MouseLeave += new System.EventHandler(this.Layers_Btns_MouseLeave);
            // 
            // Layers_BtnDelete
            // 
            this.Layers_BtnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Layers_BtnDelete.Location = new System.Drawing.Point(249, 60);
            this.Layers_BtnDelete.Name = "Layers_BtnDelete";
            this.Layers_BtnDelete.Size = new System.Drawing.Size(32, 32);
            this.Layers_BtnDelete.TabIndex = 8;
            this.Layers_BtnDelete.Tag = "layers_BtnDelete.png";
            this.Layers_BtnDelete.UseVisualStyleBackColor = true;
            this.Layers_BtnDelete.Click += new System.EventHandler(this.Layers_BtnDelete_Click);
            this.Layers_BtnDelete.Paint += new System.Windows.Forms.PaintEventHandler(this.Layers_Btns_Paint);
            this.Layers_BtnDelete.MouseEnter += new System.EventHandler(this.Layers_Btns_MouseEnter);
            this.Layers_BtnDelete.MouseLeave += new System.EventHandler(this.Layers_Btns_MouseLeave);
            // 
            // Layers_BtnMoveDown
            // 
            this.Layers_BtnMoveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Layers_BtnMoveDown.Location = new System.Drawing.Point(211, 60);
            this.Layers_BtnMoveDown.Name = "Layers_BtnMoveDown";
            this.Layers_BtnMoveDown.Size = new System.Drawing.Size(32, 32);
            this.Layers_BtnMoveDown.TabIndex = 6;
            this.Layers_BtnMoveDown.Tag = "layers_BtnMoveDown.png";
            this.Layers_BtnMoveDown.UseVisualStyleBackColor = true;
            this.Layers_BtnMoveDown.Click += new System.EventHandler(this.Layers_BtnMoveDown_Click);
            this.Layers_BtnMoveDown.Paint += new System.Windows.Forms.PaintEventHandler(this.Layers_Btns_Paint);
            this.Layers_BtnMoveDown.MouseEnter += new System.EventHandler(this.Layers_Btns_MouseEnter);
            this.Layers_BtnMoveDown.MouseLeave += new System.EventHandler(this.Layers_Btns_MouseLeave);
            // 
            // Layers_BtnMoveUp
            // 
            this.Layers_BtnMoveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Layers_BtnMoveUp.Location = new System.Drawing.Point(211, 22);
            this.Layers_BtnMoveUp.Name = "Layers_BtnMoveUp";
            this.Layers_BtnMoveUp.Size = new System.Drawing.Size(32, 32);
            this.Layers_BtnMoveUp.TabIndex = 5;
            this.Layers_BtnMoveUp.Tag = "layers_BtnMoveUp.png";
            this.Layers_BtnMoveUp.UseVisualStyleBackColor = true;
            this.Layers_BtnMoveUp.Click += new System.EventHandler(this.Layers_BtnMoveUp_Click);
            this.Layers_BtnMoveUp.Paint += new System.Windows.Forms.PaintEventHandler(this.Layers_Btns_Paint);
            this.Layers_BtnMoveUp.MouseEnter += new System.EventHandler(this.Layers_Btns_MouseEnter);
            this.Layers_BtnMoveUp.MouseLeave += new System.EventHandler(this.Layers_Btns_MouseLeave);
            // 
            // Layers_List
            // 
            this.Layers_List.FormattingEnabled = true;
            this.Layers_List.ItemHeight = 17;
            this.Layers_List.Location = new System.Drawing.Point(6, 22);
            this.Layers_List.Name = "Layers_List";
            this.Layers_List.Size = new System.Drawing.Size(199, 72);
            this.Layers_List.TabIndex = 4;
            // 
            // HoverBlockCoords
            // 
            this.HoverBlockCoords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.HoverBlockCoords.Location = new System.Drawing.Point(192, 30);
            this.HoverBlockCoords.Name = "HoverBlockCoords";
            this.HoverBlockCoords.Size = new System.Drawing.Size(116, 20);
            this.HoverBlockCoords.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(10, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Мышь наведена на:";
            // 
            // BlockCoords
            // 
            this.BlockCoords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.BlockCoords.Location = new System.Drawing.Point(192, 10);
            this.BlockCoords.Name = "BlockCoords";
            this.BlockCoords.Size = new System.Drawing.Size(116, 20);
            this.BlockCoords.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выбранный блок:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem,
            this.настройкиМираToolStripMenuItem,
            this.функцииToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1280, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.выйтиToolStripMenuItem});
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.менюToolStripMenuItem.Text = "Меню";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.СохранитьToolStripMenuItem_Click);
            // 
            // выйтиToolStripMenuItem
            // 
            this.выйтиToolStripMenuItem.Name = "выйтиToolStripMenuItem";
            this.выйтиToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.выйтиToolStripMenuItem.Text = "Выйти";
            this.выйтиToolStripMenuItem.Click += new System.EventHandler(this.ВыйтиToolStripMenuItem_Click);
            // 
            // настройкиМираToolStripMenuItem
            // 
            this.настройкиМираToolStripMenuItem.Name = "настройкиМираToolStripMenuItem";
            this.настройкиМираToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.настройкиМираToolStripMenuItem.Text = "Настройки мира";
            this.настройкиМираToolStripMenuItem.Click += new System.EventHandler(this.НастройкиМираToolStripMenuItem_Click);
            // 
            // функцииToolStripMenuItem
            // 
            this.функцииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.найтиКоллизииToolStripMenuItem});
            this.функцииToolStripMenuItem.Name = "функцииToolStripMenuItem";
            this.функцииToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.функцииToolStripMenuItem.Text = "Функции";
            // 
            // найтиКоллизииToolStripMenuItem
            // 
            this.найтиКоллизииToolStripMenuItem.Name = "найтиКоллизииToolStripMenuItem";
            this.найтиКоллизииToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.найтиКоллизииToolStripMenuItem.Text = "Найти коллизии";
            this.найтиКоллизииToolStripMenuItem.Click += new System.EventHandler(this.НайтиКоллизииToolStripMenuItem_Click);
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.ЗагрузитьToolStripMenuItem_Click);
            // 
            // WorldEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1280, 664);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "WorldEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "World Editor";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WorldEditor_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WorldEditor_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WorldEditor_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WorldEditor_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WorldEditor_MouseUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.WorldEditor_PreviewKeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Object_GroupBox.ResumeLayout(false);
            this.Object_GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectIndex)).EndInit();
            this.Brush_GroupBox.ResumeLayout(false);
            this.Textures_GroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выйтиToolStripMenuItem;
        private System.Windows.Forms.Label BlockCoords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label HoverBlockCoords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox Layers_List;
        private System.Windows.Forms.Button Layers_BtnDelete;
        private System.Windows.Forms.Button Layers_BtnMoveDown;
        private System.Windows.Forms.Button Layers_BtnMoveUp;
        private System.Windows.Forms.Button Layers_BtnAdd;
        private System.Windows.Forms.GroupBox Brush_GroupBox;
        private System.Windows.Forms.ComboBox Brush_ComboBox;
        private System.Windows.Forms.GroupBox Textures_GroupBox;
        private System.Windows.Forms.ListBox Textures_List;
        private System.Windows.Forms.ToolStripMenuItem настройкиМираToolStripMenuItem;
        private System.Windows.Forms.GroupBox Object_GroupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown ObjectIndex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem функцииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem найтиКоллизииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
    }
}

