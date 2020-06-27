using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EssenceOfMagic
{
    public class Item : GameObject
    {
        public Item() { }

        public string Description { get; set; }
        public int Count { get; set; }
        public int Durability { get; set; }
        public int DurabilityMax { get; set; }
        public bool isUsable { get; set; }
        public int DurabilitySpending { get; set; }
        public bool Stackable { get; set; }
        public int Cost { get; set; }
        public ItemType Type { get; set; }
        public void Use()
        {
            Action?.Invoke();
            Durability -= DurabilitySpending;
            if (Durability <= 0)
            {
                Durability = DurabilityMax;
                Count--;
            }
        }
        public void Buy()
        {
            BuyAction?.Invoke(this);
        }
        [JsonIgnore]
        new public Action Action { get; set; }
        [JsonIgnore]
        new public ItemAction1 BuyAction { get; set; }

        [JsonIgnore]
        public bool isDragging { get; set; }
        public bool isDraggable { get; set; }

        public Item Clone()
        {
            Item output = (Item)this.MemberwiseClone();
            output.Action = Action;
            output.BuyAction = BuyAction;
            output.Type = Type;
            return output;
        }
    }

    public enum ItemType
    {
        Thing, Weapon, Armor
    }

    public static class Items
    {
        public static Item[] Index = new Item[0];
        public static void Init()
        {
            Index = null;
            FileInfo fi = new FileInfo(GameData.ObjectsFolder + "\\items.json");
            using (StreamReader sr = new StreamReader(fi.FullName))
                Index = JsonSerializer.Deserialize<Item[]>(sr.ReadToEnd());
        }
        public static Item Get(string ID)
        {
            for (int i = 0; i < Index.Length; i++)
                if (Index[i].ID == ID)
                    return Index[i];
            return null;
        }
    }
}
