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
    public static class Interface
    {
        public static InterfacePages Page = InterfacePages.Game;
        public static InterfacePage[] Pages;
        public static void Init()
        {
            Bitmap bmp = new Bitmap(GameData.Window.Width, GameData.Window.Height);
            IMG.FromBitmap(bmp);
            bmp.Dispose();
            IMG.SetImageTiles(new System.Collections.Generic.List<RectangleF>() { new RectangleF(0, 0, GameData.Window.Width, GameData.Window.Height) });

            Pages = new InterfacePage[4];

            #region Game
            Pages[0] = new InterfacePage();
            Pages[0].Type = InterfacePages.Game;
            Pages[0].InterfaceGroups = new InterfaceGroup[1]
            {
                new InterfaceGroup()
                {
                    Rect = new Rectangle(InterfacePage.Size.Width - 310, InterfacePage.Size.Height - 110, 300, 100),
                    Back = (Bitmap)Image.FromFile(GameData.TextureFolder + "\\Technical\\health.png")
                }
            };
            #endregion

            #region Menu
            Pages[1] = new InterfacePage();
            Pages[1].Type = InterfacePages.Menu;

            #endregion

            #region Inventory
            Pages[2] = new InterfacePage();
            Pages[2].Type = InterfacePages.Inventory;

            #endregion

            #region Shop
            Pages[3] = new InterfacePage();
            Pages[3].Type = InterfacePages.Shop;

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
                        Owner.Invoke(new ThreadTransit(() => { DrawPage(); }));
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
                InterfacePage temp = new InterfacePage();
                for (int i = 0; i < Pages.Length; i++) if (Pages[i].Type == Page) temp = Pages[i];

                gr1.DrawImage(temp.GetBMP(), 0, 0, Output.Width, Output.Height);
            }
            IMG.Free();
            IMG.FromBitmap(Output);
        }
        public static Game Owner;
        public delegate void ThreadTransit();
    }

    public class InterfacePage
    {
        public static Size Size = new Size(GameData.Window.Width, GameData.Window.Height);
        public InterfaceGroup[] InterfaceGroups { get; set; }
        private Rectangle _rect = new Rectangle(0, 0, Size.Width, Size.Height);
        public Rectangle Rect
        {
            get { return _rect; }
            set { _rect = new Rectangle(value.X, value.Y, Size.Width, Size.Height); }
        }
        public InterfacePages Type { get; set; }
        public Bitmap GetBMP()
        {
            Bitmap buffer1 = new Bitmap(Size.Width, Size.Height);
            using (Graphics gr1 = Graphics.FromImage(buffer1))
            {
                for (int i = 0; i < InterfaceGroups.Length; i++)
                {
                    gr1.DrawImage(InterfaceGroups[i].Back, InterfaceGroups[i].Rect);
                }
            }
            return buffer1;
        }
    }

    public class InterfaceGroup
    {
        public Rectangle Rect { get; set; }
        public Bitmap Back { get; set; }
    }

    public class InterfaceCell
    {
        public Item Item { get; set; }
        public Rectangle Rect { get; set; }
    }

    public enum InterfacePages
    {
        Game,
        Menu,
        Inventory,
        Shop
    }
}
