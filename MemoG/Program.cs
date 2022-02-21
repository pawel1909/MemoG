using System;
using System.IO;
using System.Collections.Generic;
using MemoG.src;
using System.Linq;
using System.Diagnostics;

namespace MemoG
{
    class Program
    {
        static void Main(string[] args)
        {


            while (true)
            {
                Game app = new Game();
                if (app.Start())
                {

                    Console.Clear();
                    continue;
                }
                else
                {
                    Console.Clear();
                    break;
                }
            }


        }
    }
}
