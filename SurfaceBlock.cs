using System;
using System.Drawing;

namespace EssenceOfMagic
{
    public struct SurfaceBlock
    {
        public string[] Layers { get; set; }
        //private int[] _coords;
        public Location Coords { get; set; }
        public bool Fogged { get; set; }
        public bool Darked { get; set; }
    }
}
