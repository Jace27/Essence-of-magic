using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfMagic
{
    public class Weapon
    {
        public Weapon()
        {

        }

        private double _damage = 1;
        public double Damage
        {
            get { return _damage; }
            set
            {
                if (value < 0)
                    _damage = 0;
                else
                    _damage = value;
            }
        }
        public DamageType DamageType { get; set; }
        private double _weight = 1;
        public double Weight
        {
            get { if (_weight < 0) return 0; else return _weight; }
            set { _weight = value; }
        }

        public Weapon Clone()
        {
            Weapon temp = (Weapon)this.MemberwiseClone();
            temp.DamageType = DamageType;
            return temp;
        }
    }
}
