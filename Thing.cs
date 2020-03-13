using System;
using System.Drawing;

namespace EssenceOfMagic
{
    public class Item
    {
        public Item() { }

        public string Name { get; set; }
        public string Description { get; set; }
        public Texture Texture { get; set; }
        public int Count { get; set; }
        public bool Stackable { get; set; }
    }
}
