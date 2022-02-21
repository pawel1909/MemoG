using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MemoG.src
{
    class Watch
    {
        public Stopwatch counter = new Stopwatch();
        public long time = 0;
        public void StartWatch()
        {
            time = 0;
            counter.Start();
        }
        public void StopWatch()
        {
            counter.Stop();
            time = counter.ElapsedMilliseconds / 1000;
        }

        public long GetTime() => time;
    }
}
