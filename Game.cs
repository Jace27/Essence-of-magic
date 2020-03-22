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

            graphicSurface1.OnLoad += GraphicSurface1_OnLoad;
        }

        private void GraphicSurface1_OnLoad()
        {
            Textures.Init();
            Animations.Init();
            GameObjects.Init();
            Creatures.Init();
            Players.Init();
            Interface.Owner = this;
            Interface.Init();

            using (StreamReader sr = new StreamReader(GameData.WorldFolder + "\\world.json"))
                GameData.World = JsonSerializer.Deserialize<World>(sr.ReadToEnd());

            Player = GameData.World.Players[0];
            Player.Animations = new Animations();
            Animation calm = new Animation();
            calm.SpriteFile = "player.calm.gif";
            calm.ID = "player.calm";
            calm.Init();
            Player.Animations.Add("player.calm", calm);
            Player.BeginAnim("player.calm", true);
            Player.OnMove += Player_OnMove;
            Camera = new Camera() { Location = new Location(Player.Location.X + GameData.BlockSize.Width / 2, Player.Location.Y + GameData.BlockSize.Height / 2, 0) };

            GameTime.Start();

            isInitialized = true;
        }

        bool isInitialized = false;

        private void Player_OnMove(Vector Vector)
        {
            Camera.Move(Vector);
            if (Camera.Location.X != Player.Location.X + GameData.BlockSize.Width / 2 ||
                Camera.Location.Y != Player.Location.Y + GameData.BlockSize.Height / 2)
            {
                Camera.Location = new Location(Player.Location.X + GameData.BlockSize.Width / 2, Player.Location.Y + GameData.BlockSize.Height / 2, Player.Location.Z);
            }
        }

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
            GameObject[] OCP = new GameObject[GameData.World.Objects.Length + GameData.World.Creatures.Length + GameData.World.Players.Length];
            int ind = 0;
            for (int i = 0; i < GameData.World.Objects.Length; i++)
            {
                OCP[ind] = GameData.World.Objects[i];
                ind++;
            }
            for (int i = 0; i < GameData.World.Creatures.Length; i++)
            {
                OCP[ind] = GameData.World.Creatures[i];
                ind++;
            }
            for (int i = 0; i < GameData.World.Players.Length; i++)
            {
                OCP[ind] = GameData.World.Players[i];
                ind++;
            }

            for (int c = 0; c < OCP.Length; c++)
            {
                Point ol = new Point(
                    Convert.ToInt32(
                        WorldToWindow.X + OCP[c].Location.X
                    ),
                    Convert.ToInt32(
                        WorldToWindow.Y + OCP[c].Location.Y - OCP[c].Location.Z
                    )
                );

                int cl = UT.Length;

                Array.Resize<UsingTexture>(ref UT, UT.Length + 1);

                UT[cl].tID = OCP[c].ID;
                UT[cl].Rect = new List<RectangleF>();
                UT[cl].Rect.Add(new RectangleF(ol.X, ol.Y, OCP[c].Size.Width, OCP[c].Size.Height));
                if (OCP[c].CurrentAnim != null)
                    UT[cl].IMG = OCP[c].CurrentAnim.Sprites[OCP[c].CurrentAnim.CurrentFrame];
                else
                    UT[cl].IMG = OCP[c].Sprite.GetIMG();
            }
            #endregion

            for (int i = 0; i < UT.Length; i++)
            {
                try 
                {
                    UT[i].IMG.SetImageTiles(UT[i].Rect); 
                    e.DrawMultiImage(UT[i].IMG);
                }
                catch { 
                    Invalidate(); 
                }
            }

            e.FillRectangle(Color.FromArgb(191, 63, 63, 63), new Rectangle(0, 0, 45, 30));
            e.DrawString(graphicSurface1.FPS.ToString(), new Font("Arial", 12f), Color.White, 5, 5);
            e.FillRectangle(Color.FromArgb(191, 63, 63, 63), new Rectangle(0, 30, 90, 60));
            AddedInfo = Interface.FPS.ToString() + "\n" + (GC.GetTotalMemory(false) / 1024 / 1024) + "МБ";
            e.DrawString(AddedInfo, new Font("Arial", 12f), Color.White, 5, 35);

            e.DrawMultiImage(Interface.IMG);
        }

        bool A = false;
        bool S = false;
        bool D = false;
        bool W = false;
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
                    
                    Task.Run(() => { Player.Move(direction); });
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
        }

        private void Game_MouseMove(object sender, MouseEventArgs e)
        {
            GameData.Cursor = e.Location;
        }

        private void GraphicSurface1_MouseClick(object sender, MouseEventArgs e)
        {
            Interface.Click(e.Location);
        }
    }
}
