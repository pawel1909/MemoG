using System;
using System.IO;
using System.Collections.Generic;
using MemoG.src;
using System.Linq;

namespace MemoG
{
    class Program
    {
        static void Main(string[] args)
        {
            //do poprawy
            string pathToFile = Path.Combine(Environment.CurrentDirectory, @"Data\Words.txt");

            Console.WriteLine(pathToFile);
            Console.WriteLine();
            //string x = FileManager.PathToDataFile("test.txt");
            //Console.WriteLine(x);
            Game app = new Game();
            app.Start();

            //Console.WriteLine(app._words.Count());
            //foreach (var item in app._words)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine();
            //printCol(app.aWords);
            //Console.WriteLine();
            //printCol(app.bWords);


            //app.Start();

        }

        static void printCol(string[]? x)
        {
            foreach (var item in x)
            {
                Console.WriteLine(item);
            }
        }
    }
}
