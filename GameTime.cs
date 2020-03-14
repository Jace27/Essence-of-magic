﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace EssenceOfMagic
{
    public static class GameTime
    {
        public static void Start()
        {
            Ticker = Task.Run(() =>
            {
                int delaysum = 0;
                while (true)
                {
                    while (isFreezed) Thread.Sleep(10);
                    Thread.Sleep(TimeDelay);
                    delaysum += TimeDelay;
                    Tick++;
                    try { OnTick(); } catch { }
                    if (delaysum >= 1000)
                    {
                        delaysum = 0;
                        Second++;
                        try { OnSecond(); } catch { }
                    }
                }
            });
        }
        public static void Freeze(int ticks)
        {
            isFreezed = true;
            Task.Run(() => { 
                Thread.Sleep(ticks * TimeDelay); 
                isFreezed = false; 
            });
        }
        public async static void WaitForTick()
        {
            int curtick = Tick;
            await Task.Run(() =>
            {
                while (curtick == Tick)
                    Thread.Sleep(10);
            });
        }
        public async static void WaitForSecond()
        {
            int cursec = Second;
            await Task.Run(() =>
            {
                while (cursec == Second)
                    Thread.Sleep(10);
            });
        }
        public static int Tick = 0;
        public static int Second = 0;
        public static int TimeDelay = 40;
        public static bool isFreezed = false;
        private static Task Ticker;

        public delegate void TickEventHandler();
        public static event TickEventHandler OnTick;
        public static event TickEventHandler OnSecond;

        public static void Dispose()
        {
            Ticker.Dispose();
        }
    }
}