using System;
using System.Drawing;
using System.Text.Json;
using System.IO;
using System.Windows.Forms;

namespace EssenceOfMagic
{
    public delegate void ObjectAction(GameObject gameObject, int i);
    public delegate void ItemAction1(Item Item);
    public static class GameData
    {
        // == ИГРОВОЕ ==
        public static World World;
        public static TimeSpan DayLength = new TimeSpan(0, 20, 0);
        public static GameObject ItemToObject(Item Item, Location Location)
        {
            GameObject output = Item as GameObject;
            {
                output.ID = "Item";
                output.gID = DateTime.Now.Ticks.ToString() + World.Objects.CountAll.ToString();
                output.ActionLeft = (GameObject thisobj, int i) =>
                {
                    if (thisobj is Item item)
                    {
                        World.Objects.Delete(i);
                        Game.Player.Inventory.Give(item);
                    }
                };
                output.Location = Location;
                output.Size = new Size(32, 32);
            }
            return output;
        }

        // == ТЕХНИЧЕСКОЕ ==
        public static Game Game;
        public static TitleScreen TitleScreen;
        public static bool isInGame = false;
        public static JsonSerializerOptions JsonOpts = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        public static Size BlockSize = new Size(64, 64);
        public static Size Window = new Size(960, 640);
        public delegate void ThreadTransit();
        public delegate void ThreadTransitForItems(Item Item);
        public static Point Cursor = new Point(0, 0);

        public static Font Font = new Font("Segoe Script", 14f, FontStyle.Regular);
        public static TextFormatFlags Flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;

        public static void Save()
        {
            Save Save = new Save();
            Save.Camera = Game.Camera;
            Save.GameTime_Ticks = GameTime.Tick;
            Save.GameTime_Seconds = GameTime.Second;
            Save.World = World;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.CheckFileExists = false;
            sfd.Filter = "JSON Files|*.json";
            sfd.DefaultExt = "json";
            sfd.InitialDirectory = Environment.CurrentDirectory;
            sfd.AddExtension = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi = new FileInfo(sfd.FileName);
                if (!fi.Exists) fi.Create().Close();

                using (StreamWriter sw = new StreamWriter(fi.FullName, false))
                    sw.Write(JsonSerializer.Serialize<Save>(Save));

                MessageBox.Show("Успешно сохранено в директорию: " + sfd.FileName, "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            sfd.Dispose();
        }
        public static bool isLoadingGame = false;
        public static void Load()
        {
            isLoadingGame = true;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JSON Files|*.json";
            ofd.InitialDirectory = Environment.CurrentDirectory;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(ofd.FileName))
                {
                    try
                    {
                        if (Game != null) Game.Dispose();
                        Game = new Game();
                        Game.Owner = TitleScreen;
                        Save Save = JsonSerializer.Deserialize<Save>(sr.ReadToEnd());
                        World = Save.World;
                        Game.Camera = Save.Camera;
                        GameTime.Tick = Save.GameTime_Ticks;
                        GameTime.Second = Save.GameTime_Seconds;
                        Game.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("Ошибка при чтении файла, проверьте правильность формата.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            ofd.Dispose();
            isLoadingGame = false;
        }
        public static void Settings()
        {
            Settings Settings = new Settings(Game);
            Game.Invoke(new ThreadTransit(() => { Settings.ShowDialog(); }));
        }
        public static void InvokeException(Exception ex)
        {
            Game.Invoke(new ThreadTransit(() => {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }));
        }

        // == ИГРОВЫЕ ФАЙЛЫ ==
        public static string TextureFolder = Environment.CurrentDirectory + "\\Resources\\Textures";
        public static string AnimationFolder = Environment.CurrentDirectory + "\\Resources\\Textures\\Animations";
        public static string TriggersFolder = Environment.CurrentDirectory + "\\Resources\\Triggers";
        public static string WorldFolder = Environment.CurrentDirectory + "\\Resources\\Map";
        public static string ObjectsFolder = Environment.CurrentDirectory + "\\Resources\\Objects";
        public static string WeaponFolder = Environment.CurrentDirectory + "\\Resources\\Weapon";
        public static string ArmorFolder = Environment.CurrentDirectory + "\\Resources\\Armor";
        public static string SoundFolder = Environment.CurrentDirectory + "\\Resources\\Sound";

        public static void CheckFileStruct()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Saves"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Saves");
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources\\Textures"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Resources\\Textures");
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources\\Textures\\Animations"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Resources\\Textures\\Animations");
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources\\Map"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Resources\\Map");
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources\\Objects"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Resources\\Objects");
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources\\Armor"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Resources\\Armor");
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources\\Weapon"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Resources\\Weapon");
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources\\Triggers"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Resources\\Triggers");
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources\\Sound"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Resources\\Sound");
            if (!File.Exists("Settings.ini"))
                File.Create("Settings.ini");
        }

        //ДОПОЛНИТЕЛЬНЫЙ ФУНКЦИОНАЛ
        public static Rectangle HitboxConvert(Location ObjectLocation, Size ObjectSize, Size TextureSize, Rectangle Hitbox)
        {
            int x, y, w, h;
            x = (int)Math.Floor(ObjectLocation.X + (Hitbox.X / (TextureSize.Width / 100.0)) * (ObjectSize.Width / 100.0));
            y = (int)Math.Floor(ObjectLocation.Y + (Hitbox.Y / (TextureSize.Height / 100.0)) * (ObjectSize.Height / 100.0));
            w = (int)Math.Round((Hitbox.Width / (TextureSize.Width / 100.0)) * (ObjectSize.Width / 100.0), 0);
            h = (int)Math.Round((Hitbox.Height / (TextureSize.Height / 100.0)) * (ObjectSize.Height / 100.0), 0);
            Rectangle Output = new Rectangle(x, y, w, h);
            return Output;
        }
        public static void RegenGID()
        {
            for (int i = 0; i < World.Objects.CountAll; i++)
            {
                GameObject go = World.Objects.Get(i);
                if (go.gID == null || go.gID == "")
                    go.gID = DateTime.Now.Ticks.ToString() + i.ToString();
            }
        }
    }
}