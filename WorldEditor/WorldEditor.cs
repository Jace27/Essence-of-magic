using System;
using System.IO;
using System.Drawing;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EssenceOfMagic;
using System.Collections.Generic;
using GLGDIPlus;

namespace WorldEditor
{
    public partial class WorldEditor : Form
    {
        public WorldEditor()
        {
            InitializeComponent();

            GameData.AnimationFolder = DataPath + "\\Textures\\Animations";
            GameData.ArmorFolder = DataPath + "\\Armor";
            GameData.ObjectsFolder = DataPath + "\\Creatures";
            GameData.TextureFolder = DataPath + "\\Textures";
            GameData.TriggersFolder = DataPath + "\\Triggers";
            GameData.WeaponFolder = DataPath + "\\Weapon";
            GameData.WorldFolder = DataPath + "\\Map";

            Textures.Init();
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
            WorldRect = new Rectangle(0, 0, World.Ground.Length * BlockSize.Width, World.Ground[0].Length * BlockSize.Height);
            WorldSettings.WorldSize = new Size(World.Ground.Length, World.Ground[0].Length);

            if (World.Objects == null) World.Objects = new GameObject[0];
            if (World.Creatures == null) World.Creatures = new Creature[0];
            if (World.Players == null) World.Players = new Player[0];

            Collisions = new bool[World.Ground.Length, World.Ground[0].Length];
            for (int x = 0; x < World.Ground.Length; x++)
                for (int y = 0; y < World.Ground[0].Length; y++)
                    if (World.Ground[x][y].Layers.Length > 1)
                        Collisions[x, y] = true;

            label4.Text = String.Format("Всего: {0}", World.Objects.Length + World.Creatures.Length + World.Players.Length);
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
            for (int i = 0; i < World.Objects.Length; i++)
            {
                World.Objects[i].Location = new Location(
                    World.Objects[i].Location.X + dx * BlockSize.Width,
                    World.Objects[i].Location.Y + dy * BlockSize.Height,
                    World.Objects[i].Location.Z
                );
            }
            for (int i = 0; i < World.Creatures.Length; i++)
            {
                World.Creatures[i].Location = new Location(
                    World.Creatures[i].Location.X + dx * BlockSize.Width,
                    World.Creatures[i].Location.Y + dy * BlockSize.Height,
                    World.Creatures[i].Location.Z
                );
            }
            for (int i = 0; i < World.Players.Length; i++)
            {
                World.Players[i].Location = new Location(
                    World.Players[i].Location.X + dx * BlockSize.Width,
                    World.Players[i].Location.Y + dy * BlockSize.Height,
                    World.Players[i].Location.Z
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
                        int i1 = WorldRect.X * -1 / BlockSize.Width;
                        int i2 = i1 + DrawingRect.Width / BlockSize.Width + 1;
                        for (int x = i1; x < i2; x++)
                        {
                            if (x >= 0 && x < World.Ground.Length) 
                            {
                                int l1 = WorldRect.Y * -1 / BlockSize.Height;
                                int l2 = l1 + DrawingRect.Height / BlockSize.Height + 1;
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
                    for (int i = 0; i < World.Objects.Length; i++)
                    {
                        Bitmap objectbuffer = new Bitmap(World.Objects[i].Size.Width, World.Objects[i].Size.Height);
                        using (Graphics gr3 = Graphics.FromImage(objectbuffer))
                        {
                            gr3.DrawImage(World.Objects[i].Sprite.GetIMG().bitmap, 0, 0, World.Objects[i].Size.Width, World.Objects[i].Size.Height);
                            if (i == SelectedObjectIndex)
                                gr3.DrawRectangle(Pens.Aqua, 0, 0, World.Objects[i].Size.Width - 1, World.Objects[i].Size.Height - 1);
                        }
                        gr2.DrawImage(objectbuffer, Convert.ToInt32(Math.Round(World.Objects[i].Location.X, 0)), Convert.ToInt32(Math.Round(World.Objects[i].Location.Y, 0)), World.Objects[i].Size.Width, World.Objects[i].Size.Height);
                    }
                    for (int i = 0; i < World.Creatures.Length; i++)
                    {
                        Bitmap objectbuffer = new Bitmap(World.Creatures[i].Size.Width, World.Creatures[i].Size.Height);
                        using (Graphics gr3 = Graphics.FromImage(objectbuffer))
                        {
                            gr3.DrawImage(World.Creatures[i].Sprite.GetIMG().bitmap, 0, 0, World.Creatures[i].Size.Width, World.Creatures[i].Size.Height);
                            if (i == SelectedCreatureIndex)
                                gr3.DrawRectangle(Pens.Aqua, 0, 0, World.Creatures[i].Size.Width - 1, World.Creatures[i].Size.Height - 1);
                        }
                        gr2.DrawImage(objectbuffer, Convert.ToInt32(Math.Round(World.Creatures[i].Location.X, 0)), Convert.ToInt32(Math.Round(World.Creatures[i].Location.Y, 0)), World.Creatures[i].Size.Width, World.Creatures[i].Size.Height);
                    }
                    for (int i = 0; i < World.Players.Length; i++)
                    {
                        Bitmap objectbuffer = new Bitmap(World.Players[i].Size.Width, World.Players[i].Size.Height);
                        using (Graphics gr3 = Graphics.FromImage(objectbuffer))
                        {
                            gr3.DrawImage(World.Players[i].Sprite.GetIMG().bitmap, 0, 0, World.Players[i].Size.Width, World.Players[i].Size.Height);
                            if (i == SelectedPlayerIndex)
                                gr3.DrawRectangle(Pens.Aqua, 0, 0, World.Players[i].Size.Width - 1, World.Players[i].Size.Height - 1);
                        }
                        gr2.DrawImage(objectbuffer, Convert.ToInt32(Math.Round(World.Players[i].Location.X, 0)), Convert.ToInt32(Math.Round(World.Players[i].Location.Y, 0)), World.Players[i].Size.Width, World.Players[i].Size.Height);
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
                        }
                    }
                    #endregion

                    gr2.DrawRectangle(Pens.Black, 0, 0, WorldRect.Width + 1, WorldRect.Height + 1);
                }
                gr1.DrawImage(buffer2, WorldRect.X, WorldRect.Y);
                buffer2.Dispose();
            }
            graphics.DrawImageUnscaledAndClipped(buffer1, DrawingRect);
            buffer1.Dispose();

            //if (GC.GetTotalMemory(false) > (2 * 1024 * 1024)) GC.Collect();
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
        int SelectedCreatureIndex = -1;
        int SelectedPlayerIndex = -1;

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
                        World.Objects[SelectedObjectIndex].Location = new Location(
                            World.Objects[SelectedObjectIndex].Location.X - (PreviousPosition.X - e.X), 
                            World.Objects[SelectedObjectIndex].Location.Y - (PreviousPosition.Y - e.Y), 
                            World.Objects[SelectedObjectIndex].Location.Z
                        );
                        PreviousPosition = e.Location;
                        label5.Text = String.Format("Координаты: {0}:{1}", World.Objects[SelectedObjectIndex].Location.X, World.Objects[SelectedObjectIndex].Location.Y);
                    }
                    else if (SelectedCreatureIndex != -1)
                    {
                        World.Creatures[SelectedCreatureIndex].Location = new Location(
                            World.Creatures[SelectedCreatureIndex].Location.X - (PreviousPosition.X - e.X),
                            World.Creatures[SelectedCreatureIndex].Location.Y - (PreviousPosition.Y - e.Y),
                            World.Creatures[SelectedCreatureIndex].Location.Z
                        );
                        PreviousPosition = e.Location;
                        label5.Text = String.Format("Координаты: {0}:{1}", World.Creatures[SelectedCreatureIndex].Location.X, World.Creatures[SelectedCreatureIndex].Location.Y);
                    }
                    else if (SelectedPlayerIndex != -1)
                    {
                        World.Players[SelectedPlayerIndex].Location = new Location(
                            World.Players[SelectedPlayerIndex].Location.X - (PreviousPosition.X - e.X),
                            World.Players[SelectedPlayerIndex].Location.Y - (PreviousPosition.Y - e.Y),
                            World.Players[SelectedPlayerIndex].Location.Z
                        );
                        PreviousPosition = e.Location;
                        label5.Text = String.Format("Координаты: {0}:{1}", World.Players[SelectedPlayerIndex].Location.X, World.Players[SelectedPlayerIndex].Location.Y);
                    }
                    else
                    {
                        isObjectDraging = false;
                        PreviousPosition = new Point(-1, -1);
                        label5.Text = String.Format("Координаты: -1:-1");
                    }
                }
            }
            else
            {
                HoverBlock = new Point(-1, -1);
                isMapDraging = false;
                isObjectDraging = false;
                PreviousPosition = new Point(-1, -1);
                label5.Text = String.Format("Координаты: -1:-1");
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
            MousePos = new Point(e.X - WorldRect.X, e.Y - menuStrip1.Height - WorldRect.Y);
            if (Brush == EditorBrush.Hand)
            {
                if (!isObjectDraging)
                    isMapDraging = true;
                PreviousPosition = e.Location;
                Shift = new Size(e.X - WorldRect.X, e.Y - WorldRect.Y);
            }

            #region Действия по клику (выбор, очищение, удаление/добавление слоя, выбор/добавление объекта
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
                        GameObject[] temp = new GameObject[World.Objects.Length + 1];
                        for (int i = 0; i < World.Objects.Length; i++)
                            temp[i] = World.Objects[i];
                        temp[World.Objects.Length] = temp1;
                        temp[World.Objects.Length].Location = new Location(MousePos.X - temp[World.Objects.Length].Size.Width / 2, MousePos.Y - temp[World.Objects.Length].Size.Height / 2, 0);
                        World.Objects = temp;
                    }
                    else if (temp2 != null)
                    {
                        Creature[] temp = new Creature[World.Creatures.Length + 1];
                        for (int i = 0; i < World.Creatures.Length; i++)
                            temp[i] = World.Creatures[i];
                        temp[World.Creatures.Length] = temp2;
                        temp[World.Creatures.Length].Location = new Location(MousePos.X - temp[World.Creatures.Length].Size.Width / 2, MousePos.Y - temp[World.Creatures.Length].Size.Height / 2, 0);
                        World.Creatures = temp;
                    }
                    else if (temp3 != null)
                    {
                        Player[] temp = new Player[World.Players.Length + 1];
                        for (int i = 0; i < World.Players.Length; i++)
                            temp[i] = World.Players[i];
                        temp[World.Players.Length] = temp3;
                        temp[World.Players.Length].Location = new Location(MousePos.X - temp[World.Players.Length].Size.Width / 2, MousePos.Y - temp[World.Players.Length].Size.Height / 2, 0);
                        World.Players = temp;
                    }
                }

                label4.Text = String.Format("Всего: {0}", World.Objects.Length + World.Creatures.Length + World.Players.Length);
            }

            //выбор объекта
            if (Brush == EditorBrush.SelectObject)
            {
                double distance = 2147;
                int d = -1;

                for (int i = 0; i < World.Objects.Length; i++)
                {
                    Point Center = new Point(
                        (int)Math.Round(World.Objects[i].Location.X + World.Objects[i].Size.Width / 2, 0),
                        (int)Math.Round(World.Objects[i].Location.Y + World.Objects[i].Size.Height / 2, 0)
                    );
                    int d2 = (int)Math.Round(Math.Sqrt(Math.Pow(MousePos.X - Center.X, 2) + Math.Pow(MousePos.Y - Center.Y, 2)), 0);
                    if (distance > d2) 
                    { 
                        distance = d2; 
                        d = i; 
                    }
                }
                for (int i = 0; i < World.Creatures.Length; i++)
                {
                    Point Center = new Point(
                        (int)Math.Round(World.Creatures[i].Location.X + World.Creatures[i].Size.Width / 2, 0),
                        (int)Math.Round(World.Creatures[i].Location.Y + World.Creatures[i].Size.Height / 2, 0)
                    );
                    int d2 = (int)Math.Round(Math.Sqrt(Math.Pow(MousePos.X - Center.X, 2) + Math.Pow(MousePos.Y - Center.Y, 2)), 0);
                    if (distance > d2)
                    {
                        distance = d2;
                        d = i + World.Objects.Length;
                    }
                }
                for (int i = 0; i < World.Players.Length; i++)
                {
                    Point Center = new Point(
                        (int)Math.Round(World.Players[i].Location.X + World.Players[i].Size.Width / 2, 0),
                        (int)Math.Round(World.Players[i].Location.Y + World.Players[i].Size.Height / 2, 0)
                    );
                    int d2 = (int)Math.Round(Math.Sqrt(Math.Pow(MousePos.X - Center.X, 2) + Math.Pow(MousePos.Y - Center.Y, 2)), 0);
                    if (distance > d2)
                    {
                        distance = d2;
                        d = i + World.Objects.Length + World.Creatures.Length;
                    }
                }

                if (d != -1)
                {
                    SelectedObjectIndex = -1;
                    SelectedCreatureIndex = -1;
                    SelectedPlayerIndex = -1;
                    ObjectIndex.Enabled = false;
                    ObjectIndex.Minimum = 0;
                    if (d >= World.Objects.Length + World.Creatures.Length) //игрок ближе
                    {
                        SelectedPlayerIndex = d - World.Objects.Length - World.Creatures.Length;
                        ObjectIndex.Maximum = World.Players.Length - 1;
                        ObjectIndex.Value = SelectedPlayerIndex;
                    }
                    else if (d >= World.Objects.Length) //существо ближе
                    {
                        SelectedCreatureIndex = d - World.Objects.Length;
                        ObjectIndex.Maximum = World.Creatures.Length - 1;
                        ObjectIndex.Value = SelectedCreatureIndex;
                    }
                    else //объект ближе
                    {
                        SelectedObjectIndex = d;
                        ObjectIndex.Maximum = World.Objects.Length - 1;
                        ObjectIndex.Value = SelectedObjectIndex;
                    }
                    ObjectIndex.Enabled = true;
                }

                #region Перетаскивание объекта
                if (SelectedObjectIndex != -1 || SelectedCreatureIndex != -1 || SelectedPlayerIndex != -1)
                {
                    if (SelectedObjectIndex != -1)
                    {
                        if (MousePos.X > World.Objects[SelectedObjectIndex].Location.X &&
                            MousePos.X < World.Objects[SelectedObjectIndex].Location.X + World.Objects[SelectedObjectIndex].Size.Width &&
                            MousePos.Y > World.Objects[SelectedObjectIndex].Location.Y &&
                            MousePos.Y < World.Objects[SelectedObjectIndex].Location.Y + World.Objects[SelectedObjectIndex].Size.Height)
                            isObjectDraging = true;
                        label5.Text = String.Format("Координаты: {0}:{1}", World.Objects[SelectedObjectIndex].Location.X, World.Objects[SelectedObjectIndex].Location.Y);
                    }
                    else if (SelectedCreatureIndex != -1)
                    {
                        if (MousePos.X > World.Creatures[SelectedCreatureIndex].Location.X &&
                                MousePos.X < World.Creatures[SelectedCreatureIndex].Location.X + World.Creatures[SelectedCreatureIndex].Size.Width &&
                                MousePos.Y > World.Creatures[SelectedCreatureIndex].Location.Y &&
                                MousePos.Y < World.Creatures[SelectedCreatureIndex].Location.Y + World.Creatures[SelectedCreatureIndex].Size.Height)
                            isObjectDraging = true;
                        label5.Text = String.Format("Координаты: {0}:{1}", World.Creatures[SelectedCreatureIndex].Location.X, World.Creatures[SelectedCreatureIndex].Location.Y);
                    }
                    else if (SelectedPlayerIndex != -1)
                    {
                        if (MousePos.X > World.Players[SelectedPlayerIndex].Location.X &&
                            MousePos.X < World.Players[SelectedPlayerIndex].Location.X + World.Players[SelectedPlayerIndex].Size.Width &&
                            MousePos.Y > World.Players[SelectedPlayerIndex].Location.Y &&
                            MousePos.Y < World.Players[SelectedPlayerIndex].Location.Y + World.Players[SelectedPlayerIndex].Size.Height)
                            isObjectDraging = true;
                        label5.Text = String.Format("Координаты: {0}:{1}", World.Players[SelectedPlayerIndex].Location.X, World.Players[SelectedPlayerIndex].Location.Y);
                    }
                    PreviousPosition = e.Location;
                }
                #endregion
            }
            else
            {
                SelectedObjectIndex = -1;
                SelectedCreatureIndex = -1;
                SelectedPlayerIndex = -1;
                ObjectIndex.Enabled = false;
                ObjectIndex.Minimum = -1;
                ObjectIndex.Maximum = -1;
                label5.Text = String.Format("Координаты: -1:-1");
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
                    for (int i = 0; i < Creatures.Index.Length; i++)
                        Textures_List.Items.Add(Creatures.Index[i].ID);
                    for (int i = 0; i < Players.Index.Length; i++)
                        Textures_List.Items.Add(Players.Index[i].ID);
                    break;
                case "Очистить блок":
                    Brush = EditorBrush.ClearBlock;
                    Textures_List.Items.Clear();
                    break;
                case "Установить непроходимость":
                    Brush = EditorBrush.SetImpassability;
                    Textures_List.Items.Clear();
                    break;
                case "Удалить непроходимость":
                    Brush = EditorBrush.SetPassability;
                    Textures_List.Items.Clear();
                    break;
                case "Рука":
                    Brush = EditorBrush.Hand;
                    Textures_List.Items.Clear();
                    break;
            }
            try { Draw(CreateGraphics()); } catch { }
        }

        private void ObjectIndex_ValueChanged(object sender, EventArgs e)
        { 
            if (ObjectIndex.Enabled)
            {
                if (SelectedObjectIndex != -1)
                {
                    GameObject temp = World.Objects[(int)ObjectIndex.Value];
                    World.Objects[(int)ObjectIndex.Value] = World.Objects[SelectedObjectIndex].Clone();
                    World.Objects[SelectedObjectIndex] = temp.Clone();
                }
                else if (SelectedCreatureIndex != -1)
                {
                    Creature temp = World.Creatures[(int)ObjectIndex.Value];
                    World.Creatures[(int)ObjectIndex.Value] = World.Creatures[SelectedObjectIndex].Clone();
                    World.Creatures[SelectedObjectIndex] = temp.Clone();
                }
                else if (SelectedPlayerIndex != -1)
                {
                    Player temp = World.Players[(int)ObjectIndex.Value];
                    World.Players[(int)ObjectIndex.Value] = World.Players[SelectedObjectIndex].Clone();
                    World.Players[SelectedObjectIndex] = temp.Clone();
                }
            }
            Draw(CreateGraphics());
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
            SetImpassability,
            SetPassability,
            Hand
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

            if (e.KeyChar == 'o' || e.KeyChar == 'щ')
                Brush_ComboBox.SelectedItem = "Добавить объект";

            if (e.KeyChar == 'x' || e.KeyChar == 'ч')
                Brush_ComboBox.SelectedItem = "Выбрать объект";

            if (e.KeyChar == 'i' || e.KeyChar == 'ш')
                Brush_ComboBox.SelectedItem = "Установить непроходимость";

            if (e.KeyChar == 'p' || e.KeyChar == 'з')
                Brush_ComboBox.SelectedItem = "Удалить непроходимость";

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

        private void WorldEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                if (Brush == EditorBrush.SelectObject)
                {
                    if (SelectedObjectIndex != -1)
                    {
                        GameObject[] temp = new GameObject[World.Objects.Length - 1];
                        for (int i = 0; i < World.Objects.Length - 1; i++)
                        {
                            if (i < SelectedObjectIndex)
                                temp[i] = World.Objects[i].Clone();
                            else
                                temp[i] = World.Objects[i + 1].Clone();
                        }
                        World.Objects = temp;
                        SelectedObjectIndex = -1;
                        label4.Text = String.Format("Всего: {0}", World.Objects.Length + World.Creatures.Length + World.Players.Length);
                        label5.Text = String.Format("Координаты: -1:-1");
                    }
                    else
                    if (SelectedCreatureIndex != -1)
                    {
                        Creature[] temp = new Creature[World.Creatures.Length - 1];
                        for (int i = 0; i < World.Creatures.Length - 1; i++)
                        {
                            if (i < SelectedCreatureIndex)
                                temp[i] = World.Creatures[i].Clone();
                            else
                                temp[i] = World.Creatures[i + 1].Clone();
                        }
                        World.Creatures = temp;
                        SelectedCreatureIndex = -1;
                        label4.Text = String.Format("Всего: {0}", World.Objects.Length + World.Creatures.Length + World.Players.Length);
                        label5.Text = String.Format("Координаты: -1:-1");
                    }
                    else
                    if (SelectedPlayerIndex != -1)
                    {
                        Player[] temp = new Player[World.Players.Length - 1];
                        for (int i = 0; i < World.Players.Length - 1; i++)
                        {
                            if (i < SelectedPlayerIndex)
                                temp[i] = World.Players[i].Clone();
                            else
                                temp[i] = World.Players[i + 1].Clone();
                        }
                        World.Players = temp;
                        SelectedPlayerIndex = -1;
                        label4.Text = String.Format("Всего: {0}", World.Objects.Length + World.Creatures.Length + World.Players.Length);
                        label5.Text = String.Format("Координаты: -1:-1");
                    }
                }
            }
            Draw(CreateGraphics());
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
    }
}
