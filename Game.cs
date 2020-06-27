using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GLGDIPlus;

namespace EssenceOfMagic
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();

            GameData.CheckFileStruct();

            GameData.Game = this;
            GameData.isInGame = true;

            graphicSurface1.OnLoad += GraphicSurface1_OnLoad;
        }

        private void Game_MouseDown(object sender, MouseEventArgs e)
        {
            Interface.Game_MouseDown(e);
        }

        private void Game_MouseUp(object sender, MouseEventArgs e)
        {
            Interface.Game_MouseUp(e);
        }

        private void GraphicSurface1_OnLoad()
        {
            Textures.Init();
            Animations.Init();
            GameObjects.Init();
            Creatures.Init();
            Players.Init();
            Items.Init();

            if (!GameData.isLoadingGame)
                using (StreamReader sr = new StreamReader(GameData.WorldFolder + "\\world.json"))
                    GameData.World = JsonSerializer.Deserialize<World>(sr.ReadToEnd());
            GameData.RegenGID();
            Player = GameData.World.Objects.players[0];
            Interface.Owner = this;
            Interface.InventoryCellsCount = Player.Inventory.CountBackpack;
            Interface.ShopCellsCount = Creatures.Get("shop").Inventory.CountBackpack;
            Interface.Init();


            Player.Animations = new Animations();
            Animation calm = new Animation();
            calm.SpriteFile = "player.calm.gif";
            calm.ID = "player.calm";
            calm.Init();
            Player.Animations.Add("player.calm", calm);
            Player.BeginAnim("player.calm", true);

            if (!GameData.isLoadingGame) Camera = new Camera();
            Camera.Link(Player);

            GameTime.Start();

            _ = Task.Run(() =>
            {
                GLGraphics g = new GLGraphics();
                while (true) graphicSurface1.Invoke(new GraphicSurface.ThreadTransit(graphicSurface1.Draw), g);
            });

            graphicSurface1.MouseDown += Game_MouseDown;
            graphicSurface1.MouseUp += Game_MouseUp;

            GameData.TitleScreen.Hide();

            isInitialized = true;
        }

        bool isInitialized = false;

        public struct UsingTexture
        {
            /// <summary>
            /// Строковые ID используемых текстур
            /// </summary>
            public string tID { get; set; }

            /// <summary>
            /// Массив перечислений ректанглов. Каждый элемент массива (каждое перечисление) соответствует используемой текстуре
            /// </summary>
            public List<RectangleF> Rect { get; set; }

            /// <summary>
            /// Мульти-изображения для OpenGL. На вход берут соответствующие по индексу перечисления из Rects
            /// </summary>
            public GLMultiImage IMG { get; set; }
        }

        /// <summary>
        /// Временный массив выводимых объектов. Сбрасывается при каждой отрисовке!
        /// </summary>
        UsingTexture[] UT = new UsingTexture[0];

        /// <summary>
        /// Ректангл окна относительно мира (в пикселях)
        /// </summary>
        Rectangle WindowToWorld;

        /// <summary>
        /// Ректангл мира относительно окна (в пикселях)
        /// </summary>
        Rectangle WorldToWindow;

        /// <summary>
        /// Отрисовываемая часть мира (в блоках)
        /// </summary>
        Rectangle ProcessingField;

        string AddedInfo = "";
        private void GraphicSurface1_OnDraw(GLGraphics e)
        {
            if (!isInitialized) return;

            UT = new UsingTexture[0];

            WindowToWorld = new Rectangle(
                (int)Math.Round(Camera.Location.X - this.Width / 2.0, 0), 
                (int)Math.Round(Camera.Location.Y - this.Height / 2.0, 0),
                this.Width,
                this.Height
            );
            WorldToWindow = new Rectangle(
                (int)Math.Round(WindowToWorld.X * -1.0, 0),
                (int)Math.Round(WindowToWorld.Y * -1.0, 0),
                GameData.World.Ground.Length * GameData.BlockSize.Width,
                GameData.World.Ground[0].Length * GameData.BlockSize.Height
            );
            ProcessingField = new Rectangle(
                WindowToWorld.X / GameData.BlockSize.Width,
                WindowToWorld.Y / GameData.BlockSize.Height,
                this.Width / GameData.BlockSize.Width + 1,
                this.Height / GameData.BlockSize.Height + 1
            );

            #region Слои поверхности
            int lm = GameData.World.Ground[0][0].Layers.Length;
            for (int l = 0; l < lm; l++)
            {
                int xz = 0; int yz = 0;
                for (int x = ProcessingField.Left; x < ProcessingField.Right; x++)
                {
                    for (int y = ProcessingField.Top; y < ProcessingField.Bottom; y++)
                    {
                        if (x >= 0 && y >= 0 && 
                            x < GameData.World.Ground.Length && 
                            y < GameData.World.Ground[x].Length)
                            if (GameData.World.Ground[x][y].Layers.Length > lm) lm = GameData.World.Ground[x][y].Layers.Length;

                        string curID;
                        if (x < 0 || y < 0 || 
                            x >= GameData.World.Ground.Length || 
                            y >= GameData.World.Ground[x].Length || 
                            l >= GameData.World.Ground[x][y].Layers.Length)
                            curID = "empty";
                        else
                            curID = GameData.World.Ground[x][y].Layers[l];

                        Texture curTexture = Textures.Get(curID);

                        int i = -1;
                        for (int id = 0; id < UT.Length; id++)
                            if (UT[id].tID == curID)
                                i = id;

                        if (i == -1)
                        {
                            i = UT.Length;

                            Array.Resize<UsingTexture>(ref UT, UT.Length + 1);

                            UT[i].tID = curID;
                            UT[i].Rect = new List<RectangleF>();
                        }

                        UT[i].Rect.Add(new RectangleF(WorldToWindow.X + x * GameData.BlockSize.Width, WorldToWindow.Y + y * GameData.BlockSize.Height, GameData.BlockSize.Width, GameData.BlockSize.Height));
                        UT[i].IMG = curTexture.GetIMG();

                        yz++;
                    }
                    xz++;
                    yz = 0;
                }
            }
            #endregion

            #region Объекты
            int t = 0;
            Rectangle[] Hitboxes = new Rectangle[GameData.World.Objects.CountAll];
            for (int i = 0; i < GameData.World.Objects.CountAll; i++)
            {
                GameObject temp = GameData.World.Objects.Get(i);

                Point ol = new Point(
                    Convert.ToInt32(
                        WorldToWindow.X + temp.Location.X
                    ),
                    Convert.ToInt32(
                        WorldToWindow.Y + temp.Location.Y - temp.Location.Z
                    )
                );

                int cl = UT.Length;

                Array.Resize<UsingTexture>(ref UT, UT.Length + 1);

                UT[cl].tID = temp.ID;
                UT[cl].Rect = new List<RectangleF>();
                UT[cl].Rect.Add(new RectangleF(ol.X, ol.Y, temp.Size.Width, temp.Size.Height));
                if (temp.CurrentAnim != null)
                    UT[cl].IMG = temp.CurrentAnim.Sprites[temp.CurrentAnim.CurrentFrame];
                else
                    UT[cl].IMG = temp.Sprite.GetIMG();

                Bitmap texture;
                if (temp.CurrentAnim != null)
                    texture = temp.CurrentAnim.Sprite.bitmap;
                else if (temp.Sprite != null)
                    texture = temp.Sprite.IMG.bitmap;
                else
                    texture = new Bitmap(Size.Width, Size.Height);
                if (temp.Hitbox != null)
                    Hitboxes[t] = GameData.HitboxConvert(temp.Location, temp.Size, texture.Size, temp.Hitbox);
                t++;
            }
            #endregion

            for (int i = 0; i < UT.Length; i++)
            {
                try 
                {
                    UT[i].IMG.SetImageTiles(UT[i].Rect); 
                    e.DrawMultiImage(UT[i].IMG);
                }
                catch (Exception ex)
                { 
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
                    graphicSurface1.Invalidate();
                }
            }

            if (Settings.ShowHitboxes)
                for (int i = 0; i < Hitboxes.Length; i++)
                    if (Hitboxes[i] != null)
                        e.DrawRectangle(Color.FromArgb(92, 255, 255, 0), 
                            new Rectangle(
                                Hitboxes[i].X - WindowToWorld.X,
                                Hitboxes[i].Y - WindowToWorld.Y,
                                Hitboxes[i].Width,
                                Hitboxes[i].Height
                            )
                        );

            if (Settings.ShowDebugInformation)
            {
                e.FillRectangle(Color.FromArgb(191, 63, 63, 63), new Rectangle(0, 0, 135, 90));
                e.DrawString(graphicSurface1.FPS.ToString() + " FPS OpenGL", new Font("Arial", 12f), Color.White, 5, 5);
                AddedInfo = Interface.FPS.ToString() + " FPS GDI\n" + (GC.GetTotalMemory(false) / 1024 / 1024) + " МБ GC";
                if (GameTime.isFreezed)
                    AddedInfo += "\n Game freezed";
                e.DrawString(AddedInfo, new Font("Arial", 12f), Color.White, 5, 35);
            }

            if (Settings.ShowInterface) e.DrawMultiImage(Interface.IMG);
        }

        public static bool A = false;
        public static bool S = false;
        public static bool D = false;
        public static bool W = false;
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            AddedInfo = e.KeyCode.ToString();
            if (Interface.Page == InterfacePages.Game && !GameTime.isFreezed)
            {
                if (!e.Control && !e.Alt && !e.Shift)
                {
                    Direction direction;
                    if (e.KeyCode == Keys.W || e.KeyCode.ToString() == "Up")
                    {
                        direction = Direction.Up;
                        W = true;
                    }
                    else if (e.KeyCode == Keys.S || e.KeyCode.ToString() == "Down")
                    {
                        direction = Direction.Down;
                        S = true;
                    }
                    else if (e.KeyCode == Keys.A || e.KeyCode.ToString() == "Left")
                    {
                        direction = Direction.Left;
                        A = true;
                    }
                    else if (e.KeyCode == Keys.D || e.KeyCode.ToString() == "Right")
                    {
                        direction = Direction.Right;
                        D = true;
                    }
                    else if (e.KeyCode == Keys.Space)
                        direction = Direction.Ceiling;
                    else
                        direction = Direction.Null;
                    if (W && A) direction = Direction.UpLeft;
                    else if (W && D) direction = Direction.UpRight;
                    else if (S && A) direction = Direction.DownLeft;
                    else if (S && D) direction = Direction.DownRight;

                    _ = Task.Run(() =>
                    {
                        Player.Move(direction);
                    });
                }

            }
        }

        private void Game_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            AddedInfo = "";
                 if (e.KeyCode == Keys.W || e.KeyCode.ToString() == "Up") W = false;
            else if (e.KeyCode == Keys.S || e.KeyCode.ToString() == "Down") S = false;
            else if (e.KeyCode == Keys.A || e.KeyCode.ToString() == "Left") A = false;
            else if (e.KeyCode == Keys.D || e.KeyCode.ToString() == "Right") D = false;

            if (e.KeyCode == Keys.Escape && Interface.Page != InterfacePages.Game && Interface.Question == null)
            {
                Interface.Page = InterfacePages.Game;
                GameTime.isFreezed = false;
            }
            else
            if (e.KeyCode == Keys.Escape && Interface.Page == InterfacePages.Game && Interface.Question == null)
            {
                Interface.Page = InterfacePages.Menu;
                GameTime.isFreezed = true;
            }
            else
            if (e.KeyCode == Keys.E)
            {
                if (Interface.Page == InterfacePages.Game)
                {
                    Interface.Page = InterfacePages.Inventory;
                    GameTime.isFreezed = true;
                }
                else if (Interface.Page == InterfacePages.Inventory)
                {
                    Interface.Page = InterfacePages.Game;
                    GameTime.isFreezed = false;
                }
            }
        }

        private void Game_MouseMove(object sender, MouseEventArgs e)
        {
            GameData.Cursor = e.Location;
            Interface.MouseMove(e.Location);
            GameData.World.MouseMove(new Point(e.X + WindowToWorld.X, e.Y + WindowToWorld.Y));
        }

        private void GraphicSurface1_MouseClick(object sender, MouseEventArgs e)
        {
            Interface.Click(e.Location, e.Button);
            GameData.World.Click(new Point(e.X + WindowToWorld.X, e.Y + WindowToWorld.Y), e.Button);
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
