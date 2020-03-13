using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfMagic
{
    public class World
    {
        public SurfaceBlock[][] Ground { get; set; }
        public GameObject[] Objects { get; set; }
        public Creature[] Creatures { get; set; }
        public Player[] Players { get; set; }
    }
}
