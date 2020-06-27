using System;
using System.Threading;
using System.Threading.Tasks;

namespace EssenceOfMagic
{
    public static class GarbageCollector
    {
        public static void Start()
        {
            Task.Run(() =>
            {
                DateTime LastGC = DateTime.Now;
                while (true)
                {
                    if ((DateTime.Now - LastGC).TotalMilliseconds >= CollectorDelay)
                    {
                        LastGC = DateTime.Now;
                        GC.Collect();
                    }
                    Thread.Sleep(200);
                }
            });
        }
        public static int CollectorDelay = 10000;
    }
}
