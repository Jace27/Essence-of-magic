using System.IO;
using System.Text.Json;

namespace EssenceOfMagic
{
    public class Player : Creature
    {
        public Player()
        {
            ID = "player";
        }

        new public Player Clone()
        {
            Player temp = (Player)this.MemberwiseClone();
            return temp;
        }
    }

    public static class Players
    {
        /// <summary>
        /// Массив зарегистрированных игроков
        /// </summary>
        public static Player[] Index;
        /// <summary>
        /// Инициализировать список игроков
        /// </summary>
        public static void Init()
        {
            Index = null;
            FileInfo fi = new FileInfo(GameData.ObjectsFolder + "\\players.json");
            using (StreamReader sr = new StreamReader(fi.FullName))
                Index = JsonSerializer.Deserialize<Player[]>(sr.ReadToEnd());
        }
        /// <summary>
        /// Получить игрока по ID
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>Игрок или null</returns>
        public static Player Get(string ID)
        {
            for (int i = 0; i < Index.Length; i++)
                if (Index[i].ID == ID)
                    return Index[i];
            return null;
        }
    }
}
