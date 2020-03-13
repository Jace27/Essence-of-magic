using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfMagic
{
    public class Resists
    {
        public Resists()
        {

        }

        readonly private DamageType[] Keys = new DamageType[]
        {
            DamageType.Sword,
            DamageType.Magic,
            DamageType.Spirit,
            DamageType.Fire,
            DamageType.Water,
            DamageType.Terra,
            DamageType.Air,
            DamageType.Darkness,
            DamageType.Light,
            DamageType.Death,
            DamageType.Life
        };
        readonly private double[] Values = new double[]
        {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
        public double this[DamageType key]
        {
            get
            {
                for (int i = 0; i < Keys.Length; i++)
                    if (Keys[i] == key)
                        return Values[i];
                return 0;
            }
            set
            {
                for (int i = 0; i < Keys.Length; i++)
                {
                    if (Keys[i] == key)
                    {
                        if (value < 0)
                            Values[i] = 0;
                        else if (value > 1)
                            Values[i] = 1;
                        else
                            Values[i] = value;
                        return;
                    }
                }
            }
        }
    }
}
