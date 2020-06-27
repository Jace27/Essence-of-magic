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
            this.CurrentObjectData_GroupBox = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MaxWater = new System.Windows.Forms.NumericUpDown();
            this.MaxSatiety = new System.Windows.Forms.NumericUpDown();
            this.MaxHP = new System.Windows.Forms.NumericUpDown();
            this.CurrentObjectHitbox_GroupBox = new System.Windows.Forms.GroupBox();
            this.CurrentObjectHitboxSize_GroupBox = new System.Windows.Forms.GroupBox();
            this.CurObjHitboxSizeHeight = new System.Windows.Forms.NumericUpDown();
            this.CurObjHitboxSizeWidth = new System.Windows.Forms.NumericUpDown();
            this.CurrentObjectHitboxCoords_GroupBox = new System.Windows.Forms.GroupBox();
            this.CurObjHitboxCoordY = new System.Windows.Forms.NumericUpDown();
            this.CurObjHitboxCoordX = new System.Windows.Forms.NumericUpDown();
            this.CurrentObjectSize_GroupBox = new System.Windows.Forms.GroupBox();
            this.CurObjSizeHeight = new System.Windows.Forms.NumericUpDown();
            this.CurObjSizeWidth = new System.Windows.Forms.NumericUpDown();
            this.CurrentObjectCoords_GroupBox = new System.Windows.Forms.GroupBox();
            this.CurObjCoordY = new System.Windows.Forms.NumericUpDown();
            this.CurObjCoordX = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.Brush_GroupBox = new System.Windows.Forms.GroupBox();
            this.Textures_GroupBox = new System.Windows.Forms.GroupBox();
            this.Textures_List = new System.Windows.Forms.ListBox();
            this.Brush_ComboBox = new System.Windows.Forms.ComboBox();
            this.Layers_GroupBox = new System.Windows.Forms.GroupBox();
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
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выйтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиМираToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.функцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.найтиКоллизииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отображениеПроходимостиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.перезагрузитьТекстурыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.Object_GroupBox.SuspendLayout();
            this.CurrentObjectData_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWater)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxSatiety)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxHP)).BeginInit();
            this.CurrentObjectHitbox_GroupBox.SuspendLayout();
            this.CurrentObjectHitboxSize_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurObjHitboxSizeHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurObjHitboxSizeWidth)).BeginInit();
            this.CurrentObjectHitboxCoords_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurObjHitboxCoordY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurObjHitboxCoordX)).BeginInit();
            this.CurrentObjectSize_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurObjSizeHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurObjSizeWidth)).BeginInit();
            this.CurrentObjectCoords_GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurObjCoordY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurObjCoordX)).BeginInit();
            this.Brush_GroupBox.SuspendLayout();
            this.Textures_GroupBox.SuspendLayout();
            this.Layers_GroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.Object_GroupBox);
            this.panel1.Controls.Add(this.Brush_GroupBox);
            this.panel1.Controls.Add(this.Layers_GroupBox);
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
            this.Object_GroupBox.Controls.Add(this.CurrentObjectData_GroupBox);
            this.Object_GroupBox.Controls.Add(this.CurrentObjectHitbox_GroupBox);
            this.Object_GroupBox.Controls.Add(this.CurrentObjectSize_GroupBox);
            this.Object_GroupBox.Controls.Add(this.CurrentObjectCoords_GroupBox);
            this.Object_GroupBox.Controls.Add(this.label4);
            this.Object_GroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.Object_GroupBox.Location = new System.Drawing.Point(14, 539);
            this.Object_GroupBox.Name = "Object_GroupBox";
            this.Object_GroupBox.Size = new System.Drawing.Size(281, 366);
            this.Object_GroupBox.TabIndex = 7;
            this.Object_GroupBox.TabStop = false;
            this.Object_GroupBox.Text = "Объект";
            // 
            // CurrentObjectData_GroupBox
            // 
            this.CurrentObjectData_GroupBox.Controls.Add(this.label6);
            this.CurrentObjectData_GroupBox.Controls.Add(this.label5);
            this.CurrentObjectData_GroupBox.Controls.Add(this.label3);
            this.CurrentObjectData_GroupBox.Controls.Add(this.MaxWater);
            this.CurrentObjectData_GroupBox.Controls.Add(this.MaxSatiety);
            this.CurrentObjectData_GroupBox.Controls.Add(this.MaxHP);
            this.CurrentObjectData_GroupBox.Location = new System.Drawing.Point(3, 248);
            this.CurrentObjectData_GroupBox.Name = "CurrentObjectData_GroupBox";
            this.CurrentObjectData_GroupBox.Size = new System.Drawing.Size(269, 112);
            this.CurrentObjectData_GroupBox.TabIndex = 6;
            this.CurrentObjectData_GroupBox.TabStop = false;
            this.CurrentObjectData_GroupBox.Text = "Характеристики";
            this.CurrentObjectData_GroupBox.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "Макс. жажда";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Макс. голод";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Макс. HP";
            // 
            // MaxWater
            // 
            this.MaxWater.Location = new System.Drawing.Point(106, 80);
            this.MaxWater.Maximum = new decimal(new int[] {
            2147000000,
            0,
            0,
            0});
            this.MaxWater.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.MaxWater.Name = "MaxWater";
            this.MaxWater.Size = new System.Drawing.Size(157, 23);
            this.MaxWater.TabIndex = 2;
            this.MaxWater.ValueChanged += new System.EventHandler(this.MaxWater_ValueChanged);
            // 
            // MaxSatiety
            // 
            this.MaxSatiety.Location = new System.Drawing.Point(106, 51);
            this.MaxSatiety.Maximum = new decimal(new int[] {
            2147000000,
            0,
            0,
            0});
            this.MaxSatiety.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.MaxSatiety.Name = "MaxSatiety";
            this.MaxSatiety.Size = new System.Drawing.Size(157, 23);
            this.MaxSatiety.TabIndex = 1;
            this.MaxSatiety.ValueChanged += new System.EventHandler(this.MaxSatiety_ValueChanged);
            // 
            // MaxHP
            // 
            this.MaxHP.Location = new System.Drawing.Point(106, 22);
            this.MaxHP.Maximum = new decimal(new int[] {
            2147000000,
            0,
            0,
            0});
            this.MaxHP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.MaxHP.Name = "MaxHP";
            this.MaxHP.Size = new System.Drawing.Size(157, 23);
            this.MaxHP.TabIndex = 0;
            this.MaxHP.ValueChanged += new System.EventHandler(this.MaxHP_ValueChanged);
            // 
            // CurrentObjectHitbox_GroupBox
            // 
            this.CurrentObjectHitbox_GroupBox.Controls.Add(this.CurrentObjectHitboxSize_GroupBox);
            this.CurrentObjectHitbox_GroupBox.Controls.Add(this.CurrentObjectHitboxCoords_GroupBox);
            this.CurrentObjectHitbox_GroupBox.Location = new System.Drawing.Point(3, 128);
            this.CurrentObjectHitbox_GroupBox.Name = "CurrentObjectHitbox_GroupBox";
            this.CurrentObjectHitbox_GroupBox.Size = new System.Drawing.Size(269, 114);
            this.CurrentObjectHitbox_GroupBox.TabIndex = 5;
            this.CurrentObjectHitbox_GroupBox.TabStop = false;
            this.CurrentObjectHitbox_GroupBox.Text = "Хитбокс";
            // 
            // CurrentObjectHitboxSize_GroupBox
            // 
            this.CurrentObjectHitboxSize_GroupBox.Controls.Add(this.CurObjHitboxSizeHeight);
            this.CurrentObjectHitboxSize_GroupBox.Controls.Add(this.CurObjHitboxSizeWidth);
            this.CurrentObjectHitboxSize_GroupBox.Location = new System.Drawing.Point(138, 22);
            this.CurrentObjectHitboxSize_GroupBox.Name = "CurrentObjectHitboxSize_GroupBox";
            this.CurrentObjectHitboxSize_GroupBox.Size = new System.Drawing.Size(125, 83);
            this.CurrentObjectHitboxSize_GroupBox.TabIndex = 6;
            this.CurrentObjectHitboxSize_GroupBox.TabStop = false;
            this.CurrentObjectHitboxSize_GroupBox.Text = "Размер";
            // 
            // CurObjHitboxSizeHeight
            // 
            this.CurObjHitboxSizeHeight.Enabled = false;
            this.CurObjHitboxSizeHeight.Location = new System.Drawing.Point(6, 51);
            this.CurObjHitboxSizeHeight.Maximum = new decimal(new int[] {
            2147000000,
            0,
            0,
            0});
            this.CurObjHitboxSizeHeight.Minimum = new decimal(new int[] {
            2147000000,
            0,
            0,
            -2147483648});
            this.CurObjHitboxSizeHeight.Name = "CurObjHitboxSizeHeight";
            this.CurObjHitboxSizeHeight.Size = new System.Drawing.Size(113, 23);
            this.CurObjHitboxSizeHeight.TabIndex = 3;
            this.CurObjHitboxSizeHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.CurObjHitboxSizeHeight.ValueChanged += new System.EventHandler(this.CurObjHitboxSizeHeight_ValueChanged);
            // 
            // CurObjHitboxSizeWidth
            // 
            this.CurObjHitboxSizeWidth.Enabled = false;
            this.CurObjHitboxSizeWidth.Location = new System.Drawing.Point(6, 22);
            this.CurObjHitboxSizeWidth.Maximum = new decimal(new int[] {
            2147000000,
            0,
            0,
            0});
            this.CurObjHitboxSizeWidth.Minimum = new decimal(new int[] {
            2147000000,
            0,
            0,
            -2147483648});
            this.CurObjHitboxSizeWidth.Name = "CurObjHitboxSizeWidth";
            this.CurObjHitboxSizeWidth.Size = new System.Drawing.Size(113, 23);
            this.CurObjHitboxSizeWidth.TabIndex = 2;
            this.CurObjHitboxSizeWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.CurObjHitboxSizeWidth.ValueChanged += new System.EventHandler(this.CurObjHitboxSizeWidth_ValueChanged);
            // 
            // CurrentObjectHitboxCoords_GroupBox
            // 
            this.CurrentObjectHitboxCoords_GroupBox.Controls.Add(this.CurObjHitboxCoordY);
            this.CurrentObjectHitboxCoords_GroupBox.Controls.Add(this.CurObjHitboxCoordX);
            this.CurrentObjectHitboxCoords_GroupBox.Location = new System.Drawing.Point(6, 22);
            this.CurrentObjectHitboxCoords_GroupBox.Name = "CurrentObjectHitboxCoords_GroupBox";
            this.CurrentObjectHitboxCoords_GroupBox.Size = new System.Drawing.Size(126, 83);
            this.CurrentObjectHitboxCoords_GroupBox.TabIndex = 5;
            this.CurrentObjectHitboxCoords_GroupBox.TabStop = false;
            this.CurrentObjectHitboxCoords_GroupBox.Text = "Координаты";
            // 
            // CurObjHitboxCoordY
            // 
            this.CurObjHitboxCoordY.Enabled = false;
            this.CurObjHitboxCoordY.Location = new System.Drawing.Point(6, 51);
            this.CurObjHitboxCoordY.Maximum = new decimal(new int[] {
            2147000000,
            0,
            0,
            0});
            this.CurObjHitboxCoordY.Minimum = new decimal(new int[] {
            2147000000,
            0,
            0,
            -2147483648});
            this.CurObjHitboxCoordY.Name = "CurObjHitboxCoordY";
            this.CurObjHitboxCoordY.Size = new System.Drawing.Size(113, 23);
            this.CurObjHitboxCoordY.TabIndex = 1;
            this.CurObjHitboxCoordY.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.CurObjHitboxCoordY.ValueChanged += new System.EventHandler(this.CurObjHitboxCoordY_ValueChanged);
            // 
            // CurObjHitboxCoordX
            // 
            this.CurObjHitboxCoordX.Enabled = false;
            this.CurObjHitboxCoordX.Location = new System.Drawing.Point(6, 22);
            this.CurObjHitboxCoordX.Maximum = new decimal(new int[] {
            2147000000,
            0,
            0,
            0});
            this.CurObjHitboxCoordX.Minimum = new decimal(new int[] {
            2147000000,
            0,
            0,
            -2147483648});
            this.CurObjHitboxCoordX.Name = "CurObjHitboxCoordX";
            this.CurObjHitboxCoordX.Size = new System.Drawing.Size(113, 23);
            this.CurObjHitboxCoordX.TabIndex = 0;
            this.CurObjHitboxCoordX.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.CurObjHitboxCoordX.ValueChanged += new System.EventHandler(this.CurObjHitboxCoordX_ValueChanged);
            // 
            // CurrentObjectSize_GroupBox
            // 
            this.CurrentObjectSize_GroupBox.Controls.Add(this.CurObjSizeHeight);
            this.CurrentObjectSize_GroupBox.Controls.Add(this.CurObjSizeWidth);
            this.CurrentObjectSize_GroupBox.Location = new System.Drawing.Point(140, 39);
            this.CurrentObjectSize_GroupBox.Name = "CurrentObjectSize_GroupBox";
            this.CurrentObjectSize_GroupBox.Size = new System.Drawing.Size(132, 83);
            this.CurrentObjectSize_GroupBox.TabIndex = 4;
            this.CurrentObjectSize_GroupBox.TabStop = false;
            this.CurrentObjectSize_GroupBox.Text = "Размер";
            // 
            // CurObjSizeHeight
            // 
            this.CurObjSizeHeight.Enabled = false;
            this.CurObjSizeHeight.Location = new System.Drawing.Point(6, 51);
            this.CurObjSizeHeight.Maximum = new decimal(new int[] {
            2147000000,
            0,
            0,
            0});
            this.CurObjSizeHeight.Minimum = new decimal(new int[] {
            2147000000,
            0,
            0,
            -2147483648});
            this.CurObjSizeHeight.Name = "CurObjSizeHeight";
            this.CurObjSizeHeight.Size = new System.Drawing.Size(120, 23);
            this.CurObjSizeHeight.TabIndex = 3;
            this.CurObjSizeHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.CurObjSizeHeight.ValueChanged += new System.EventHandler(this.CurObjSizeHeight_ValueChanged);
            // 
            // CurObjSizeWidth
            // 
            this.CurObjSizeWidth.Enabled = false;
            this.CurObjSizeWidth.Location = new System.Drawing.Point(6, 22);
            this.CurObjSizeWidth.Maximum = new decimal(new int[] {
            2147000000,
            0,
            0,
            0});
            this.CurObjSizeWidth.Minimum = new decimal(new int[] {
            2147000000,
            0,
            0,
            -2147483648});
            this.CurObjSizeWidth.Name = "CurObjSizeWidth";
            this.CurObjSizeWidth.Size = new System.Drawing.Size(120, 23);
            this.CurObjSizeWidth.TabIndex = 2;
            this.CurObjSizeWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.CurObjSizeWidth.ValueChanged += new System.EventHandler(this.CurObjSizeWidth_ValueChanged);
            // 
            // CurrentObjectCoords_GroupBox
            // 
            this.CurrentObjectCoords_GroupBox.Controls.Add(this.CurObjCoordY);
            this.CurrentObjectCoords_GroupBox.Controls.Add(this.CurObjCoordX);
            this.CurrentObjectCoords_GroupBox.Location = new System.Drawing.Point(3, 39);
            this.CurrentObjectCoords_GroupBox.Name = "CurrentObjectCoords_GroupBox";
            this.CurrentObjectCoords_GroupBox.Size = new System.Drawing.Size(131, 83);
            this.CurrentObjectCoords_GroupBox.TabIndex = 3;
            this.CurrentObjectCoords_GroupBox.TabStop = false;
            this.CurrentObjectCoords_GroupBox.Text = "Координаты";
            // 
            // CurObjCoordY
            // 
            this.CurObjCoordY.Enabled = false;
            this.CurObjCoordY.Location = new System.Drawing.Point(6, 51);
            this.CurObjCoordY.Maximum = new decimal(new int[] {
            2147000000,
            0,
            0,
            0});
            this.CurObjCoordY.Minimum = new decimal(new int[] {
            2147000000,
            0,
            0,
            -2147483648});
            this.CurObjCoordY.Name = "CurObjCoordY";
            this.CurObjCoordY.Size = new System.Drawing.Size(119, 23);
            this.CurObjCoordY.TabIndex = 1;
            this.CurObjCoordY.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.CurObjCoordY.ValueChanged += new System.EventHandler(this.CurObjCoordY_ValueChanged);
            // 
            // CurObjCoordX
            // 
            this.CurObjCoordX.Enabled = false;
            this.CurObjCoordX.Location = new System.Drawing.Point(6, 22);
            this.CurObjCoordX.Maximum = new decimal(new int[] {
            2147000000,
            0,
            0,
            0});
            this.CurObjCoordX.Minimum = new decimal(new int[] {
            2147000000,
            0,
            0,
            -2147483648});
            this.CurObjCoordX.Name = "CurObjCoordX";
            this.CurObjCoordX.Size = new System.Drawing.Size(119, 23);
            this.CurObjCoordX.TabIndex = 0;
            this.CurObjCoordX.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.CurObjCoordX.ValueChanged += new System.EventHandler(this.CurObjCoordX_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Всего: 0";
            // 
            // Brush_GroupBox
            // 
            this.Brush_GroupBox.Controls.Add(this.Textures_GroupBox);
            this.Brush_GroupBox.Controls.Add(this.Brush_ComboBox);
            this.Brush_GroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.Brush_GroupBox.Location = new System.Drawing.Point(14, 161);
            this.Brush_GroupBox.Name = "Brush_GroupBox";
            this.Brush_GroupBox.Size = new System.Drawing.Size(281, 372);
            this.Brush_GroupBox.TabIndex = 6;
            this.Brush_GroupBox.TabStop = false;
            this.Brush_GroupBox.Text = "Кисть";
            // 
            // Textures_GroupBox
            // 
            this.Textures_GroupBox.Controls.Add(this.Textures_List);
            this.Textures_GroupBox.Location = new System.Drawing.Point(6, 53);
            this.Textures_GroupBox.Name = "Textures_GroupBox";
            this.Textures_GroupBox.Size = new System.Drawing.Size(269, 314);
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
            "Добавить существо",
            "Добавить игрока"});
            this.Brush_ComboBox.Location = new System.Drawing.Point(6, 19);
            this.Brush_ComboBox.Name = "Brush_ComboBox";
            this.Brush_ComboBox.Size = new System.Drawing.Size(269, 28);
            this.Brush_ComboBox.TabIndex = 0;
            this.Brush_ComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            this.Brush_ComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboBox1_KeyPress);
            // 
            // Layers_GroupBox
            // 
            this.Layers_GroupBox.Controls.Add(this.Layers_BtnAdd);
            this.Layers_GroupBox.Controls.Add(this.Layers_BtnDelete);
            this.Layers_GroupBox.Controls.Add(this.Layers_BtnMoveDown);
            this.Layers_GroupBox.Controls.Add(this.Layers_BtnMoveUp);
            this.Layers_GroupBox.Controls.Add(this.Layers_List);
            this.Layers_GroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.Layers_GroupBox.Location = new System.Drawing.Point(14, 53);
            this.Layers_GroupBox.Name = "Layers_GroupBox";
            this.Layers_GroupBox.Size = new System.Drawing.Size(281, 102);
            this.Layers_GroupBox.TabIndex = 5;
            this.Layers_GroupBox.TabStop = false;
            this.Layers_GroupBox.Text = "Слои:";
            // 
            // Layers_BtnAdd
            // 
            this.Layers_BtnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Layers_BtnAdd.Location = new System.Drawing.Point(243, 22);
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
            this.Layers_BtnDelete.Location = new System.Drawing.Point(243, 60);
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
            this.Layers_BtnMoveDown.Location = new System.Drawing.Point(205, 60);
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
            this.Layers_BtnMoveUp.Location = new System.Drawing.Point(205, 22);
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
            this.Layers_List.Size = new System.Drawing.Size(193, 72);
            this.Layers_List.TabIndex = 4;
            // 
            // HoverBlockCoords
            // 
            this.HoverBlockCoords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.HoverBlockCoords.Location = new System.Drawing.Point(192, 30);
            this.HoverBlockCoords.Name = "HoverBlockCoords";
            this.HoverBlockCoords.Size = new System.Drawing.Size(103, 20);
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
            this.BlockCoords.Size = new System.Drawing.Size(103, 20);
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
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.ЗагрузитьToolStripMenuItem_Click);
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
            this.найтиКоллизииToolStripMenuItem,
            this.отображениеПроходимостиToolStripMenuItem,
            this.перезагрузитьТекстурыToolStripMenuItem});
            this.функцииToolStripMenuItem.Name = "функцииToolStripMenuItem";
            this.функцииToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.функцииToolStripMenuItem.Text = "Функции";
            // 
            // найтиКоллизииToolStripMenuItem
            // 
            this.найтиКоллизииToolStripMenuItem.Name = "найтиКоллизииToolStripMenuItem";
            this.найтиКоллизииToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.найтиКоллизииToolStripMenuItem.Text = "Найти коллизии";
            this.найтиКоллизииToolStripMenuItem.Click += new System.EventHandler(this.НайтиКоллизииToolStripMenuItem_Click);
            // 
            // отображениеПроходимостиToolStripMenuItem
            // 
            this.отображениеПроходимостиToolStripMenuItem.Name = "отображениеПроходимостиToolStripMenuItem";
            this.отображениеПроходимостиToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.отображениеПроходимостиToolStripMenuItem.Text = "Отображение проходимости";
            this.отображениеПроходимостиToolStripMenuItem.Click += new System.EventHandler(this.ОтображениеПроходимостиToolStripMenuItem_Click);
            // 
            // перезагрузитьТекстурыToolStripMenuItem
            // 
            this.перезагрузитьТекстурыToolStripMenuItem.Name = "перезагрузитьТекстурыToolStripMenuItem";
            this.перезагрузитьТекстурыToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.перезагрузитьТекстурыToolStripMenuItem.Text = "Перезагрузить текстуры";
            this.перезагрузитьТекстурыToolStripMenuItem.Click += new System.EventHandler(this.ПерезагрузитьТекстурыToolStripMenuItem_Click);
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
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.WorldEditor_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WorldEditor_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WorldEditor_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WorldEditor_MouseUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.WorldEditor_PreviewKeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Object_GroupBox.ResumeLayout(false);
            this.Object_GroupBox.PerformLayout();
            this.CurrentObjectData_GroupBox.ResumeLayout(false);
            this.CurrentObjectData_GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWater)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxSatiety)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxHP)).EndInit();
            this.CurrentObjectHitbox_GroupBox.ResumeLayout(false);
            this.CurrentObjectHitboxSize_GroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CurObjHitboxSizeHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurObjHitboxSizeWidth)).EndInit();
            this.CurrentObjectHitboxCoords_GroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CurObjHitboxCoordY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurObjHitboxCoordX)).EndInit();
            this.CurrentObjectSize_GroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CurObjSizeHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurObjSizeWidth)).EndInit();
            this.CurrentObjectCoords_GroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CurObjCoordY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurObjCoordX)).EndInit();
            this.Brush_GroupBox.ResumeLayout(false);
            this.Textures_GroupBox.ResumeLayout(false);
            this.Layers_GroupBox.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox Layers_GroupBox;
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem функцииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem найтиКоллизииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отображениеПроходимостиToolStripMenuItem;
        private System.Windows.Forms.GroupBox CurrentObjectHitbox_GroupBox;
        private System.Windows.Forms.GroupBox CurrentObjectHitboxSize_GroupBox;
        private System.Windows.Forms.NumericUpDown CurObjHitboxSizeHeight;
        private System.Windows.Forms.NumericUpDown CurObjHitboxSizeWidth;
        private System.Windows.Forms.GroupBox CurrentObjectHitboxCoords_GroupBox;
        private System.Windows.Forms.NumericUpDown CurObjHitboxCoordY;
        private System.Windows.Forms.NumericUpDown CurObjHitboxCoordX;
        private System.Windows.Forms.GroupBox CurrentObjectSize_GroupBox;
        private System.Windows.Forms.NumericUpDown CurObjSizeHeight;
        private System.Windows.Forms.NumericUpDown CurObjSizeWidth;
        private System.Windows.Forms.GroupBox CurrentObjectCoords_GroupBox;
        private System.Windows.Forms.NumericUpDown CurObjCoordY;
        private System.Windows.Forms.NumericUpDown CurObjCoordX;
        private System.Windows.Forms.GroupBox CurrentObjectData_GroupBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown MaxWater;
        private System.Windows.Forms.NumericUpDown MaxSatiety;
        private System.Windows.Forms.NumericUpDown MaxHP;
        private System.Windows.Forms.ToolStripMenuItem перезагрузитьТекстурыToolStripMenuItem;
    }
}

