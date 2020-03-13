using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfMagic
{
    public enum DamageType
    {
        Sword, Magic, Spirit,
        Fire, Water, Terra, Air,
        Death, Life, Darkness, Light
    }
    public enum Pages
    {
        World, Inventory, Title, Settings
    }
    public enum Direction
    {
        Up, Down, Left, Right, Ceiling,
        UpLeft, UpRight, DownLeft, DownRight,
        Null
    }
}
