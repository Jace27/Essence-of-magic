using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
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
        public static InterfaceGroup Question;
        public static InterfaceCaption[] Captions;
        #region Question templates
        public static void Question_ExitSaveGame()
        {
            Question = new InterfaceGroup();
            {
                Question.Rect = new Rectangle((GameData.Window.Width - 300) / 2, (GameData.Window.Height - 200) / 2, 300, 200);
                Question.Back = new Bitmap(300, 200);
                Question.Captions = new InterfaceCaption[4];
                {
                    Question.Captions[0] = new InterfaceCaption()
                    {
                        Text = "Сохранить игру?",
                        HoverColor = Color.White,
                        Rect = new Rectangle((Question.Rect.Width - 200) / 2, (Question.Rect.Height - 30) / 2 - 50, 200, 30)
                    };
                    Question.Captions[1] = new InterfaceCaption()
                    {
                        Text = "Да",
                        Rect = new Rectangle((Question.Rect.Width - 75) / 2 - 75, (Question.Rect.Height - 30) / 2 + 50, 75, 30),
                        Action = () =>
                        {
                            GameData.Save();
                            Question = null;
                            Application.Exit();
                        }
                    };
                    Question.Captions[2] = new InterfaceCaption()
                    {
                        Text = "Нет",
                        Rect = new Rectangle((Question.Rect.Width - 75) / 2, (Question.Rect.Height - 30) / 2 + 50, 75, 30),
                        Action = () =>
                        {
                            Question = null;
                            Application.Exit();
                        }
                    };
                    Question.Captions[3] = new InterfaceCaption()
                    {
                        Text = "Отмена",
                        Rect = new Rectangle((Question.Rect.Width - 75) / 2 + 75, (Question.Rect.Height - 30) / 2 + 50, 100, 30),
                        Action = () =>
                        {
                            Question = null;
                        }
                    };
                }
            }
        }
        public static void Question_LoadSaveGame()
        {
            Question = new InterfaceGroup();
            {
                Question.Rect = new Rectangle((GameData.Window.Width - 300) / 2, (GameData.Window.Height - 200) / 2, 300, 200);
                Question.Back = new Bitmap(300, 200);
                Question.Captions = new InterfaceCaption[4];
                {
                    Question.Captions[0] = new InterfaceCaption()
                    {
                        Text = "Сохранить игру?",
                        HoverColor = Color.White,
                        Rect = new Rectangle((Question.Rect.Width - 200) / 2, (Question.Rect.Height - 30) / 2 - 50, 200, 30)
                    };
                    Question.Captions[1] = new InterfaceCaption()
                    {
                        Text = "Да",
                        Rect = new Rectangle((Question.Rect.Width - 75) / 2 - 75, (Question.Rect.Height - 30) / 2 + 50, 75, 30),
                        Action = () =>
                        {
                            GameData.Save();
                            GameData.Load();
                            Question = null;
                        }
                    };
                    Question.Captions[2] = new InterfaceCaption()
                    {
                        Text = "Нет",
                        Rect = new Rectangle((Question.Rect.Width - 75) / 2, (Question.Rect.Height - 30) / 2 + 50, 75, 30),
                        Action = () =>
                        {
                            GameData.Load();
                            Question = null;
                        }
                    };
                    Question.Captions[3] = new InterfaceCaption()
                    {
                        Text = "Отмена",
                        Rect = new Rectangle((Question.Rect.Width - 75) / 2 + 75, (Question.Rect.Height - 30) / 2 + 50, 100, 30),
                        Action = () =>
                        {
                            Question = null;
                        }
                    };
                }
            }
        }
        #endregion
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
                        ID = "HealthBar"
                    }
                }
            };
            #endregion

            #region Menu
            Pages[InterfacePages.Menu] = new InterfacePage()
            {
                Type = InterfacePages.Menu,
                InterfaceGroups = new InterfaceGroup[4]
                {
                    new InterfaceGroup()
                    {
                        Rect = new Rectangle(100, 100 + 50 * 0, 300, 50),
                        Back = new Bitmap(300, 50),
                        Captions = new InterfaceCaption[1]
                        {
                            new InterfaceCaption()
                            {
                                Text = "Сохранить игру",
                                Rect = new Rectangle(0, 0, 300, 50),
                                Action = () => { GameData.Save(); }
                            }
                        }
                    },
                    new InterfaceGroup()
                    {
                        Rect = new Rectangle(100, 100 + 50 * 1, 300, 50),
                        Back = new Bitmap(300, 50),
                        Captions = new InterfaceCaption[1]
                        {
                            new InterfaceCaption()
                            {
                                Text = "Загрузить игру",
                                Rect = new Rectangle(0, 0, 300, 50),
                                Action = () =>
                                {
                                    Interface.Question_LoadSaveGame();
                                }
                            }
                        }
                    },
                    new InterfaceGroup()
                    {
                        Rect = new Rectangle(100, 100 + 50 * 2, 300, 50),
                        Back = new Bitmap(300, 50),
                        Captions = new InterfaceCaption[1]
                        {
                            new InterfaceCaption()
                            {
                                Text = "Настройки",
                                Rect = new Rectangle(0, 0, 300, 50)
                            }
                        }
                    },
                    new InterfaceGroup()
                    {
                        Rect = new Rectangle(100, 100 + 50 * 3, 300, 50),
                        Back = new Bitmap(300, 50),
                        Captions = new InterfaceCaption[1]
                        {
                            new InterfaceCaption()
                            {
                                Text = "Выйти из игры",
                                Rect = new Rectangle(0, 0, 300, 50),
                                Action = () =>
                                {
                                    Interface.Question_ExitSaveGame();
                                }
                            }
                        }
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
                        Owner.Invoke(new GameData.ThreadTransit(() => { try { Interface.DrawPage(); } catch { } }));
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
                gr1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                if (Page == InterfacePages.Game)
                {
                    InterfacePage page = Pages[InterfacePages.Game];
                    for (int g = 0; g < page.InterfaceGroups.Length; g++)
                    {
                        InterfaceGroup gr = page.InterfaceGroups[g];
                        Bitmap group = new Bitmap(gr.Rect.Width, gr.Rect.Height);
                        using (Graphics gr2 = Graphics.FromImage(group))
                        {
                            gr2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                            gr2.DrawImage(gr.Back, 0, 0, group.Width, group.Height);
                            if (gr.ID == "HealthBar")
                            {
                                double max, reg; int barw, barh = 10;
                                #region Health bar
                                gr2.DrawImage(Image.FromFile(GameData.TextureFolder + "\\Technical\\hp.png"), 260, 15, 20, 20);
                                gr2.FillRectangle(Brushes.White, 50, 20, 200, barh);
                                max = GameData.World.Players[0].HPMax;
                                reg = GameData.World.Players[0].HP;
                                barw = (int)Math.Round((reg / max) * 200, 0);
                                gr2.FillRectangle(Brushes.GreenYellow, 50, 20, barw, barh);
                                #endregion

                                #region Food bar
                                gr2.DrawImage(Image.FromFile(GameData.TextureFolder + "\\Technical\\satiety.png"), 260, 40, 20, 20);
                                gr2.FillRectangle(Brushes.White, 50, 45, 200, barh);
                                max = GameData.World.Players[0].SatietyMax;
                                reg = GameData.World.Players[0].Satiety;
                                barw = (int)Math.Round((reg / max) * 200, 0);
                                gr2.FillRectangle(Brushes.Yellow, 50, 45, barw, barh);
                                #endregion

                                #region Water bar
                                gr2.DrawImage(Image.FromFile(GameData.TextureFolder + "\\Technical\\water.png"), 260, 65, 20, 20);
                                gr2.FillRectangle(Brushes.White, 50, 70, 200, barh);
                                max = GameData.World.Players[0].WaterMax;
                                reg = GameData.World.Players[0].Water;
                                barw = (int)Math.Round((reg / max) * 200, 0);
                                gr2.FillRectangle(Brushes.Blue, 50, 70, barw, barh);
                                #endregion
                            }
                            else
                            if (gr.ID == "Hint")
                            {
                                gr2.DrawImage(gr.Back, gr.Rect);
                                for (int c = 0; c < gr.Captions.Length; c++)
                                {
                                    TextRenderer.DrawText(
                                        gr2, 
                                        gr.Captions[c].Text, 
                                        gr.Captions[c].Font, 
                                        gr.Captions[c].Rect, 
                                        gr.Captions[c].Color, 
                                        gr.Captions[c].Flags);
                                }
                            }

                            if (page.InterfaceGroups[g].Captions != null)
                            {
                                for (int c = 0; c < page.InterfaceGroups[g].Captions.Length; c++)
                                {
                                    InterfaceCaption cap = page.InterfaceGroups[g].Captions[c];
                                    if (!cap.Hovered || cap.Action == null)
                                        TextRenderer.DrawText(gr2, cap.Text, cap.Font, cap.Rect, cap.Color, cap.Flags);
                                    else
                                        TextRenderer.DrawText(gr2, cap.Text, cap.Font, cap.Rect, cap.HoverColor, cap.Flags);
                                }
                            }
                        }
                        gr1.DrawImage(group, page.InterfaceGroups[g].Rect);
                        group.Dispose();
                        group = null;
                    }
                }
                else
                if (Page == InterfacePages.Menu)
                {
                    gr1.FillRectangle(new SolidBrush(Color.FromArgb(192, 32, 32, 32)), 0, 0, Output.Width, Output.Height);
                    InterfacePage page = Pages[InterfacePages.Menu];
                    for (int g = 0; g < page.InterfaceGroups.Length; g++)
                    {
                        InterfaceGroup gr = page.InterfaceGroups[g];
                        if (gr.Captions != null)
                        {
                            Bitmap group = new Bitmap(gr.Rect.Width, gr.Rect.Height);
                            using (Graphics gr2 = Graphics.FromImage(group))
                            {
                                for (int c = 0; c < gr.Captions.Length; c++)
                                {
                                    if (gr.Captions[c].Hovered && gr.Captions[c].Action != null)
                                        TextRenderer.DrawText(gr2, gr.Captions[c].Text, gr.Captions[c].Font, gr.Captions[c].Rect, gr.Captions[c].HoverColor, gr.Captions[c].Flags);
                                    else
                                        TextRenderer.DrawText(gr2, gr.Captions[c].Text, gr.Captions[c].Font, gr.Captions[c].Rect, gr.Captions[c].Color, gr.Captions[c].Flags);
                                }
                            }
                            gr1.DrawImage(group, page.InterfaceGroups[g].Rect);
                            group.Dispose();
                        }
                    }
                }

                if (Question != null)
                {
                    Bitmap question = new Bitmap(Question.Rect.Width, Question.Rect.Height);
                    using (Graphics gr2 = Graphics.FromImage(question))
                    {
                        gr2.Clear(Color.FromArgb(50, 50, 100));
                        gr2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        if (Question.Captions != null)
                        {
                            for (int c = 0; c < Question.Captions.Length; c++)
                            {
                                InterfaceCaption cap = Question.Captions[c];
                                if (cap.Hovered && cap.Action != null)
                                    TextRenderer.DrawText(gr2, cap.Text, cap.Font, cap.Rect, cap.HoverColor, cap.Flags);
                                else
                                    TextRenderer.DrawText(gr2, cap.Text, cap.Font, cap.Rect, cap.Color, cap.Flags);
                            }
                        }
                    }
                    gr1.DrawImage(question, Question.Rect);
                    question.Dispose();
                }

                if (Captions != null)
                {
                    for (int c = 0; c < Captions.Length; c++)
                    {
                        InterfaceCaption cap = Captions[c];
                        TextRenderer.DrawText(gr1, cap.Text, cap.Font, cap.Rect, cap.Color, cap.Flags);
                    }
                }
            }
            IMG.Free();
            IMG.FromBitmap(Output);
            Output.Dispose();
        }
        public static void Click(Point e)
        {
            InterfacePage page = Pages[Interface.Page];
            for (int g = 0; g < page.InterfaceGroups.Length; g++)
            {
                if (e.X > page.InterfaceGroups[g].Rect.Left &&
                    e.X < page.InterfaceGroups[g].Rect.Right &&
                    e.Y > page.InterfaceGroups[g].Rect.Top &&
                    e.Y < page.InterfaceGroups[g].Rect.Bottom)
                {
                    page.InterfaceGroups[g].Click(new Point(e.X - page.InterfaceGroups[g].Rect.Left, e.Y - page.InterfaceGroups[g].Rect.Top));
                }
            }
            if (Question != null)
            {
                if (e.X > Question.Rect.Left &&
                    e.X < Question.Rect.Right &&
                    e.Y > Question.Rect.Top &&
                    e.Y < Question.Rect.Bottom)
                {
                    Question.Click(new Point(e.X - Question.Rect.Left, e.Y - Question.Rect.Top));
                }
            }
        }
        public static void MouseMove(Point e)
        {
            InterfacePage page = Pages[Interface.Page];
            for (int g = 0; g < page.InterfaceGroups.Length; g++)
            {
                page.InterfaceGroups[g].MouseMove(new Point(e.X - page.InterfaceGroups[g].Rect.Left, e.Y - page.InterfaceGroups[g].Rect.Top));
            }
            if (Question != null)
            {
                Question.MouseMove(new Point(e.X - Question.Rect.Left, e.Y - Question.Rect.Top));
            }
        }
        public static void Hint(string Header, string Text)
        {
            if (Page != InterfacePages.Menu)
            {
                GameTime.isFreezed = true;
                InterfacePage page = Pages[Page];
                for (int i = 0; i < page.InterfaceGroups.Length; i++)
                    if (page.InterfaceGroups[i].ID == "Hint")
                        return;
                InterfaceGroup[] newgroups = new InterfaceGroup[page.InterfaceGroups.Length + 1];
                for (int i = 0; i < page.InterfaceGroups.Length; i++)
                    newgroups[i] = page.InterfaceGroups[i];
                newgroups[newgroups.Length - 1] = new InterfaceGroup()
                {
                    ID = "Hint",
                    Back = (Bitmap)Image.FromFile(GameData.TextureFolder + "\\Technical\\hint.png"),
                    Captions = new InterfaceCaption[2]
                    {
                        new InterfaceCaption()
                        {
                            Text = Header,
                            Rect = new Rectangle(60, 30, HintRectangle.Width - 120, 40),
                            Font = new Font(GameData.Font.FontFamily, 24f, FontStyle.Bold),
                            Color = Color.Black
                        },
                        new InterfaceCaption()
                        {
                            Text = Text,
                            Rect = new Rectangle(60, 70, HintRectangle.Width - 120, HintRectangle.Height - 80),
                            Color = Color.FromArgb(24,24,24),
                            Flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak
                        }
                    },
                    Action = () =>
                    {
                        GameTime.isFreezed = false;
                        InterfaceGroup[] newgr = new InterfaceGroup[page.InterfaceGroups.Length - 1];
                        int i1 = 0;
                        for (int i2 = 0; i2 < page.InterfaceGroups.Length; i2++)
                        {
                            if (page.InterfaceGroups[i2].ID != "Hint")
                            {
                                newgr[i1] = page.InterfaceGroups[i2];
                                i1++;
                            }
                        }
                        page.InterfaceGroups = newgr;
                    },
                    Rect = HintRectangle
                };
                page.InterfaceGroups = newgroups;
            }
        }
        public static Game Owner;

        public static Item DraggingItem;
        public static InterfaceCell StartCell;
        public static InterfaceCell EndCell;

        public static Rectangle HintRectangle = new Rectangle(GameData.Window.Width - 310, 50, 300, 250);
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
        public InterfaceCaption[] Captions { get; set; }
    }

    public class InterfaceGroup
    {
        public InterfaceCell[] Cells { get; set; }
        public Rectangle Rect { get; set; }
        public Bitmap Back { get; set; }
        public string ID { get; set; }
        public Action Action { get; set; }
        public InterfaceCaption[] Captions { get; set; }
        public void Click(Point e)
        {
            if (e.X >= 0 && e.X < Rect.Width &&
                e.Y >= 0 && e.Y < Rect.Height)
            {
                Action?.Invoke();
                if (Captions != null)
                {
                    for (int c = 0; c < Captions.Length; c++)
                    {
                        if (e.X > Captions[c].Rect.Left &&
                            e.X < Captions[c].Rect.Right &&
                            e.Y > Captions[c].Rect.Top &&
                            e.Y < Captions[c].Rect.Bottom)
                        {
                            Captions[c].Action?.Invoke();
                            Captions[c].Hovered = true;
                        }
                        else
                        {
                            Captions[c].Hovered = false;
                        }
                    }
                }
                if (Cells != null)
                {
                    for (int c = 0; c < Cells.Length; c++)
                    {
                        if (e.X > Cells[c].Rect.Left &&
                            e.X < Cells[c].Rect.Right &&
                            e.Y > Cells[c].Rect.Top &&
                            e.Y < Cells[c].Rect.Bottom)
                        {
                            Cells[c].Action?.Invoke();
                            Cells[c].Hovered = true;
                        }
                        else
                        {
                            Cells[c].Hovered = false;
                        }
                    }
                }
            }
        }
        public void MouseMove(Point e)
        {
            if (e.X >= 0 && e.X < Rect.Width &&
                e.Y >= 0 && e.Y < Rect.Height)
            {
                if (Captions != null)
                {
                    for (int c = 0; c < Captions.Length; c++)
                    {
                        if (e.X > Captions[c].Rect.Left &&
                            e.X < Captions[c].Rect.Right &&
                            e.Y > Captions[c].Rect.Top &&
                            e.Y < Captions[c].Rect.Bottom)
                        {
                            Captions[c].Hovered = true;
                        }
                        else
                        {
                            Captions[c].Hovered = false;
                        }
                    }
                }
                if (Cells != null)
                {
                    for (int c = 0; c < Cells.Length; c++)
                    {
                        if (e.X > Cells[c].Rect.Left &&
                            e.X < Cells[c].Rect.Right &&
                            e.Y > Cells[c].Rect.Top &&
                            e.Y < Cells[c].Rect.Bottom)
                        {
                            Cells[c].Hovered = true;
                        }
                        else
                        {
                            Cells[c].Hovered = false;
                        }
                    }
                }
            }
            else
            {
                if (Captions != null) for (int c = 0; c < Captions.Length; c++) Captions[c].Hovered = false; 
                if (Cells != null) for (int c = 0; c < Cells.Length; c++) Cells[c].Hovered = false; 
            }
        }
        public void Dispose()
        {
            Back.Dispose();
        }
    }

    public class InterfaceCell
    {
        public Item Item { get; set; }
        public Rectangle Rect { get; set; }
        public InterfaceCaption[] Captions { get; set; }
        public bool Hovered { get; set; }
        public Action Action { get; set; }
    }

    public class InterfaceCaption
    {
        public string Text { get; set; } = "";
        public Rectangle Rect { get; set; } = new Rectangle(0, 0, 100, 50);
        public Font Font { get; set; } = GameData.Font;
        public Color Color { get; set; } = Color.White;
        public Color HoverColor { get; set; } = Color.FromArgb(255, 73, 99, 217);
        public bool Hovered { get; set; } = false;
        public TextFormatFlags Flags { get; set; } = GameData.Flags;
        public Action Action { get; set; }
    }
}
