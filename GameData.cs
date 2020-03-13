using System;
using System.Drawing;
using System.Text.Json;
using System.IO;

namespace EssenceOfMagic
{
    public static class GameData
    {
        // == ИГРОВОЕ ==
        public static World World;

        // == ТЕХНИЧЕСКОЕ ==
        public static JsonSerializerOptions JsonOpts = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        public static Size BlockSize = new Size(64, 64);
        public static Size Window = new Size(960, 640);

        // == ИГРОВЫЕ ФАЙЛЫ ==
        public static string TextureFolder = Environment.CurrentDirectory + "\\Resources\\Textures";
        public static string AnimationFolder = Environment.CurrentDirectory + "\\Resources\\Textures\\Animations";
        public static string TriggersFolder = Environment.CurrentDirectory + "\\Resources\\Triggers";
        public static string WorldFolder = Environment.CurrentDirectory + "\\Resources\\Map";
        public static string ObjectsFolder = Environment.CurrentDirectory + "\\Resources\\Creatures";
        public static string WeaponFolder = Environment.CurrentDirectory + "\\Resources\\Weapon";
        public static string ArmorFolder = Environment.CurrentDirectory + "\\Resources\\Armor";

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
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources\\Creatures"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Resources\\Creatures");
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources\\Armor"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Resources\\Armor");
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources\\Weapon"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Resources\\Weapon");
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Resources\\Triggers"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Resources\\Triggers");
        }
    }
}