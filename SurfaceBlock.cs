using System;
using System.Drawing;

namespace EssenceOfMagic
{
    public struct SurfaceBlock
    {
        public string[] Layers { get; set; }
        public Location Coords { get; set; }
        public bool Fogged { get; set; }
        public bool Darked { get; set; }
    }
}
