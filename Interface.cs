using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using GLGDIPlus;

namespace EssenceOfMagic
{
    public class InterfacePagesCollection
    {
        private InterfacePage[] _pages = new InterfacePage[0];
        public int Length
        {
            get { return _pages.Length; }
        }
        public InterfacePage this[InterfacePages Page]
        {
            get
            {
                for (int i = 0; i < _pages.Length; i++)
                    if (_pages[i].Type == Page)
                        return _pages[i];
                return null;
            }
            set
            {
                for (int i = 0; i < _pages.Length; i++)
                {
                    if (_pages[i].Type == Page)
                    {
                        _pages[i].Rect = value.Rect;
                        _pages[i].InterfaceGroups = value.InterfaceGroups;
                        return;
                    }
                }
                Array.Resize<InterfacePage>(ref _pages, _pages.Length + 1);
                _pages[_pages.Length - 1] = value;
                _pages[_pages.Length - 1].Type = Page;
            }
        }
    }
    public static class Interface
    {
        public static InterfacePages Page = InterfacePages.Game;
        public static InterfacePagesCollection Pages;
        public static void Init()
        {
            Bitmap bmp = new Bitmap(GameData.Window.Width, GameData.Window.Height);
            IMG.FromBitmap(bmp);
            bmp.Dispose();
            IMG.SetImageTiles(new System.Collections.Generic.List<RectangleF>() { new RectangleF(0, 0, GameData.Window.Width, GameData.Window.Height) });

            Pages = new InterfacePagesCollection();

            #region Game
            Pages[InterfacePages.Game] = new InterfacePage()
            {
                InterfaceGroups = new InterfaceGroup[1]
                {
                    new InterfaceGroup()
                    {
                        Rect = new Rectangle(GameData.Window.Width - 310, GameData.Window.Height - 110, 300, 100),
                        Back = (Bitmap)Image.FromFile(GameData.TextureFolder + "\\Technical\\health.png"),
                        Name = "HealthBar"
                    }
                }
            };
            #endregion

            #region Menu
            Pages[InterfacePages.Menu] = new InterfacePage()
            {
                Type = InterfacePages.Menu,
                InterfaceGroups = new InterfaceGroup[1]
                {
                    new InterfaceGroup()
                    {
                        Rect = new Rectangle(100, 100, 300, 50),
                        Back = new Bitmap(300, 50),
                        Name = "Выйти из игры"
                    }
                }
            };

            #endregion

            #region Inventory
            Pages[InterfacePages.Inventory] = new InterfacePage()
            {

            };

            #endregion

            #region Shop
            Pages[InterfacePages.Shop] = new InterfacePage()
            {

            };

            #endregion

            _ = Task.Run(() =>
            {
                DateTime last1 = DateTime.Now;
                DateTime last2 = DateTime.Now;
                int f = 0;
                while (true)
                {
                    f++;
                    
                    if (Owner != null)
                    {
                        Owner.Invoke(new GameData.ThreadTransit(() => { Interface.DrawPage(); }));
                    }

                    double ms = 1000.0 / FPSlimit;
                    TimeSpan diff = DateTime.Now - last2;
                    if (diff.TotalMilliseconds < ms)
                        Thread.Sleep((int)Math.Round(ms - diff.TotalMilliseconds, 0));
                    last2 = DateTime.Now;

                    if ((DateTime.Now - last1).TotalMilliseconds >= 1000)
                    {
                        last1 = DateTime.Now;
                        FPS = f;
                        f = 0;
                    }
                }
            });
        }
        public static int FPS = 20;
        public static int FPSlimit = 20;
        public static GLMultiImage IMG = new GLMultiImage();
        public static void DrawPage()
        {
            Bitmap Output = new Bitmap(GameData.Window.Width, GameData.Window.Height);
            using (Graphics gr1 = Graphics.FromImage(Output))
            {
                if (Page == InterfacePages.Game)
                {
                    InterfacePage page = Pages[InterfacePages.Game];
                    for (int g = 0; g < page.InterfaceGroups.Length; g++)
                    {
                        Bitmap group = new Bitmap(page.InterfaceGroups[g].Rect.Width, page.InterfaceGroups[g].Rect.Height);
                        using (Graphics gr2 = Graphics.FromImage(group))
                        {
                            gr2.DrawImage(page.InterfaceGroups[g].Back, 0, 0, group.Width, group.Height);
                            if (page.InterfaceGroups[g].Name == "HealthBar")
                            {
                                double max, reg; int barw, barh = 10;
                                #region Health bar
                                gr2.FillRectangle(Brushes.White, 50, 20, 200, barh);
                                max = GameData.World.Players[0].HPMax;
                                reg = GameData.World.Players[0].HP;
                                barw = (int)Math.Round((reg / max) * 200, 0);
                                gr2.FillRectangle(Brushes.GreenYellow, 50, 20, barw, barh);
                                #endregion

                                #region Food bar
                                gr2.FillRectangle(Brushes.White, 50, 45, 200, barh);
                                max = GameData.World.Players[0].SatietyMax;
                                reg = GameData.World.Players[0].Satiety;
                                barw = (int)Math.Round((reg / max) * 200, 0);
                                gr2.FillRectangle(Brushes.Yellow, 50, 45, barw, barh);
                                #endregion

                                #region Water bar
                                gr2.FillRectangle(Brushes.White, 50, 70, 200, barh);
                                max = GameData.World.Players[0].WaterMax;
                                reg = GameData.World.Players[0].Water;
                                barw = (int)Math.Round((reg / max) * 200, 0);
                                gr2.FillRectangle(Brushes.Blue, 50, 70, barw, barh);
                                #endregion
                            }
                            else
                            if (page.InterfaceGroups[g].Name == "Hint")
                            {

                            }
                        }
                        gr1.DrawImage(group, page.InterfaceGroups[g].Rect);
                    }
                }
                else
                if (Page == InterfacePages.Menu)
                {
                    gr1.FillRectangle(new SolidBrush(Color.FromArgb(127, 127, 127, 255)), 0, 0, Output.Width, Output.Height);
                    InterfacePage page = Pages[InterfacePages.Game];
                    for (int g = 0; g < page.InterfaceGroups.Length; g++)
                    {
                        Bitmap group = new Bitmap(page.InterfaceGroups[g].Rect.Width, page.InterfaceGroups[g].Rect.Height);
                        using (Graphics gr2 = Graphics.FromImage(group))
                        {
                            if (GameData.Cursor.X > page.InterfaceGroups[g].Rect.Left &&
                                GameData.Cursor.X < page.InterfaceGroups[g].Rect.Right &&
                                GameData.Cursor.Y > page.InterfaceGroups[g].Rect.Top &&
                                GameData.Cursor.Y < page.InterfaceGroups[g].Rect.Bottom)
                                gr2.FillEllipse(new SolidBrush(Color.FromArgb(127, 0, 0, 127)), 0, 0, group.Width, group.Height);
                            //System.Windows.Forms()
                        }
                        gr1.DrawImage(group, page.InterfaceGroups[g].Rect);
                    }
                }
            }
            IMG.Free();
            IMG.FromBitmap(Output);
        }
        public static Game Owner;
    }

    public class InterfacePage
    {
        public InterfaceGroup[] InterfaceGroups { get; set; }
        private Rectangle _rect = new Rectangle(0, 0, GameData.Window.Width, GameData.Window.Height);
        public Rectangle Rect
        {
            get { return _rect; }
            set { _rect = new Rectangle(value.X, value.Y, GameData.Window.Width, GameData.Window.Height); }
        }
        public InterfacePages Type { get; set; }
    }

    public class InterfaceGroup
    {
        public InterfaceCell[] Cells { get; set; }
        public Rectangle Rect { get; set; }
        public Bitmap Back { get; set; }
        public string Name { get; set; }
    }

    public class InterfaceCell
    {
        public Item Item { get; set; }
        public Rectangle Rect { get; set; }
    }
}
