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
    public enum InterfacePages
    {
        Game,
        Menu,
        Inventory,
        Shop,
        Dialog
    }
    public enum Direction
    {
        Up, Down, Left, Right, Ceiling,
        UpLeft, UpRight, DownLeft, DownRight,
        Null
    }
    public enum ObjectType
    {
        Object, Creature, Player, Unknown
    }
}
