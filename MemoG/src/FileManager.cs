using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MemoG.src
{
    public static class FileManager
    {
        private static string PathToDataFile(string fileName)
        {
            string path2 = @$"Data\{fileName}";
            return Path.Combine(Environment.CurrentDirectory, path2);
        }

        public static List<string> ReadWords(string file)
        {
            List<string> words = new();
            try
            {
                words = File.ReadAllLines(PathToDataFile(file)).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return words;
        }

        public static void SaveHighScore(int guesses, int time, double score)
        {
            Console.WriteLine("Wanna save your Score? Y/N");
            while (true)
            {
                string reply = Console.ReadKey(true).Key.ToString();
                if (reply == "Y")
                {
                    break;
                }
                else if (reply != "N")
                {
                    Console.WriteLine("Y/N");
                    continue;
                }
                else
                {
                    return;
                }
            }
            Console.WriteLine("Coś dalej");
        }
    }
}
