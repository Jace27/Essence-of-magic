using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EssenceOfMagic
{
    public class Save
    {
        public World World { get; set; }
        public int GameTime_Ticks { get; set; }
        public int GameTime_Seconds { get; set; }
        public Camera Camera { get; set; }
    }
}
