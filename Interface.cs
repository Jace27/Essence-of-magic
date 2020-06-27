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
        public static InterfaceColorCollection Colors;
        public static InterfacePages Page = InterfacePages.Game;
        public static InterfacePagesCollection Pages;
        public static InterfaceGroup Question;
        public static InterfaceGroup MouseHint;
        public static InterfaceGroup Screen;
        public static InterfaceCaption[] Captions;
        #region Interface Settings
        private static Size CellSize = new Size(64, 64);
        private static Size CellMargin = new Size(8, 8);
        private static Rectangle CellArea = new Rectangle(GameData.Window.Width / 3, 32, GameData.Window.Width / 3 * 2 - 32, GameData.Window.Height - 64);
        public static int InventoryCellsCount;
        public static int ShopCellsCount;
        private static Size MouseHintSize = new Size(200, 50);
        #endregion
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
                        Rect = new Rectangle((Question.Rect.Width - 290) / 2, (Question.Rect.Height - 75) / 2 - 25, 290, 75)
                    };
                    Question.Captions[1] = new InterfaceCaption()
                    {
                        Text = "Да",
                        Rect = new Rectangle((Question.Rect.Width - 75) / 2 - 75, (Question.Rect.Height - 30) / 2 + 50, 75, 30),
                        ActionLeft = () =>
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
                        ActionLeft = () =>
                        {
                            Question = null;
                            Application.Exit();
                        }
                    };
                    Question.Captions[3] = new InterfaceCaption()
                    {
                        Text = "Отмена",
                        Rect = new Rectangle((Question.Rect.Width - 75) / 2 + 75, (Question.Rect.Height - 30) / 2 + 50, 100, 30),
                        ActionLeft = () =>
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
                        ActionLeft = () =>
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
                        ActionLeft = () =>
                        {
                            GameData.Load();
                            Question = null;
                        }
                    };
                    Question.Captions[3] = new InterfaceCaption()
                    {
                        Text = "Отмена",
                        Rect = new Rectangle((Question.Rect.Width - 75) / 2 + 75, (Question.Rect.Height - 30) / 2 + 50, 100, 30),
                        ActionLeft = () =>
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
            #region Colors
            Colors = new InterfaceColorCollection();
            Colors["HealthBar"] = Color.FromArgb(255, 168, 240, 0);
            Colors["SatietyBar"] = Color.FromArgb(255, 255, 165, 0);
            Colors["WaterBar"] = Color.FromArgb(255, 51, 204, 204);
            Colors["QuestionBackColor"] = Color.FromArgb(255, 0, 0, 0);
            Colors["TranslucentBack"] = Color.FromArgb(192, 48, 48, 48);
            Colors["Empty"] = Color.FromArgb(0, 0, 0, 0);
            Colors["White"] = Color.FromArgb(255, 255, 255, 255);
            Colors["HoverTranslucentBack"] = Color.FromArgb(128, 255, 255, 255);
            Colors["PlayerDataCaptions"] = Color.FromArgb(255, 60, 60, 60);
            #endregion

            IMG.FromBitmap(template);
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
                }, 
                BackColor = Colors["Empty"]
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
                                ActionLeft = () => { GameData.Save(); }
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
                                ActionLeft = () =>
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
                                Rect = new Rectangle(0, 0, 300, 50),
                                ActionLeft = () =>
                                {
                                    GameData.Settings();
                                }
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
                                ActionLeft = () =>
                                {
                                    Interface.Question_ExitSaveGame();
                                }
                            }
                        }
                    }
                }, 
                BackColor = Colors["TranslucentBack"]
            };

            #endregion

            #region Inventory
            InterfaceCell[] cells = new InterfaceCell[InventoryCellsCount];
            int colscount = CellArea.Width / (CellMargin.Width + CellSize.Width);
            int rowscount = InventoryCellsCount / colscount;
            int height = rowscount * (CellMargin.Height + CellSize.Height) - CellMargin.Height;
            int x = CellMargin.Width, y = (CellArea.Height - height) / 2;
            for (int i = 0; i < cells.Length; i++)
            {
                if (x + CellSize.Width + CellMargin.Width > CellArea.Width)
                {
                    x = CellMargin.Width;
                    y += CellSize.Height + CellMargin.Height;
                }
                cells[i] = new InterfaceCell()
                {
                    Rect = new Rectangle(new Point(x, y), CellSize)
                };
                x += CellSize.Width + CellMargin.Width;
            }
            Pages[InterfacePages.Inventory] = new InterfacePage()
            {
                InterfaceGroups = new InterfaceGroup[2]
                {
                    new InterfaceGroup()
                    {
                        ID = "PlayerData",
                        Rect = new Rectangle(0, GameData.Window.Height / 6, GameData.Window.Width / 3, GameData.Window.Height / 3 * 2),
                        Back = (Bitmap)Image.FromFile(GameData.TextureFolder + "\\Technical\\hint.png"),
                        Captions = new InterfaceCaption[7]
                        {
                            new InterfaceCaption()
                            {
                                ID = "Name",
                                Text = GameData.Game.Player.Name,
                                Rect = new Rectangle(0, 60, GameData.Window.Width / 3, 30),
                                Color = Colors["PlayerDataCaptions"],
                                Font = new Font("Arial", 14)
                            },
                            new InterfaceCaption()
                            {
                                ID = "Health",
                                Rect = new Rectangle(0, 100, GameData.Window.Width / 3, 30),
                                Color = Colors["PlayerDataCaptions"],
                                Font = new Font("Arial", 14)
                            },
                            new InterfaceCaption()
                            {
                                ID = "Satiety",
                                Rect = new Rectangle(0, 130, GameData.Window.Width / 3, 30),
                                Color = Colors["PlayerDataCaptions"],
                                Font = new Font("Arial", 14)
                            },
                            new InterfaceCaption()
                            {
                                ID = "Water",
                                Rect = new Rectangle(0, 160, GameData.Window.Width / 3, 30),
                                Color = Colors["PlayerDataCaptions"],
                                Font = new Font("Arial", 14)
                            },
                            new InterfaceCaption()
                            {
                                ID = "Money",
                                Rect = new Rectangle(0, 190, GameData.Window.Width / 3, 30),
                                Color = Colors["PlayerDataCaptions"],
                                Font = new Font("Arial", 14)
                            },
                            new InterfaceCaption()
                            {
                                ID = "Level",
                                Rect = new Rectangle(),
                                Color = Colors["PlayerDataCaptions"]
                            },
                            new InterfaceCaption()
                            {
                                ID = "Experience",
                                Rect = new Rectangle(),
                                Color = Colors["PlayerDataCaptions"]
                            }
                        }
                    },
                    new InterfaceGroup()
                    {
                        ID = "CellArea",
                        Rect = CellArea,
                        BackColor = Colors["TranslucentBack"],
                        Cells = cells,
                        Captions = new InterfaceCaption[1]
                        {
                            new InterfaceCaption()
                            {
                                ID = "_nameOfGroup",
                                Text = "Инвентарь",
                                Color = Colors["White"],
                                Rect = new Rectangle(0, 0, GameData.Window.Width / 3 * 2 - 32, 40),
                                Font = new Font(GameData.Font.FontFamily, 18, FontStyle.Underline)
                            }
                        }
                    }
                }, 
                Type = InterfacePages.Inventory
            };

            #endregion

            #region Shop
            cells = new InterfaceCell[ShopCellsCount];
            colscount = CellArea.Width / (CellMargin.Width + CellSize.Width);
            rowscount = ShopCellsCount / colscount;
            height = rowscount * (CellMargin.Height + CellSize.Height) - CellMargin.Height;
            x = CellMargin.Width; y = (CellArea.Height - height) / 2;
            for (int i = 0; i < cells.Length; i++)
            {
                if (x + CellSize.Width + CellMargin.Width > CellArea.Width)
                {
                    x = CellMargin.Width;
                    y += CellSize.Height + CellMargin.Height;
                }
                cells[i] = new InterfaceCell()
                {
                    Rect = new Rectangle(new Point(x, y), CellSize)
                };
                x += CellSize.Width + CellMargin.Width;
            }
            Pages[InterfacePages.Shop] = new InterfacePage()
            {
                InterfaceGroups = new InterfaceGroup[2]
                {
                    new InterfaceGroup()
                    {
                        ID = "ShopData",
                        Rect = new Rectangle(0, GameData.Window.Height / 6, GameData.Window.Width / 3, GameData.Window.Height / 3 * 2),
                        Back = (Bitmap)Image.FromFile(GameData.TextureFolder + "\\Technical\\hint.png"),
                        Captions = new InterfaceCaption[3]
                        {
                            new InterfaceCaption()
                            {
                                ID = "Name",
                                Text = GameData.World.Objects.creatures[0].Name,
                                Rect = new Rectangle(0, 60, GameData.Window.Width / 3, 30),
                                Color = Colors["PlayerDataCaptions"],
                                Font = new Font("Arial", 14)
                            },
                            new InterfaceCaption()
                            {
                                ID = "Message",
                                Text = "Добрый день!\nПрекрасная погода,\nне правда ли?\n\nЧто изволите купить?",
                                Rect = new Rectangle(0, 100, GameData.Window.Width / 3, 120),
                                Color = Colors["PlayerDataCaptions"],
                                Font = new Font("Arial", 14)
                            },
                            new InterfaceCaption()
                            {
                                ID = "Money",
                                Rect = new Rectangle(0, 300, GameData.Window.Width / 3, 30),
                                Color = Colors["PlayerDataCaptions"],
                                Font = new Font("Arial", 14)
                            }
                        }
                    },
                    new InterfaceGroup()
                    {
                        ID = "CellArea",
                        Rect = CellArea,
                        BackColor = Colors["TranslucentBack"],
                        Cells = cells,
                        Captions = new InterfaceCaption[1]
                        {
                            new InterfaceCaption()
                            {
                                ID = "_nameOfGroup",
                                Text = "Торговец",
                                Color = Colors["White"],
                                Rect = new Rectangle(0, 0, GameData.Window.Width / 3 * 2 - 32, 40),
                                Font = new Font(GameData.Font.FontFamily, 18, FontStyle.Underline)
                            }
                        }
                    }
                },
                Type = InterfacePages.Shop
            };
            #endregion

            _ = Task.Run(() =>
            {
                DateTime last1 = DateTime.Now;
                DateTime last2 = DateTime.Now;
                int f = 0;
                while (Settings.RegenerateInterface)
                {
                    f++;

                    if (Owner != null)
                    {
                        try
                        {
                            Owner.Invoke(new GameData.ThreadTransit(() =>
                            {
                                try
                                {
                                    Interface.DrawPage();
                                }
                                catch (Exception ex)
                                {
                                    Settings.RegenerateInterface = false;
                                }
                            }));
                        }
                        catch
                        {

                        }
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

        #region Item Drag'n'Drop
        public static Item DraggingItem;
        public static int StartCell;
        public static int EndCell;
        public static bool isDragging = false;
        public static Size delta = new Size(-1, -1);

        public static void Game_MouseDown(MouseEventArgs e)
        {
            if (Page == InterfacePages.Inventory && e.Button == MouseButtons.Left)
            {
                int groupI = -1;
                for (int i = 0; i < Pages[Page].InterfaceGroups.Length; i++)
                    if (Pages[Page].InterfaceGroups[i].ID == "CellArea")
                        groupI = i;
                if (groupI == -1) return;
                if (e.X >= Pages[Page].InterfaceGroups[groupI].Rect.Left && e.Y >= Pages[Page].InterfaceGroups[groupI].Rect.Top &&
                    e.X <= Pages[Page].InterfaceGroups[groupI].Rect.Right && e.Y <= Pages[Page].InterfaceGroups[groupI].Rect.Bottom)
                {
                    Point mouse = new Point(e.X - Pages[Page].InterfaceGroups[groupI].Rect.Left, e.Y - Pages[Page].InterfaceGroups[groupI].Rect.Top);
                    for (int i = 0; i < Pages[Page].InterfaceGroups[groupI].Cells.Length; i++)
                    {
                        if (mouse.X >= Pages[Page].InterfaceGroups[groupI].Cells[i].Rect.Left && mouse.Y >= Pages[Page].InterfaceGroups[groupI].Cells[i].Rect.Top &&
                            mouse.X <= Pages[Page].InterfaceGroups[groupI].Cells[i].Rect.Right && mouse.Y <= Pages[Page].InterfaceGroups[groupI].Cells[i].Rect.Bottom)
                        {
                            if (Pages[Page].InterfaceGroups[groupI].Cells[i].Item != null)
                            {
                                DraggingItem = GameData.Game.Player.Inventory.Backpack[i];
                                GameData.Game.Player.Inventory.Backpack[i] = null;
                                isDragging = true;
                                StartCell = i;
                                delta = new Size(mouse.X - Pages[Page].InterfaceGroups[groupI].Cells[i].Rect.Left, mouse.Y - Pages[Page].InterfaceGroups[groupI].Cells[i].Rect.Top);
                            }
                            break;
                        }
                    }
                }
            }
        }

        public static void Game_MouseUp(MouseEventArgs e)
        {
            if (isDragging && e.Button == MouseButtons.Left)
            {
                if (Page == InterfacePages.Inventory)
                {
                    int groupI = -1;
                    for (int i = 0; i < Pages[Page].InterfaceGroups.Length; i++)
                        if (Pages[Page].InterfaceGroups[i].ID == "CellArea")
                            groupI = i;
                    if (groupI == -1) return;
                    if (e.X >= Pages[Page].InterfaceGroups[groupI].Rect.Left && e.Y >= Pages[Page].InterfaceGroups[groupI].Rect.Top &&
                        e.X <= Pages[Page].InterfaceGroups[groupI].Rect.Right && e.Y <= Pages[Page].InterfaceGroups[groupI].Rect.Bottom)
                    {
                        Point mouse = new Point(e.X - Pages[Page].InterfaceGroups[groupI].Rect.Left, e.Y - Pages[Page].InterfaceGroups[groupI].Rect.Top);
                        for (int i = 0; i < Pages[Page].InterfaceGroups[groupI].Cells.Length; i++)
                        {
                            if (mouse.X >= Pages[Page].InterfaceGroups[groupI].Cells[i].Rect.Left && mouse.Y >= Pages[Page].InterfaceGroups[groupI].Cells[i].Rect.Top &&
                                mouse.X <= Pages[Page].InterfaceGroups[groupI].Cells[i].Rect.Right && mouse.Y <= Pages[Page].InterfaceGroups[groupI].Cells[i].Rect.Bottom)
                            {
                                EndCell = i;
                                if (GameData.Game.Player.Inventory.Backpack[EndCell] != null)
                                {
                                    if (GameData.Game.Player.Inventory.Backpack[EndCell].ID == DraggingItem.ID && DraggingItem.Stackable)
                                        GameData.Game.Player.Inventory.Backpack[EndCell].Count += DraggingItem.Count;
                                    else
                                        GameData.Game.Player.Inventory.Set(StartCell, GameData.Game.Player.Inventory.Backpack[EndCell]);
                                    if (GameData.Game.Player.Inventory.Backpack[EndCell].ID != DraggingItem.ID || !DraggingItem.Stackable)
                                        GameData.Game.Player.Inventory.Set(EndCell, DraggingItem);
                                }
                                else
                                {
                                    GameData.Game.Player.Inventory.Set(EndCell, DraggingItem);
                                }
                                isDragging = false;
                                delta = new Size(-1, -1);
                                return;
                            }
                        }
                    }
                }
                GameData.Game.Player.Inventory.Set(StartCell, DraggingItem);
                delta = new Size(-1, -1);
                isDragging = false;
            }
        }
        #endregion

        public static int FPS = 20;
        public static int FPSlimit = 20;
        public static GLMultiImage IMG = new GLMultiImage();
        private static Bitmap template = new Bitmap(GameData.Window.Width, GameData.Window.Height);
        public static void DrawPage()
        {
            Bitmap Output = (Bitmap)template.Clone();
            using (Graphics gr1 = Graphics.FromImage(Output))
            {
                gr1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                InterfacePage page = Pages[Page];
                if (page.Back != null)
                    gr1.DrawImage(page.Back, 0, 0, page.Rect.Width, page.Rect.Height);
                else if (page.BackColor != null)
                    gr1.Clear(page.BackColor);
                else
                    gr1.Clear(Colors["TranslucentBack"]);

                if (Page == InterfacePages.Game)
                {
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
                                double max, reg;
                                int barx = 50, bary = 20,
                                    bardx = 10, bardy = 10,
                                    barw = 200, barh = 10,
                                    iconsize = 20;
                                #region Health bar
                                gr2.DrawImage(Image.FromFile(GameData.TextureFolder + "\\Technical\\hp.png"),
                                              barx + barw + bardx, bary - ((iconsize - barh) / 2), iconsize, iconsize);
                                gr2.FillRectangle(Brushes.White, barx, bary, barw, barh);
                                max = GameData.Game.Player.HPMax;
                                reg = GameData.Game.Player.HP;
                                gr2.FillRectangle(new SolidBrush(Colors["HealthBar"]), barx, bary, (int)Math.Round((reg / max) * barw, 0), barh);
                                bary = bary + barh + bardy;
                                #endregion

                                #region Food bar
                                gr2.DrawImage(Image.FromFile(GameData.TextureFolder + "\\Technical\\satiety.png"),
                                              barx + barw + bardx, bary - ((iconsize - barh) / 2), iconsize, iconsize);
                                gr2.FillRectangle(Brushes.White, barx, bary, barw, barh);
                                max = GameData.Game.Player.SatietyMax;
                                reg = GameData.Game.Player.Satiety;
                                gr2.FillRectangle(new SolidBrush(Colors["SatietyBar"]), barx, bary, (int)Math.Round((reg / max) * barw, 0), barh);
                                bary = bary + barh + bardy;
                                #endregion

                                #region Water bar
                                gr2.DrawImage(Image.FromFile(GameData.TextureFolder + "\\Technical\\water.png"),
                                              barx + barw + bardx, bary - ((iconsize - barh) / 2), iconsize, iconsize);
                                gr2.FillRectangle(Brushes.White, barx, bary, barw, barh);
                                max = GameData.Game.Player.WaterMax;
                                reg = GameData.Game.Player.Water;
                                gr2.FillRectangle(new SolidBrush(Colors["WaterBar"]), barx, bary, (int)Math.Round((reg / max) * barw, 0), barh);
                                bary = bary + barh + bardy;
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
                                    if (!cap.Hovered || cap.ActionLeft == null)
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
                                    if (gr.Captions[c].Hovered && gr.Captions[c].ActionLeft != null)
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
                else
                if (Page == InterfacePages.Inventory)
                {
                    for (int g = 0; g < page.InterfaceGroups.Length; g++)
                    {
                        InterfaceGroup gr = page.InterfaceGroups[g];

                        if (gr.ID == "CellArea")
                        {
                            if (gr.Cells.Length != Settings.BackpackCapacity)
                                gr.Cells = new InterfaceCell[Settings.BackpackCapacity];
                            int colscount = CellArea.Width / (CellMargin.Width + CellSize.Width);
                            int rowscount = InventoryCellsCount / colscount;
                            int height = rowscount * (CellMargin.Height + CellSize.Height) - CellMargin.Height;
                            int x = CellMargin.Width, y = (CellArea.Height - height) / 2;
                            for (int c = 0; c < gr.Cells.Length; c++)
                            {
                                if (x + CellSize.Width + CellMargin.Width > CellArea.Width)
                                {
                                    x = CellMargin.Width;
                                    y += CellSize.Height + CellMargin.Height;
                                }
                                if (gr.Cells[c] == null)
                                {
                                    gr.Cells[c] = new InterfaceCell()
                                    {
                                        Rect = new Rectangle(new Point(x, y), CellSize)
                                    };
                                }
                                if (GameData.Game.Player.Inventory.Backpack[c] != null)
                                    if (GameData.Game.Player.Inventory.Backpack[c].Count <= 0) 
                                        GameData.Game.Player.Inventory.Backpack[c] = null;
                                gr.Cells[c].Item = GameData.Game.Player.Inventory.Backpack[c];
                                if (gr.Cells[c].Item != null)
                                {
                                    if (gr.Cells[c].Item.ID == "flask")
                                        gr.Cells[c].Item.Action = () =>
                                        {
                                            GameData.Game.Invoke(new GameData.ThreadTransit(() => { GameData.Game.Player.Water += 100; }));
                                        };
                                    else if (gr.Cells[c].Item.ID == "bread")
                                        gr.Cells[c].Item.Action = () =>
                                        {
                                            GameData.Game.Invoke(new GameData.ThreadTransit(() => { GameData.Game.Player.Satiety += 30; }));
                                        };
                                    else if (gr.Cells[c].Item.ID == "soup")
                                        gr.Cells[c].Item.Action = () =>
                                        {
                                            GameData.Game.Invoke(new GameData.ThreadTransit(() => { GameData.Game.Player.Water += 70; }));
                                        };
                                }
                                x += CellSize.Width + CellMargin.Width;
                            }
                        }
                        if (gr.ID == "PlayerData")
                        {
                            for (int c = 0; c < gr.Captions.Length; c++)
                            {
                                if (gr.Captions[c].ID == "Health")
                                    gr.Captions[c].Text = String.Format("Здоровье: {0} / {1}", GameData.Game.Player.HP, GameData.Game.Player.HPMax);
                                if (gr.Captions[c].ID == "Satiety")
                                    gr.Captions[c].Text = String.Format("Сытость: {0} / {1}", GameData.Game.Player.Satiety, GameData.Game.Player.SatietyMax);
                                if (gr.Captions[c].ID == "Water")
                                    gr.Captions[c].Text = String.Format("Жажда: {0} / {1}", GameData.Game.Player.Water, GameData.Game.Player.WaterMax);
                                if (gr.Captions[c].ID == "Money")
                                    gr.Captions[c].Text = String.Format("Монет: {0}", GameData.Game.Player.Inventory.Money);
                            }
                        }

                        Bitmap group = new Bitmap(gr.Rect.Width, gr.Rect.Height);
                        using (Graphics gr2 = Graphics.FromImage(group))
                        {
                            if (gr.Back != null)
                                gr2.DrawImage(gr.Back, 0, 0, gr.Rect.Width, gr.Rect.Height);
                            else if (gr.BackColor != null)
                                gr2.Clear(gr.BackColor);
                            else
                                gr2.Clear(Colors["TranslucentBack"]);
                            if (gr.Cells != null)
                            {
                                for (int c = 0; c < gr.Cells.Length; c++)
                                {
                                    InterfaceCell cell = gr.Cells[c];
                                    Bitmap cellbmp = new Bitmap(cell.Rect.Width, cell.Rect.Height);
                                    using (Graphics gr3 = Graphics.FromImage(cellbmp))
                                    {
                                        if (cell.Hovered)
                                            gr3.Clear(Colors["HoverTranslucentBack"]);
                                        else
                                            gr3.Clear(Colors["TranslucentBack"]);

                                        if (cell.Item != null)
                                        {
                                            gr3.DrawImage(cell.Item.Sprite.GetIMG().bitmap, 0, 0, cell.Rect.Width, cell.Rect.Height);
                                            TextRenderer.DrawText(
                                                gr3,
                                                cell.Item.Count.ToString(),
                                                new Font("Arial", 10),
                                                new Rectangle(cell.Rect.Width - 20, cell.Rect.Height - 20, 20, 20), 
                                                Color.White,
                                                TextFormatFlags.Right | TextFormatFlags.Bottom
                                            );
                                        }
                                    }
                                    gr2.DrawImage(cellbmp, cell.Rect);
                                }
                            }
                            if (gr.Captions != null)
                            {
                                for (int c = 0; c < gr.Captions.Length; c++)
                                {
                                    if (gr.Captions[c].Hovered && gr.Captions[c].ActionLeft != null)
                                        TextRenderer.DrawText(gr2, gr.Captions[c].Text, gr.Captions[c].Font, gr.Captions[c].Rect, gr.Captions[c].HoverColor, gr.Captions[c].Flags);
                                    else
                                        TextRenderer.DrawText(gr2, gr.Captions[c].Text, gr.Captions[c].Font, gr.Captions[c].Rect, gr.Captions[c].Color, gr.Captions[c].Flags);
                                }
                            }
                            gr1.DrawImage(group, page.InterfaceGroups[g].Rect);
                            group.Dispose();
                        }
                    }
                    if (isDragging)
                    {
                        gr1.DrawImage(DraggingItem.GetBMP(), GameData.Cursor.X - delta.Width, GameData.Cursor.Y - delta.Height, DraggingItem.Size.Width, DraggingItem.Size.Height);
                    }
                }
                else
                if (Page == InterfacePages.Shop)
                {
                    for (int g = 0; g < page.InterfaceGroups.Length; g++)
                    {
                        InterfaceGroup gr = page.InterfaceGroups[g];

                        if (gr.ID == "CellArea")
                        {
                            if (gr.Cells.Length != ShopCellsCount)
                                gr.Cells = new InterfaceCell[ShopCellsCount];
                            int colscount = CellArea.Width / (CellMargin.Width + CellSize.Width);
                            int rowscount = InventoryCellsCount / colscount;
                            int height = rowscount * (CellMargin.Height + CellSize.Height) - CellMargin.Height;
                            int x = CellMargin.Width, y = (CellArea.Height - height) / 2;
                            for (int c = 0; c < gr.Cells.Length; c++)
                            {
                                if (x + CellSize.Width + CellMargin.Width > CellArea.Width)
                                {
                                    x = CellMargin.Width;
                                    y += CellSize.Height + CellMargin.Height;
                                }
                                if (gr.Cells[c] == null)
                                {
                                    gr.Cells[c] = new InterfaceCell()
                                    {
                                        Rect = new Rectangle(new Point(x, y), CellSize)
                                    };
                                }
                                if (GameData.World.Objects.creatures[0].Inventory.Backpack[c] != null)
                                    if (GameData.World.Objects.creatures[0].Inventory.Backpack[c].Count <= 0)
                                        GameData.World.Objects.creatures[0].Inventory.Backpack[c] = null;
                                gr.Cells[c].Item = GameData.World.Objects.creatures[0].Inventory.Backpack[c];
                                if (gr.Cells[c].Item != null)
                                {
                                    gr.Cells[c].Item.BuyAction = (Item Item) =>
                                    {
                                        GameData.Game.Invoke(new GameData.ThreadTransitForItems((Item item) =>
                                        {
                                            if (GameData.Game.Player.Inventory.Money >= item.Cost)
                                            {
                                                GameData.Game.Player.Inventory.Money -= item.Cost;
                                                GameData.Game.Player.Inventory.Give(item);
                                            }
                                        }), Item.Clone());
                                    };
                                }
                                x += CellSize.Width + CellMargin.Width;
                            }
                        }
                        if (gr.ID == "ShopData")
                        {
                            for (int c = 0; c < gr.Captions.Length; c++)
                            {
                                if (gr.Captions[c].ID == "Money")
                                    gr.Captions[c].Text = String.Format("Монет: {0}", GameData.Game.Player.Inventory.Money);
                            }
                        }

                        Bitmap group = new Bitmap(gr.Rect.Width, gr.Rect.Height);
                        using (Graphics gr2 = Graphics.FromImage(group))
                        {
                            if (gr.Back != null)
                                gr2.DrawImage(gr.Back, 0, 0, gr.Rect.Width, gr.Rect.Height);
                            else if (gr.BackColor != null)
                                gr2.Clear(gr.BackColor);
                            else
                                gr2.Clear(Colors["TranslucentBack"]);
                            if (gr.Cells != null)
                            {
                                for (int c = 0; c < gr.Cells.Length; c++)
                                {
                                    InterfaceCell cell = gr.Cells[c];
                                    Bitmap cellbmp = new Bitmap(cell.Rect.Width, cell.Rect.Height);
                                    using (Graphics gr3 = Graphics.FromImage(cellbmp))
                                    {
                                        if (cell.Hovered)
                                            gr3.Clear(Colors["HoverTranslucentBack"]);
                                        else
                                            gr3.Clear(Colors["TranslucentBack"]);

                                        if (cell.Item != null)
                                        {
                                            gr3.DrawImage(cell.Item.Sprite.GetIMG().bitmap, 0, 0, cell.Rect.Width, cell.Rect.Height);
                                            TextRenderer.DrawText(
                                                gr3,
                                                cell.Item.Count.ToString(),
                                                new Font("Arial", 10),
                                                new Rectangle(cell.Rect.Width - 20, cell.Rect.Height - 20, 20, 20),
                                                Color.White,
                                                TextFormatFlags.Right | TextFormatFlags.Bottom
                                            );
                                        }
                                    }
                                    gr2.DrawImage(cellbmp, cell.Rect);
                                }
                            }
                            if (gr.Captions != null)
                            {
                                for (int c = 0; c < gr.Captions.Length; c++)
                                {
                                    if (gr.Captions[c].Hovered && gr.Captions[c].ActionLeft != null)
                                        TextRenderer.DrawText(gr2, gr.Captions[c].Text, gr.Captions[c].Font, gr.Captions[c].Rect, gr.Captions[c].HoverColor, gr.Captions[c].Flags);
                                    else
                                        TextRenderer.DrawText(gr2, gr.Captions[c].Text, gr.Captions[c].Font, gr.Captions[c].Rect, gr.Captions[c].Color, gr.Captions[c].Flags);
                                }
                            }
                            gr1.DrawImage(group, page.InterfaceGroups[g].Rect);
                            group.Dispose();
                        }
                    }
                    if (isDragging)
                    {
                        gr1.DrawImage(DraggingItem.GetBMP(), GameData.Cursor.X - delta.Width, GameData.Cursor.Y - delta.Height, DraggingItem.Size.Width, DraggingItem.Size.Height);
                    }
                }

                if (Question != null)
                {
                    Bitmap question = new Bitmap(Question.Rect.Width, Question.Rect.Height);
                    using (Graphics gr2 = Graphics.FromImage(question))
                    {
                        gr2.Clear(Colors["QuestionBackColor"]);
                        gr2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        if (Question.Captions != null)
                        {
                            for (int c = 0; c < Question.Captions.Length; c++)
                            {
                                InterfaceCaption cap = Question.Captions[c];
                                if (cap.Hovered && cap.ActionLeft != null)
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

                if (MouseHint != null)
                {
                    Bitmap _mousehint = new Bitmap(MouseHint.Rect.Width, MouseHint.Rect.Height);
                    using (Graphics gr2 = Graphics.FromImage(_mousehint))
                    {
                        if (MouseHint.Back != null)
                            gr2.DrawImage(MouseHint.Back, 0, 0, MouseHint.Rect.Width, MouseHint.Rect.Height);
                        else if (MouseHint.BackColor != null)
                            gr2.Clear(MouseHint.BackColor);
                        else
                            gr2.Clear(Colors["TranslucentBack"]);

                        for (int c = 0; c < MouseHint.Captions.Length; c++)
                        {
                            InterfaceCaption cap = MouseHint.Captions[c];
                            TextRenderer.DrawText(gr2, cap.Text, cap.Font, cap.Rect, cap.Color, cap.Flags);
                        }
                    }
                    gr1.DrawImage(_mousehint, GameData.Cursor.X + 5, GameData.Cursor.Y + 5, _mousehint.Width, _mousehint.Height);
                }

                if (Screen != null)
                {
                    Bitmap screen = new Bitmap(GameData.Window.Width, GameData.Window.Height);
                    using (Graphics gr2 = Graphics.FromImage(screen))
                    {
                        gr2.Clear(Screen.BackColor);
                        for (int c = 0; c < Screen.Captions.Length; c++)
                        {
                            InterfaceCaption cap = Screen.Captions[c];
                            TextRenderer.DrawText(gr2, cap.Text, cap.Font, cap.Rect, cap.Color, cap.Flags);
                        }
                    }
                    gr1.DrawImage(screen, 0, 0);
                    screen.Dispose();
                }
            }
            IMG.Free();
            IMG.FromBitmap(Output);
            Output.Dispose();
        }
        public static void Click(Point e, MouseButtons Button)
        {
            InterfacePage page = Pages[Interface.Page];
            for (int g = 0; g < page.InterfaceGroups.Length; g++)
            {
                if (e.X > page.InterfaceGroups[g].Rect.Left &&
                    e.X < page.InterfaceGroups[g].Rect.Right &&
                    e.Y > page.InterfaceGroups[g].Rect.Top &&
                    e.Y < page.InterfaceGroups[g].Rect.Bottom)
                {
                    page.InterfaceGroups[g].Click(new Point(e.X - page.InterfaceGroups[g].Rect.Left, e.Y - page.InterfaceGroups[g].Rect.Top), Button);
                }
            }
            if (Question != null)
            {
                if (e.X > Question.Rect.Left &&
                    e.X < Question.Rect.Right &&
                    e.Y > Question.Rect.Top &&
                    e.Y < Question.Rect.Bottom)
                {
                    Question.Click(new Point(e.X - Question.Rect.Left, e.Y - Question.Rect.Top), Button);
                }
            }
            if (Screen != null)
            {
                Screen.Click(new Point(e.X - Screen.Rect.Left, e.Y - Screen.Rect.Top), Button);
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
        public static void ShowHint(string Header, string Text)
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
                    ActionLeft = () =>
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
        public static void ShowMouseHint(Item Item, Size Size)
        {
            string Header = Item.Name;
            string Text = Item.Description;
            Rectangle rect = new Rectangle(new Point(0, 0), Size);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.Clear(Color.Black);
                gr.DrawRectangle(Pens.Blue, 0, 0, rect.Width - 1, rect.Height - 1);
            }
            MouseHint = new InterfaceGroup()
            {
                ID = "MouseHint",
                Back = bmp,
                Captions = new InterfaceCaption[2]
                {
                    new InterfaceCaption()
                    {
                        ID = "MouseHintHeader",
                        Text = Header,
                        Color = Color.Yellow,
                        Flags = TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.Top,
                        Font = new Font(GameData.Font.FontFamily, 10),
                        Rect = new Rectangle(5, 5, rect.Width - 10, 20)
                    },
                    new InterfaceCaption()
                    {
                        ID = "MouseHintText",
                        Text = Text,
                        Color = Color.White,
                        Flags = TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.Top,
                        Font = new Font(GameData.Font.FontFamily, 10),
                        Rect = new Rectangle(5, 30, rect.Width - 10, rect.Height - 35)
                    }
                },
                Rect = rect
            };
        }
        public static void ShowMouseHint(string Header, string Text, Size Size)
        {
            Rectangle rect = new Rectangle(new Point(0, 0), Size);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.Clear(Color.Black);
                gr.DrawRectangle(Pens.Blue, 0, 0, rect.Width - 1, rect.Height - 1);
            }
            MouseHint = new InterfaceGroup()
            {
                ID = "MouseHint",
                Back = bmp,
                Captions = new InterfaceCaption[2]
                {
                    new InterfaceCaption()
                    {
                        ID = "MouseHintHeader",
                        Text = Header,
                        Color = Color.Yellow,
                        Flags = TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.Top,
                        Font = new Font(GameData.Font.FontFamily, 10),
                        Rect = new Rectangle(5, 5, rect.Width - 10, 15)
                    },
                    new InterfaceCaption()
                    {
                        ID = "MouseHintText",
                        Text = Text,
                        Color = Color.White,
                        Flags = TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.Top,
                        Font = new Font(GameData.Font.FontFamily, 10),
                        Rect = new Rectangle(5, 30, rect.Width - 10, rect.Height - 35)
                    }
                },
                Rect = rect
            };
        }
        public static void ShowScreen(string Header, string Description, Color HeaderColor, Color DescColor, Color BackColor, Action Action)
        {
            Screen = new InterfaceGroup()
            {
                ID = "Screen",
                Rect = new Rectangle(0, 0, GameData.Window.Width, GameData.Window.Height),
                BackColor = BackColor,
                Captions = new InterfaceCaption[2]
                {
                    new InterfaceCaption()
                    {
                        ID = "ScreenHeader",
                        Color = HeaderColor,
                        Font = new Font(GameData.Font.FontFamily, 30),
                        Text = Header,
                        Rect = new Rectangle(GameData.Window.Width / 12, 0, GameData.Window.Width / 12 * 10, GameData.Window.Height / 3),
                        Flags = TextFormatFlags.WordBreak | TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom
                    },
                    new InterfaceCaption()
                    {
                        ID = "ScreenDescription", 
                        Color = DescColor,
                        Font = new Font(GameData.Font.FontFamily, 24),
                        Text = Description,
                        Rect = new Rectangle(GameData.Window.Width / 12, GameData.Window.Height / 3, GameData.Window.Width / 12 * 10, GameData.Window.Height / 3 * 2),
                        Flags = TextFormatFlags.WordBreak | TextFormatFlags.HorizontalCenter | TextFormatFlags.Top
                    }
                },
                ActionLeft = Action
            };
        }

        public static Game Owner;

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
        public Bitmap Back { get; set; }
        public Color BackColor { get; set; }
    }

    public class InterfaceGroup
    {
        public InterfaceCell[] Cells { get; set; }
        public Rectangle Rect { get; set; }
        public Bitmap Back { get; set; }
        public Color BackColor { get; set; }
        public string ID { get; set; }
        public Action ActionLeft { get; set; }
        public Action ActionRight { get; set; }
        public InterfaceCaption[] Captions { get; set; }
        public void Click(Point e, MouseButtons Button)
        {
            if (e.X >= 0 && e.X < Rect.Width &&
                e.Y >= 0 && e.Y < Rect.Height)
            {
                if (Button == MouseButtons.Left) ActionLeft?.Invoke();
                if (Button == MouseButtons.Right) 
                    ActionRight?.Invoke();
                if (Captions != null)
                {
                    for (int c = 0; c < Captions.Length; c++)
                    {
                        if (e.X > Captions[c].Rect.Left &&
                            e.X < Captions[c].Rect.Right &&
                            e.Y > Captions[c].Rect.Top &&
                            e.Y < Captions[c].Rect.Bottom)
                        {
                            if (Button == MouseButtons.Left) Captions[c].ActionLeft?.Invoke();
                            if (Button == MouseButtons.Right) Captions[c].ActionRight?.Invoke();
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
                            if (Button == MouseButtons.Left) Cells[c].ActionLeft();
                            if (Button == MouseButtons.Right) 
                                Cells[c].ActionRight();
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
                            if (Cells[c].Item != null)
                                Interface.ShowMouseHint(Cells[c].Item, new Size(200, 75));
                            break;
                        }
                        else
                        {
                            Cells[c].Hovered = false;
                            Interface.MouseHint = null;
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
        public void ActionLeft()
        {

        }
        public void ActionRight()
        {
            if (Item != null)
            {
                if (Interface.Page == InterfacePages.Inventory)
                    Item.Use();
                if (Interface.Page == InterfacePages.Shop)
                    Item.Buy();
            }
        }
    }

    public class InterfaceCaption
    {
        public string ID { get; set; } = "";
        public string Text { get; set; } = "";
        public Rectangle Rect { get; set; } = new Rectangle(0, 0, 100, 50);
        public Font Font { get; set; } = GameData.Font;
        public Color Color { get; set; } = Color.White;
        public Color HoverColor { get; set; } = Color.FromArgb(255, 73, 99, 217);
        public bool Hovered { get; set; } = false;
        public TextFormatFlags Flags { get; set; } = GameData.Flags;
        public Action ActionLeft { get; set; }
        public Action ActionRight { get; set; }
    }

    public class InterfaceColorCollection
    {
        private Color[] _colors = new Color[0];
        private string[] _id = new string[0];
        public Color this[string id]
        {
            get
            {
                for (int i = 0; i < _id.Length; i++)
                {
                    if (_id[i] == id)
                    {
                        return _colors[i];
                    }
                }
                return Color.Empty;
            }
            set
            {
                for (int i = 0; i < _id.Length; i++)
                {
                    if (_id[i] == id)
                    {
                        _colors[i] = value;
                        return;
                    }
                }

                Array.Resize<Color>(ref _colors, _colors.Length + 1);
                Array.Resize<string>(ref _id, _id.Length + 1);
                _colors[_colors.Length - 1] = value;
                _id[_id.Length - 1] = id;
            }
        }
    }
}
