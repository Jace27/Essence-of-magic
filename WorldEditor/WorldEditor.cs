using System;
using System.IO;
using System.Drawing;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EssenceOfMagic;
using System.Collections.Generic;
//using GLGDIPlus;

namespace WorldEditor
{
    public partial class WorldEditor : Form
    {
        public WorldEditor()
        {
            InitializeComponent();

            GameData.AnimationFolder = DataPath + "\\Textures\\Animations";
            GameData.ArmorFolder = DataPath + "\\Armor";
            GameData.ObjectsFolder = DataPath + "\\Objects";
            GameData.TextureFolder = DataPath + "\\Textures";
            GameData.TriggersFolder = DataPath + "\\Triggers";
            GameData.WeaponFolder = DataPath + "\\Weapon";
            GameData.WorldFolder = DataPath + "\\Map";

            Textures.Init();
            Animations.Init();
            GameObjects.Init();
            Creatures.Init();
            Players.Init();
            Init();
            SelectedBlock = new Point(0, 0);

            Brush_ComboBox.SelectedItem = Brush_ComboBox.Items[0];

            Textures_List.Items.Clear();

            GarbageCollector.Start();
        }

        static string DataPath = Environment.CurrentDirectory + "\\..\\..\\..\\bin\\Debug\\Resources";
        public void Init()
        {
            DirectoryInfo di = new DirectoryInfo(DataPath);
            using (StreamReader sr = new StreamReader(di.FullName + "\\Map\\world.json"))
                World = JsonSerializer.Deserialize<World>(sr.ReadToEnd());
            GameData.World = World;

            WorldRect = new Rectangle(0, 0, World.Ground.Length * BlockSize.Width, World.Ground[0].Length * BlockSize.Height);
            WorldSettings.WorldSize = new Size(World.Ground.Length, World.Ground[0].Length);

            for (int c = 0; c < GameData.World.Objects.creatures.Length; c++)
                GameData.World.Objects.creatures[c].Vulnarable = false;
            for (int p = 0; p < GameData.World.Objects.players.Length; p++)
                GameData.World.Objects.players[p].Vulnarable = false;

            Collisions = new bool[World.Ground.Length, World.Ground[0].Length];
            for (int x = 0; x < World.Ground.Length; x++)
                for (int y = 0; y < World.Ground[0].Length; y++)
                    if (World.Ground[x][y].Layers.Length > 1)
                        Collisions[x, y] = true;

            GameData.RegenGID();

            label4.Text = String.Format("Всего: {0}", World.Objects.CountAll);
        }

        private static World World;
        static Size BlockSize = new Size(64, 64);

        static Rectangle DrawingRect = new Rectangle(0, 24, 960, 640);
        private static Rectangle WorldRect;

        public static void ResizeWorld(Size NewSize, Point Mode)
        {
            SurfaceBlock[][] NewWorld = new SurfaceBlock[NewSize.Width][];
            for (int x = 0; x < NewWorld.Length; x++) NewWorld[x] = new SurfaceBlock[NewSize.Height];

            int dx = 0, dy = 0;
                 if (Mode.X == 0)
                dx = 0;
            else if (Mode.X == 1)
                dx = (NewWorld.Length - World.Ground.Length) / 2;
            else if (Mode.X == 2)
                dx = NewWorld.Length - World.Ground.Length;

                 if (Mode.Y == 0)
                dy = 0;
            else if (Mode.Y == 1)
                dy = (NewWorld[0].Length - World.Ground[0].Length) / 2;
            else if (Mode.Y == 2)
                dy = NewWorld[0].Length - World.Ground[0].Length;

            for (int x = 0; x < NewWorld.Length; x++)
            {
                for (int y = 0; y < NewWorld[0].Length; y++)
                {
                    if (x >= dx && x < World.Ground.Length + dx && y >= dy && y < World.Ground[0].Length + dy)
                    {
                        NewWorld[x][y] = World.Ground[x - dx][y - dy];
                    }
                    else
                    {
                        NewWorld[x][y] = new SurfaceBlock()
                        {
                            Coords = new Location(x, y, 0),
                            Fogged = true,
                            Darked = true,
                            Layers = new string[0]
                        };
                    }
                }
            }
            for (int i = 0; i < World.Objects.CountAll; i++)
            {
                GameObject go = World.Objects.Get(i);
                go.Location = new Location(
                    go.Location.X + dx * BlockSize.Width,
                    go.Location.Y + dy * BlockSize.Height,
                    go.Location.Z
                );
            }

            World.Ground = NewWorld;
            WorldRect = new Rectangle(0, 0, World.Ground.Length * BlockSize.Width, World.Ground[0].Length * BlockSize.Height);
            WorldSettings.WorldSize = new Size(World.Ground.Length, World.Ground[0].Length);

            Collisions = new bool[World.Ground.Length, World.Ground[0].Length];
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        public void Draw(Graphics graphics)
        {
            int i1 = WorldRect.X * -1 / BlockSize.Width;
            int i2 = i1 + DrawingRect.Width / BlockSize.Width + 1;
            int l1 = WorldRect.Y * -1 / BlockSize.Height;
            int l2 = l1 + DrawingRect.Height / BlockSize.Height + 1;
            Bitmap output = new Bitmap((i2 - i1 - 1) * BlockSize.Width, (l2 - l1 - 1) * BlockSize.Height);
            using (Graphics gr = Graphics.FromImage(output))
            {
                Bitmap buffer1 = new Bitmap(DrawingRect.Width, DrawingRect.Height);
                buffer1.SetResolution(192f, 192f);
                using (Graphics gr1 = Graphics.FromImage(buffer1))
                {
                    gr1.FillRectangle(SystemBrushes.ActiveCaption, new Rectangle(0, 0, buffer1.Width, buffer1.Height));
                    Bitmap buffer2 = new Bitmap(WorldRect.Width + 2, WorldRect.Height + 2);
                    buffer2.SetResolution(192f, 192f);
                    using (Graphics gr2 = Graphics.FromImage(buffer2))
                    {
                        int lm = World.Ground[0][0].Layers.Length;
                        for (int x = 0; x < World.Ground.Length; x++)
                            for (int y = 0; y < World.Ground[0].Length; y++)
                                if (lm < World.Ground[x][y].Layers.Length)
                                    lm = World.Ground[x][y].Layers.Length;

                        #region Отрисовка слоев
                        for (int l = 0; l < lm; l++)
                        {
                            for (int x = i1; x < i2; x++)
                            {
                                if (x >= 0 && x < World.Ground.Length)
                                {
                                    for (int y = l1; y < l2; y++)
                                    {
                                        if (y >= 0 && y < World.Ground[x].Length)
                                        {
                                            Bitmap block = new Bitmap(BlockSize.Width, BlockSize.Height);
                                            block.SetResolution(192f, 192f);
                                            using (Graphics gr3 = Graphics.FromImage(block))
                                            {
                                                if (l < World.Ground[x][y].Layers.Length)
                                                {
                                                    Texture Current = Textures.Get(World.Ground[x][y].Layers[l]);
                                                    if (Current != null)
                                                    {
                                                        if (!Current.isInitialized) Current.Init();
                                                        if (Current.GetIMG().bitmap != null)
                                                            gr3.DrawImage(Current.GetIMG().bitmap, 0, 0, block.Width, block.Height);
                                                    }
                                                }

                                                if (ShowCollisions)
                                                    if (Collisions != null)
                                                        if (Collisions[x, y])
                                                            gr3.DrawRectangle(Pens.Red, 0, 0, block.Width - 1, block.Height - 1);

                                                if (HoverBlock != null && HoverBlock.X >= 0 && HoverBlock.Y >= 0)
                                                    if (x == HoverBlock.X && y == HoverBlock.Y)
                                                        gr3.DrawRectangle(Pens.Silver, 0, 0, block.Width - 1, block.Height - 1);

                                                if (Brush == EditorBrush.SelectBlock)
                                                    if (SelectedBlock != null && SelectedBlock.X >= 0 && SelectedBlock.Y >= 0)
                                                        if (x == SelectedBlock.X && y == SelectedBlock.Y)
                                                            gr3.DrawRectangle(Pens.Blue, 0, 0, block.Width - 1, block.Height - 1);
                                            }
                                            gr2.DrawImage(block, 1 + x * BlockSize.Width, 1 + y * BlockSize.Height, BlockSize.Width, BlockSize.Height);
                                            block.Dispose();
                                        }
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Отрисовка объектов
                        for (int i = 0; i < World.Objects.CountAll; i++)
                        {
                            GameObject go = World.Objects.Get(i);
                            if (go != null)
                            {
                                var img = go.Sprite.GetIMG();
                                Bitmap objectbuffer = new Bitmap(img.bitmap.Width, img.bitmap.Height);
                                using (Graphics gr3 = Graphics.FromImage(objectbuffer))
                                {
                                    if (go.ID != "invisible wall")
                                        gr3.DrawImage(img.bitmap, 0, 0, img.bitmap.Width, img.bitmap.Height);
                                    else
                                        gr3.Clear(Color.FromArgb(127, 255, 255, 0));
                                    if (ShowPassability && go.Hitbox != null)
                                        gr3.FillRectangle(new SolidBrush(Color.FromArgb(127, 255, 255, 0)), go.Hitbox);
                                }
                                Point op = new Point(Convert.ToInt32(Math.Round(go.Location.X, 0)), Convert.ToInt32(Math.Round(go.Location.Y, 0)));
                                gr2.DrawImage(objectbuffer, op.X, op.Y, go.Size.Width, go.Size.Height);
                                if (i == SelectedObjectIndex)
                                    gr2.DrawRectangle(Pens.Aqua, op.X, op.Y, go.Size.Width - 1, go.Size.Height - 1);
                            }
                        }
                        #endregion

                        #region Превью устанавливаемого объекта
                        if (Brush == EditorBrush.AddObject)
                        {
                            if (Textures_List.SelectedIndex >= 0)
                            {
                                string Object = Textures_List.Items[Textures_List.SelectedIndex].ToString();
                                Bitmap obj = new Bitmap(64, 64);
                                Size osize = new Size(0, 0);
                                GameObject temp1 = GameObjects.Get(Object);
                                Creature temp2 = Creatures.Get(Object);
                                Player temp3 = Players.Get(Object);
                                if (temp1 != null)
                                {
                                    obj = temp1.Sprite.GetIMG().bitmap;
                                    osize = temp1.Size;
                                }
                                else if (temp2 != null)
                                {
                                    obj = temp2.Sprite.GetIMG().bitmap;
                                    osize = temp2.Size;
                                }
                                else if (temp3 != null)
                                {
                                    obj = temp3.Sprite.GetIMG().bitmap;
                                    osize = temp3.Size;
                                }
                                gr2.DrawImage(obj, MousePos.X - osize.Width / 2, MousePos.Y - osize.Height / 2, osize.Width, osize.Height);
                                gr2.DrawRectangle(new Pen(Color.Gray), MousePos.X - osize.Width / 2, MousePos.Y - osize.Height / 2, osize.Width, osize.Height);
                            }
                        }
                        if (Brush == EditorBrush.InsertObject)
                        {
                            gr2.DrawImage(IOBuffer.Sprite.GetIMG().bitmap, MousePos.X - IOBuffer.Size.Width / 2, MousePos.Y - IOBuffer.Size.Height / 2, IOBuffer.Size.Width, IOBuffer.Size.Height);
                            gr2.DrawRectangle(new Pen(Color.Gray), MousePos.X - IOBuffer.Size.Width / 2, MousePos.Y - IOBuffer.Size.Height / 2, IOBuffer.Size.Width, IOBuffer.Size.Height);
                        }
                        #endregion

                        gr2.DrawRectangle(Pens.Black, 0, 0, WorldRect.Width + 1, WorldRect.Height + 1);
                    }
                    gr1.DrawImage(buffer2, WorldRect.X, WorldRect.Y);
                    buffer2.Dispose();
                }
                gr.DrawImage(buffer1, 0, 0, buffer1.Width, buffer1.Height);
                buffer1.Dispose();
            }
            graphics.DrawImage(output, DrawingRect);
        }

        private Point _hoverblock;
        public Point HoverBlock
        {
            get { return _hoverblock; }
            set
            {
                _hoverblock = value;
                HoverBlockCoords.Text = String.Format("{0}:{1}", _hoverblock.X, _hoverblock.Y);
                HoverBlockCoords.Invalidate();
            }
        }
        private Point _selectedblock;
        public Point SelectedBlock
        {
            get { return _selectedblock; }
            set
            {
                _selectedblock = value;
                BlockCoords.Text = String.Format("{0}:{1}", _selectedblock.X, _selectedblock.Y);
                BlockCoords.Invalidate();
                if (value.X >= 0 && value.Y >= 0 && value.X < World.Ground.Length && value.Y < World.Ground[0].Length) {
                    Layers_List.Items.Clear();
                    for (int i = 0; i < World.Ground[value.X][value.Y].Layers.Length; i++)
                        Layers_List.Items.Add(World.Ground[value.X][value.Y].Layers[i]);
                }
            }
        }
        Size Shift;

        int SelectedObjectIndex = -1;

        Point MousePos = new Point(0, 0);
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            MousePos = new Point(e.X - WorldRect.X, e.Y - menuStrip1.Height - WorldRect.Y);

            #region Перетаскивание карты и объекта
            if (e.X >= DrawingRect.Left && e.X <= DrawingRect.Right && e.Y >= DrawingRect.Top && e.Y <= DrawingRect.Bottom)
            {
                if (!isObjectDraging && !isMapDraging && PreviousPosition.X < 0 && PreviousPosition.Y < 0)
                    HoverBlock = new Point(MousePos.X / BlockSize.Width, MousePos.Y / BlockSize.Height);
                else if (!isObjectDraging && isMapDraging)
                    WorldRect = new Rectangle(e.X - Shift.Width, e.Y - Shift.Height, World.Ground.Length * BlockSize.Width, World.Ground[0].Length * BlockSize.Height);
                else if (isObjectDraging)
                {
                    if (SelectedObjectIndex != -1)
                    {
                        GameObject go = World.Objects.Get(SelectedObjectIndex);
                        go.Location = new Location(
                            go.Location.X - (PreviousPosition.X - e.X),
                            go.Location.Y - (PreviousPosition.Y - e.Y),
                            go.Location.Z
                        );
                        PreviousPosition = e.Location;
                        UnLockCurrentObjectInfo();
                        UpdateCurrentObjectInfo();
                    }
                    else
                    {
                        isObjectDraging = false;
                        PreviousPosition = new Point(-1, -1);
                        CurrentObjectInfoSetNull();
                        LockCurrentObjectInfo();
                    }
                }
            }
            else
            {
                HoverBlock = new Point(-1, -1);
                isMapDraging = false;
                isObjectDraging = false;
                PreviousPosition = new Point(-1, -1);
                CurrentObjectInfoSetNull();
                LockCurrentObjectInfo();
            }
            #endregion

            #region Действия при движении мыши (выбор, очищение, удаление/добавление слоя)
            if (e.Button == MouseButtons.Left)
            {
                // выбор блока
                if (e.X >= DrawingRect.Left && e.X <= DrawingRect.Right && e.Y >= DrawingRect.Top && e.Y <= DrawingRect.Bottom)
                {
                    Point pos = new Point(e.X - WorldRect.X, e.Y - 24 - WorldRect.Y);
                    SelectedBlock = new Point(pos.X / BlockSize.Width, pos.Y / BlockSize.Height);
                }
                else
                {
                    SelectedBlock = new Point(-1, -1);
                }

                if (SelectedBlock.X != -1 && SelectedBlock.Y != -1 && SelectedBlock.X != PreviousBlock.X && SelectedBlock.Y != PreviousBlock.Y)
                {
                    // очистить блок
                    if (Brush == EditorBrush.ClearBlock)
                    {
                        if (SelectedBlock.X >= 0 && SelectedBlock.Y >= 0 && SelectedBlock.X < World.Ground.Length && SelectedBlock.Y < World.Ground[0].Length)
                        {
                            World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers = new string[0];
                            Collisions[SelectedBlock.X, SelectedBlock.Y] = false;
                        }
                    }

                    // удалить слой
                    if (Brush == EditorBrush.DeleteLayer && Textures_List.SelectedIndex >= 0)
                    {
                        if (SelectedBlock.X >= 0 && SelectedBlock.Y >= 0 && SelectedBlock.X < World.Ground.Length && SelectedBlock.Y < World.Ground[0].Length)
                        {
                            for (int l = 0; l < World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers.Length; l++)
                            {
                                if (World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers[l] == Textures_List.Items[Textures_List.SelectedIndex].ToString())
                                {
                                    string[] new_layers = new string[World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers.Length - 1];
                                    int i1 = 0;
                                    for (int i2 = 0; i2 < new_layers.Length + 1; i2++)
                                    {
                                        if (World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers[i2] != Textures_List.Items[Textures_List.SelectedIndex].ToString())
                                        {
                                            new_layers[i1] = World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers[i2];
                                            i1++;
                                        }
                                    }
                                    World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers = new_layers;
                                    Collisions[SelectedBlock.X, SelectedBlock.Y] = false;
                                    break;
                                }
                            }
                        }
                    }

                    // добавить слой
                    if (Brush == EditorBrush.AddLayer && Textures_List.SelectedIndex >= 0)
                    {
                        if (SelectedBlock.X >= 0 && SelectedBlock.Y >= 0 && SelectedBlock.X < World.Ground.Length && SelectedBlock.Y < World.Ground[0].Length)
                        {
                            bool isAlreadyHaveLayer = false;
                            for (int l = 0; l < World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers.Length; l++)
                                if (World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers[l] == Textures_List.Items[Textures_List.SelectedIndex].ToString())
                                    isAlreadyHaveLayer = true;
                            if (!isAlreadyHaveLayer)
                            {
                                string[] new_layers = new string[World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers.Length + 1];
                                for (int l = 0; l < World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers.Length; l++)
                                    new_layers[l] = World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers[l];
                                new_layers[new_layers.Length - 1] = Textures_List.Items[Textures_List.SelectedIndex].ToString();
                                World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers = new_layers;
                                if (new_layers.Length > 1) Collisions[SelectedBlock.X, SelectedBlock.Y] = true;
                            }
                        }
                    }
                }
            }
            #endregion

            Draw(CreateGraphics());
        }

        bool isMapDraging = false;
        bool isObjectDraging = false;
        Point PreviousPosition = new Point(-1, -1);
        Point PreviousBlock = new Point(-1, -1);
        private void WorldEditor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < DrawingRect.Left || e.X > DrawingRect.Right || e.Y < DrawingRect.Top || e.Y > DrawingRect.Bottom) return;

            MousePos = new Point(e.X - WorldRect.X, e.Y - menuStrip1.Height - WorldRect.Y);
            if (Brush == EditorBrush.Hand)
            {
                if (!isObjectDraging)
                    isMapDraging = true;
                PreviousPosition = e.Location;
                Shift = new Size(e.X - WorldRect.X, e.Y - WorldRect.Y);
            }

            #region Действия по клику (выбор, очищение, удаление/добавление слоя, выбор/добавление объекта)
            // выбор блока
            if (e.X >= DrawingRect.Left && e.X <= DrawingRect.Right && e.Y >= DrawingRect.Top && e.Y <= DrawingRect.Bottom)
            {
                Point pos = new Point(e.X - WorldRect.X, e.Y - 24 - WorldRect.Y);
                SelectedBlock = new Point(pos.X / BlockSize.Width, pos.Y / BlockSize.Height);
            }
            else
            {
                SelectedBlock = new Point(-1, -1);
            }

            // очистить блок
            if (Brush == EditorBrush.ClearBlock)
            {
                if (SelectedBlock.X >= 0 && SelectedBlock.Y >= 0 && SelectedBlock.X < World.Ground.Length && SelectedBlock.Y < World.Ground[0].Length)
                {
                    World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers = new string[0];
                    Collisions[SelectedBlock.X, SelectedBlock.Y] = false;
                }
            }

            // удалить слой
            if (Brush == EditorBrush.DeleteLayer && Textures_List.SelectedIndex >= 0)
            {
                if (SelectedBlock.X >= 0 && SelectedBlock.Y >= 0 && SelectedBlock.X < World.Ground.Length && SelectedBlock.Y < World.Ground[0].Length)
                {
                    for (int l = 0; l < World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers.Length; l++)
                    {
                        if (World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers[l] == Textures_List.Items[Textures_List.SelectedIndex].ToString())
                        {
                            string[] new_layers = new string[World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers.Length - 1];
                            int i1 = 0;
                            for (int i2 = 0; i2 < new_layers.Length + 1; i2++)
                            {
                                if (World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers[i2] != Textures_List.Items[Textures_List.SelectedIndex].ToString())
                                {
                                    new_layers[i1] = World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers[i2];
                                    i1++;
                                }
                            }
                            World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers = new_layers;
                            Collisions[SelectedBlock.X, SelectedBlock.Y] = false;
                            break;
                        }
                    }
                }
            }

            // добавить слой
            if (Brush == EditorBrush.AddLayer && Textures_List.SelectedIndex >= 0)
            {
                if (SelectedBlock.X >= 0 && SelectedBlock.Y >= 0 && SelectedBlock.X < World.Ground.Length && SelectedBlock.Y < World.Ground[0].Length)
                {
                    bool isAlreadyHaveLayer = false;
                    for (int l = 0; l < World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers.Length; l++)
                        if (World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers[l] == Textures_List.Items[Textures_List.SelectedIndex].ToString())
                            isAlreadyHaveLayer = true;
                    if (!isAlreadyHaveLayer)
                    {
                        string[] new_layers = new string[World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers.Length + 1];
                        for (int l = 0; l < World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers.Length; l++)
                            new_layers[l] = World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers[l];
                        new_layers[new_layers.Length - 1] = Textures_List.Items[Textures_List.SelectedIndex].ToString();
                        World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers = new_layers;
                        if (new_layers.Length > 1) Collisions[SelectedBlock.X, SelectedBlock.Y] = true;
                    }
                }
            }

            //добавить объект
            if (Brush == EditorBrush.AddObject)
            {
                if (Textures_List.SelectedIndex >= 0)
                {
                    string Object = Textures_List.Items[Textures_List.SelectedIndex].ToString();
                    GameObject temp1;
                    try { temp1 = GameObjects.Get(Object).Clone(); } catch { temp1 = null; }
                    Creature temp2;
                    try { temp2 = Creatures.Get(Object).Clone(); } catch { temp2 = null; }
                    Player temp3;
                    try { temp3 = Players.Get(Object).Clone(); } catch { temp3 = null; }
                    if (temp1 != null)
                    {
                        temp1.Location = new Location(MousePos.X - temp1.Size.Width / 2, MousePos.Y - temp1.Size.Height / 2, 0);
                        World.Objects.Add(temp1);
                    }
                    else if (temp2 != null)
                    {
                        temp2.Location = new Location(MousePos.X - temp2.Size.Width / 2, MousePos.Y - temp2.Size.Height / 2, 0);
                        World.Objects.Add(temp2);
                    }
                    else if (temp3 != null)
                    {
                        temp3.Location = new Location(MousePos.X - temp3.Size.Width / 2, MousePos.Y - temp3.Size.Height / 2, 0);
                        World.Objects.Add(temp3);
                    }
                }

                label4.Text = String.Format("Всего: {0}", World.Objects.CountAll);
            }

            //выбор объекта
            if (Brush == EditorBrush.SelectObject)
            {
                double distance = 2147;
                int d = -1;

                for (int i = 0; i < World.Objects.CountAll; i++)
                {
                    GameObject go = World.Objects.Get(i);
                    Point Center = new Point(
                        (int)Math.Round(go.Location.X + go.Size.Width / 2, 0),
                        (int)Math.Round(go.Location.Y + go.Size.Height / 2, 0)
                    );
                    int d2 = (int)Math.Round(Math.Sqrt(Math.Pow(MousePos.X - Center.X, 2) + Math.Pow(MousePos.Y - Center.Y, 2)), 0);
                    if (distance > d2) 
                    { 
                        distance = d2; 
                        d = i; 
                    }
                }

                if (d != -1)
                {
                    SelectedObjectIndex = d;

                    GameObject go = World.Objects.Get(SelectedObjectIndex, out ObjectType type);

                    Object_GroupBox.Text = "Объект " + go.gID;

                    CurObjCoordX.Value = (decimal)Math.Round(go.Location.X, 0);
                    CurObjCoordY.Value = (decimal)Math.Round(go.Location.Y, 0);
                    CurObjSizeWidth.Value = go.Size.Width;
                    CurObjSizeHeight.Value = go.Size.Height;

                    CurObjHitboxCoordX.Value = go.Hitbox.X;
                    CurObjHitboxCoordY.Value = go.Hitbox.Y;
                    CurObjHitboxSizeWidth.Value = go.Hitbox.Width;
                    CurObjHitboxSizeHeight.Value = go.Hitbox.Height;

                    if (type == ObjectType.Creature || type == ObjectType.Player)
                        CurrentObjectData_GroupBox.Visible = true;
                    else
                        CurrentObjectData_GroupBox.Visible = false;

                    UnLockCurrentObjectInfo();
                    UpdateCurrentObjectInfo();
                }

                #region Перетаскивание объекта
                if (SelectedObjectIndex != -1)
                {
                    GameObject go = World.Objects.Get(SelectedObjectIndex);
                    if (MousePos.X > go.Location.X &&
                        MousePos.X < go.Location.X + go.Size.Width &&
                        MousePos.Y > go.Location.Y &&
                        MousePos.Y < go.Location.Y + go.Size.Height)
                        isObjectDraging = true;
                    UnLockCurrentObjectInfo();
                    UpdateCurrentObjectInfo();
                    PreviousPosition = e.Location;
                }
                #endregion
            }
            else
            {
                SelectedObjectIndex = -1;
                CurrentObjectInfoSetNull();
                LockCurrentObjectInfo();
                Object_GroupBox.Text = "Объект";
            }

            //вставить объект
            if (Brush == EditorBrush.InsertObject && IOBuffer != null)
            {
                IOBuffer.Location = new Location(MousePos.X - IOBuffer.Size.Width / 2, MousePos.Y - IOBuffer.Size.Height / 2, 0);

                GameObject temp1 = GameObjects.Get(IOBuffer.ID);
                Creature temp2 = Creatures.Get(IOBuffer.ID);
                Player temp3 = Players.Get(IOBuffer.ID);
                if (temp1 != null)
                {
                    IOBuffer.gID = DateTime.Now.Ticks.ToString() + (World.Objects.CountObjects - 1).ToString();
                    World.Objects.Add(((GameObject)IOBuffer).Clone());
                }
                else
                if (temp2 != null)
                {
                    IOBuffer.gID = DateTime.Now.Ticks.ToString() + (World.Objects.CountObjects + World.Objects.CountCreatures - 1).ToString();
                    World.Objects.Add(((Creature)IOBuffer).Clone());
                }
                else
                if (temp3 != null)
                {
                    IOBuffer.gID = DateTime.Now.Ticks.ToString() + (World.Objects.CountAll - 1).ToString();
                    World.Objects.Add(((Player)IOBuffer).Clone());
                }
            }
            #endregion

            Draw(CreateGraphics());
        }

        private void WorldEditor_MouseUp(object sender, MouseEventArgs e)
        {
            isMapDraging = false;
            isObjectDraging = false;
            PreviousPosition = new Point(-1, -1);
        }

        private void WorldEditor_MouseClick(object sender, MouseEventArgs e)
        {
            // перетаскивание
            isMapDraging = false;
            isObjectDraging = false;
            PreviousPosition = new Point(-1, -1);

            // отрисовка
            Draw(CreateGraphics());
        }

        private void ВыйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void СохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckCreatureData();
            DirectoryInfo di = new DirectoryInfo(DataPath);
            string json = JsonSerializer.Serialize<World>(World, GameData.JsonOpts);
            using (StreamWriter sw = new StreamWriter(di.FullName + "\\Map\\world.json", false))
                sw.Write(json);
            MessageBox.Show("Сохранено");
        }

        #region Layers GroupBox
        Button Hovered;
        private void Layers_Btns_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.DrawImage(Image.FromFile(EssenceOfMagic.GameData.TextureFolder + "\\Technical\\" + ((Button)sender).Tag), 1, 1, ((Button)sender).Width - 2, ((Button)sender).Height - 2);
            if (Hovered != null && Hovered == ((Button)sender))
                e.Graphics.DrawRectangle(Pens.Silver, 0, 0, ((Button)sender).Width - 1, ((Button)sender).Height - 1);
            else
                e.Graphics.DrawRectangle(Pens.White, 0, 0, ((Button)sender).Width - 1, ((Button)sender).Height - 1);
        }

        private void Layers_Btns_MouseEnter(object sender, EventArgs e)
        {
            Hovered = ((Button)sender);
            Graphics g = ((Button)sender).CreateGraphics();
            g.DrawRectangle(Pens.Silver, 0, 0, ((Button)sender).Width - 1, ((Button)sender).Height - 1);
        }

        private void Layers_Btns_MouseLeave(object sender, EventArgs e)
        {
            Hovered = null;
            Graphics g = ((Button)sender).CreateGraphics();
            g.DrawRectangle(Pens.White, 0, 0, ((Button)sender).Width - 1, ((Button)sender).Height - 1);
        }

        public string SelectedID { get; set; }

        private void Layers_BtnAdd_Click(object sender, EventArgs e)
        {
            if (SelectedBlock.X >= 0 && SelectedBlock.Y >= 0 && SelectedBlock.X < World.Ground.Length && SelectedBlock.Y < World.Ground[0].Length) 
            {
                SelectTextureDialog std = new SelectTextureDialog(Textures.Index, this);
                if (std.ShowDialog() == DialogResult.OK)
                {
                    if (!Textures.Get(SelectedID).isInitialized) Textures.Get(SelectedID).Init();
                    Layers_List.Items.Add(SelectedID);
                    string[] layers = World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers;
                    Array.Resize<string>(ref layers, layers.Length + 1);
                    layers[layers.Length - 1] = SelectedID;
                    World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers = layers;
                    Draw(CreateGraphics());
                }
            }
        }

        private void Layers_BtnMoveUp_Click(object sender, EventArgs e)
        {
            if (SelectedBlock.X >= 0 && SelectedBlock.Y >= 0 && SelectedBlock.X < World.Ground.Length && SelectedBlock.Y < World.Ground[0].Length)
            {
                if (Layers_List.SelectedIndex > 0)
                {
                    int s = Layers_List.SelectedIndex;
                    object temp = Layers_List.Items[s - 1];
                    Layers_List.Items[s - 1] = Layers_List.Items[s];
                    Layers_List.Items[s] = temp;

                    string[] newlayers = new string[Layers_List.Items.Count];
                    for (int i = 0; i < Layers_List.Items.Count; i++)
                        newlayers[i] = Layers_List.Items[i].ToString();

                    World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers = newlayers;
                    Draw(CreateGraphics());

                    Layers_List.SelectedIndex = s - 1;
                }
            }
        }

        private void Layers_BtnMoveDown_Click(object sender, EventArgs e)
        {
            if (SelectedBlock.X >= 0 && SelectedBlock.Y >= 0 && SelectedBlock.X < World.Ground.Length && SelectedBlock.Y < World.Ground[0].Length)
            {
                if (Layers_List.SelectedIndex < Layers_List.Items.Count - 1)
                {
                    int s = Layers_List.SelectedIndex;
                    object temp = Layers_List.Items[s + 1];
                    Layers_List.Items[s + 1] = Layers_List.Items[s];
                    Layers_List.Items[s] = temp;

                    string[] newlayers = new string[Layers_List.Items.Count];
                    for (int i = 0; i < Layers_List.Items.Count; i++)
                        newlayers[i] = Layers_List.Items[i].ToString();

                    World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers = newlayers;
                    Draw(CreateGraphics());

                    Layers_List.SelectedIndex = s + 1;
                }
            }
        }

        private void Layers_BtnDelete_Click(object sender, EventArgs e)
        {
            if (SelectedBlock.X >= 0 && SelectedBlock.Y >= 0 && SelectedBlock.X < World.Ground.Length && SelectedBlock.Y < World.Ground[0].Length)
            {
                if (Layers_List.SelectedIndex >= 0)
                {
                    Layers_List.Items.RemoveAt(Layers_List.SelectedIndex);
                    string[] layers = new string[Layers_List.Items.Count];
                    for (int i = 0; i < layers.Length; i++)
                        layers[i] = Layers_List.Items[i].ToString();
                    World.Ground[SelectedBlock.X][SelectedBlock.Y].Layers = layers;
                    Draw(CreateGraphics());
                }
            }
        }
        #endregion

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Brush_ComboBox.Items[Brush_ComboBox.SelectedIndex].ToString())
            {
                case "Выбрать блок":
                    Brush = EditorBrush.SelectBlock;
                    Textures_List.Items.Clear();
                    break;
                case "Удалить слой":
                    Brush = EditorBrush.DeleteLayer;
                    Textures_List.Items.Clear();
                    for (int i = 0; i < Textures.Index.Length; i++)
                        if (Textures.Index[i].Type == TextureTypes.Layer)
                            Textures_List.Items.Add(Textures.Index[i].ID);
                    break;
                case "Добавить слой":
                    Brush = EditorBrush.AddLayer;
                    Textures_List.Items.Clear();
                    for (int i = 0; i < Textures.Index.Length; i++)
                        if (Textures.Index[i].Type == TextureTypes.Layer)
                            Textures_List.Items.Add(Textures.Index[i].ID);
                    break;
                case "Выбрать объект":
                    Brush = EditorBrush.SelectObject;
                    Textures_List.Items.Clear();
                    for (int i = 0; i < GameObjects.Index.Length; i++)
                        Textures_List.Items.Add(GameObjects.Index[i].ID);
                    for (int i = 0; i < Creatures.Index.Length; i++)
                        Textures_List.Items.Add(Creatures.Index[i].ID);
                    for (int i = 0; i < Players.Index.Length; i++)
                        Textures_List.Items.Add(Players.Index[i].ID);
                    break;
                case "Добавить объект":
                    Brush = EditorBrush.AddObject;
                    Textures_List.Items.Clear();
                    for (int i = 0; i < GameObjects.Index.Length; i++)
                        Textures_List.Items.Add(GameObjects.Index[i].ID);
                    break;
                case "Очистить блок":
                    Brush = EditorBrush.ClearBlock;
                    Textures_List.Items.Clear();
                    break;
                case "Рука":
                    Brush = EditorBrush.Hand;
                    Textures_List.Items.Clear();
                    break;
                case "Добавить существо":
                    Brush = EditorBrush.AddObject;
                    Textures_List.Items.Clear();
                    for (int i = 0; i < Creatures.Index.Length; i++)
                        Textures_List.Items.Add(Creatures.Index[i].ID);
                    break;
                case "Добавить игрока":
                    Brush = EditorBrush.AddObject;
                    Textures_List.Items.Clear();
                    for (int i = 0; i < Players.Index.Length; i++)
                        Textures_List.Items.Add(Players.Index[i].ID);
                    break;
            }
            try { Draw(CreateGraphics()); } catch { }
        }

        EditorBrush Brush;

        private enum EditorBrush
        {
            SelectBlock,
            DeleteLayer,
            AddLayer,
            SelectObject,
            AddObject,
            ClearBlock,
            Hand,
            InsertObject
        }

        private void WorldEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 's' || e.KeyChar == 'ы')
                Brush_ComboBox.SelectedItem = "Выбрать блок";

            if (e.KeyChar == 'c' || e.KeyChar == 'с')
                Brush_ComboBox.SelectedItem = "Очистить блок";

            if (e.KeyChar == 'l' || e.KeyChar == 'д')
                Brush_ComboBox.SelectedItem = "Добавить слой";

            if (e.KeyChar == 'd' || e.KeyChar == 'в')
                Brush_ComboBox.SelectedItem = "Удалить слой";

            if ((e.KeyChar == 'o' || e.KeyChar == 'щ') && Brush_ComboBox.SelectedItem.ToString() == "Добавить существо")
                Brush_ComboBox.SelectedItem = "Добавить игрока";

            if ((e.KeyChar == 'o' || e.KeyChar == 'щ') && Brush_ComboBox.SelectedItem.ToString() == "Добавить объект")
                Brush_ComboBox.SelectedItem = "Добавить существо";

            if ((e.KeyChar == 'o' || e.KeyChar == 'щ') && Brush_ComboBox.SelectedItem.ToString() != "Добавить существо" && Brush_ComboBox.SelectedItem.ToString() != "Добавить игрока")
                Brush_ComboBox.SelectedItem = "Добавить объект";

            if (e.KeyChar == 'x' || e.KeyChar == 'ч')
                Brush_ComboBox.SelectedItem = "Выбрать объект";

            if (e.KeyChar == 'h' || e.KeyChar == 'р')
                Brush_ComboBox.SelectedItem = "Рука";
        }

        private void ComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void НастройкиМираToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorldSettings worldSettings = new WorldSettings();
            worldSettings.ShowDialog();
            Draw(CreateGraphics());
        }

        GameObject IOBuffer;

        bool isCTRL = false;
        private void WorldEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) isCTRL = true;
            #region ctrl+x, ctrl+c, ctrl+v
            if (isCTRL)
            {
                if (e.KeyCode.ToString() == "X")
                {
                    if (SelectedObjectIndex != -1)
                    {
                        IOBuffer = World.Objects.Get(SelectedObjectIndex);
                        World.Objects.Delete(SelectedObjectIndex);
                    }
                    SelectedObjectIndex = -1;
                }
                if (e.KeyCode.ToString() == "C")
                {
                    if (SelectedObjectIndex != -1) IOBuffer = World.Objects.Get(SelectedObjectIndex).Clone();
                }
                if (e.KeyCode.ToString() == "V")
                {
                    if (IOBuffer != null)
                    {
                        Brush = EditorBrush.InsertObject;
                    }
                }
            }
            #endregion
            #region ctrl+z, ctrl+y
            if (isCTRL)
            {
                if (e.KeyCode.ToString() == "Z")
                {

                }
                if (e.KeyCode.ToString() == "Y")
                {

                }
            }
            #endregion

            if (e.KeyCode.ToString() == "Delete")
            {
                if (Brush == EditorBrush.SelectObject)
                {
                    if (SelectedObjectIndex != -1)
                    {
                        World.Objects.Delete(SelectedObjectIndex);
                        SelectedObjectIndex = -1;
                        label4.Text = String.Format("Всего: {0}", World.Objects.CountAll);
                        CurrentObjectInfoSetNull();
                        LockCurrentObjectInfo();
                    }
                }
            }
            Draw(CreateGraphics());
        }

        private void WorldEditor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) isCTRL = false;
        }

        private void WorldEditor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void ЗагрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Init();
            SelectedBlock = new Point(0, 0);
            Brush_ComboBox.SelectedItem = Brush_ComboBox.Items[0];
            Textures_List.Items.Clear();
        }

        static bool ShowCollisions = false;
        static bool[,] Collisions;
        private void НайтиКоллизииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCollisions = !ShowCollisions;
            найтиКоллизииToolStripMenuItem.Checked = ShowCollisions;
            Collisions = new bool[World.Ground.Length, World.Ground[0].Length];
            for (int x = 0; x < World.Ground.Length; x++)
                for (int y = 0; y < World.Ground[0].Length; y++)
                    if (World.Ground[x][y].Layers.Length > 1)
                        Collisions[x, y] = true;
        }

        static bool ShowPassability = false;
        private void ОтображениеПроходимостиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPassability = !ShowPassability;
            отображениеПроходимостиToolStripMenuItem.Checked = ShowPassability;
        }

        #region Current object info changed
        bool DisableEvents = false;
        private void CurrentObjectInfoSetNull()
        {
            DisableEvents = true;
            {
                CurObjCoordX.Value = -1;
                CurObjCoordY.Value = -1;
                CurObjHitboxCoordX.Value = -1;
                CurObjHitboxCoordY.Value = -1;
                CurObjHitboxSizeWidth.Value = -1;
                CurObjHitboxSizeHeight.Value = -1;
                CurObjSizeWidth.Value = -1;
                CurObjSizeHeight.Value = -1;
                MaxHP.Value = -1;
                MaxSatiety.Value = -1;
                MaxWater.Value = -1;
            }
            DisableEvents = false;
        }
        private void UpdateCurrentObjectInfo()
        {
            GameObject obj = new GameObject();
            if (SelectedObjectIndex != -1)
                obj = World.Objects.Get(SelectedObjectIndex);
            else
                return;

            DisableEvents = true;
            {
                CurObjCoordX.Value = (int)Math.Round(obj.Location.X, 0);
                CurObjCoordY.Value = (int)Math.Round(obj.Location.Y, 0);
                CurObjSizeWidth.Value = obj.Size.Width;
                CurObjSizeHeight.Value = obj.Size.Height;

                CurObjHitboxCoordX.Value = obj.Hitbox.X;
                CurObjHitboxCoordY.Value = obj.Hitbox.Y;
                CurObjHitboxSizeWidth.Value = obj.Hitbox.Width;
                CurObjHitboxSizeHeight.Value = obj.Hitbox.Height;

                if (CurrentObjectData_GroupBox.Visible)
                {
                    if (obj is Creature cr)
                    {
                        MaxHP.Value = (int)Math.Round(cr.HPMax, 0);
                        MaxSatiety.Value = cr.SatietyMax;
                        MaxWater.Value = cr.WaterMax;
                    }
                }
                else
                {
                    MaxHP.Value = -1;
                    MaxSatiety.Value = -1;
                    MaxWater.Value = -1;
                }
            }
            DisableEvents = false;
        }
        private void LockCurrentObjectInfo()
        {
            CurObjCoordX.Enabled = false;
            CurObjCoordY.Enabled = false;
            CurObjHitboxCoordX.Enabled = false;
            CurObjHitboxCoordY.Enabled = false;
            CurObjSizeWidth.Enabled = false;
            CurObjSizeHeight.Enabled = false;
            CurObjHitboxSizeWidth.Enabled = false;
            CurObjHitboxSizeHeight.Enabled = false;
        }
        private void UnLockCurrentObjectInfo()
        {
            CurObjCoordX.Enabled = true;
            CurObjCoordY.Enabled = true;
            CurObjHitboxCoordX.Enabled = true;
            CurObjHitboxCoordY.Enabled = true;
            CurObjSizeWidth.Enabled = true;
            CurObjSizeHeight.Enabled = true;
            CurObjHitboxSizeWidth.Enabled = true;
            CurObjHitboxSizeHeight.Enabled = true;
        }

        decimal cocx_old = -1;
        private void CurObjCoordX_ValueChanged(object sender, EventArgs e)
        {
            if (DisableEvents) return;

            cocx_old = CurObjCoordX.Value;

            GameObject obj = new GameObject();
            if (SelectedObjectIndex != -1)
                obj = World.Objects.Get(SelectedObjectIndex);
            else
                return;
            obj.Location = new Location((double)CurObjCoordX.Value, obj.Location.Y, obj.Location.Z);
            Draw(CreateGraphics());
        }

        decimal cocy_old = -1;
        private void CurObjCoordY_ValueChanged(object sender, EventArgs e)
        {
            if (DisableEvents) return;

            cocy_old = CurObjCoordX.Value;

            GameObject obj = new GameObject();
            if (SelectedObjectIndex != -1)
                obj = World.Objects.Get(SelectedObjectIndex);
            else
                return;
            obj.Location = new Location(obj.Location.X, (double)CurObjCoordY.Value, obj.Location.Z);
            Draw(CreateGraphics());
        }

        decimal cohcx_old = -1;
        private void CurObjHitboxCoordX_ValueChanged(object sender, EventArgs e)
        {
            if (DisableEvents) return;

            cohcx_old = CurObjHitboxCoordX.Value;

            GameObject obj = new GameObject();
            if (SelectedObjectIndex != -1)
                obj = World.Objects.Get(SelectedObjectIndex);
            else
                return;
            obj.Hitbox = new Rectangle((int)CurObjHitboxCoordX.Value, obj.Hitbox.Y, obj.Hitbox.Width, obj.Hitbox.Height);
            Draw(CreateGraphics());
        }

        decimal cohcy_old = -1;
        private void CurObjHitboxCoordY_ValueChanged(object sender, EventArgs e)
        {
            if (DisableEvents) return;

            cohcy_old = CurObjCoordX.Value;

            GameObject obj = new GameObject();
            if (SelectedObjectIndex != -1)
                obj = World.Objects.Get(SelectedObjectIndex);
            else
                return;
            obj.Hitbox = new Rectangle(obj.Hitbox.X, (int)CurObjHitboxCoordY.Value, obj.Hitbox.Width, obj.Hitbox.Height);
            Draw(CreateGraphics());
        }

        decimal cosw_old = -1;
        private void CurObjSizeWidth_ValueChanged(object sender, EventArgs e)
        {
            if (DisableEvents) return;

            cosw_old = CurObjCoordX.Value;

            GameObject obj = new GameObject();
            if (SelectedObjectIndex != -1)
                obj = World.Objects.Get(SelectedObjectIndex);
            else
                return;
            obj.Size = new Size((int)CurObjSizeWidth.Value, obj.Size.Height);
            Draw(CreateGraphics());
        }

        decimal cosh_old = -1;
        private void CurObjSizeHeight_ValueChanged(object sender, EventArgs e)
        {
            if (DisableEvents) return;

            cosh_old = CurObjCoordX.Value;

            GameObject obj = new GameObject();
            if (SelectedObjectIndex != -1)
                obj = World.Objects.Get(SelectedObjectIndex);
            else
                return;
            obj.Size = new Size(obj.Size.Width, (int)CurObjSizeHeight.Value);
            Draw(CreateGraphics());
        }

        decimal cohsw_old = -1;
        private void CurObjHitboxSizeWidth_ValueChanged(object sender, EventArgs e)
        {
            if (DisableEvents) return;

            cohsw_old = CurObjCoordX.Value;

            GameObject obj = new GameObject();
            if (SelectedObjectIndex != -1)
                obj = World.Objects.Get(SelectedObjectIndex);
            else
                return;
            obj.Hitbox = new Rectangle(obj.Hitbox.X, obj.Hitbox.Y, (int)CurObjHitboxSizeWidth.Value, obj.Hitbox.Height);
            Draw(CreateGraphics());
        }

        decimal cohsh_old = -1;
        private void CurObjHitboxSizeHeight_ValueChanged(object sender, EventArgs e)
        {
            if (DisableEvents) return;

            cohsh_old = CurObjCoordX.Value;

            GameObject obj = new GameObject();
            if (SelectedObjectIndex != -1)
                obj = World.Objects.Get(SelectedObjectIndex);
            else
                return;
            obj.Hitbox = new Rectangle(obj.Hitbox.X, obj.Hitbox.Y, obj.Hitbox.Width, (int)CurObjHitboxSizeHeight.Value);
            Draw(CreateGraphics());
        }

        private void MaxHP_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedObjectIndex != -1)
                if (World.Objects.Get(SelectedObjectIndex) is Creature cr) cr.HPMax = (double)MaxHP.Value;
        }

        private void MaxSatiety_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedObjectIndex != -1)
                if (World.Objects.Get(SelectedObjectIndex) is Creature cr) cr.SatietyMax = (int)MaxSatiety.Value;
        }

        private void MaxWater_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedObjectIndex != -1)
                if (World.Objects.Get(SelectedObjectIndex) is Creature cr) cr.WaterMax = (int)MaxWater.Value;
        }
        #endregion

        private void CheckCreatureData()
        {
            for (int i = 0; i < World.Objects.CountCreatures; i++)
            {
                World.Objects.Get(i, out Creature cr);
                if (cr != null)
                {
                    cr.Vulnarable = false;
                    cr.HP = cr.HPMax;
                    cr.Satiety = cr.SatietyMax;
                    cr.Water = cr.WaterMax;
                    cr.Vulnarable = true;
                }
            }
        }

        private void ПерезагрузитьТекстурыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Textures.Init();
            Animations.Init();
            GameObjects.Init();
            Creatures.Init();
            Players.Init();
            Init();
        }
    }
}
