using System;
using System.Collections.Generic;

namespace EssenceOfMagic
{
    public class Armor
    {
        public Armor() { }

        public Texture Sprite { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value))
                    _name = value;
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value))
                    _description = value;
            }
        }
        private double _protection = 0;
        public double Protection
        {
            get { return _protection; }
            set
            {
                if (value < 0)
                    _protection = 0;
                else
                    _protection = value;
            }
        }
        public Resists Resists { get; set; } = new Resists();
        private double _weight = 1;
        public double Weight
        {
            get { if (_weight < 0) return 0; else return _weight; }
            set { _weight = value; }
        }

        public Armor Clone()
        {
            Armor temp = (Armor)this.MemberwiseClone();
            temp.Resists = Resists;
            if (Sprite != null)
                temp.Sprite = Sprite.Clone();
            return temp;
        }
    }
}
