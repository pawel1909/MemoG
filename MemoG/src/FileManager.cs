using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace MemoG.src
{
    public static class FileManager
    {
        private static string PathToDataFile(string fileName)
        {
            string path2 = @$"Data\{fileName}";
            return Path.Combine(Environment.CurrentDirectory, path2);
        }

        public static int LongestWord()
        {
            int lenght = 0;
            List<string> words = ReadWords("Words.txt");
            foreach (var item in words)
            {
                if (item.Length > lenght)
                {
                    lenght = item.Length;
                }
            }
            return lenght;
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
            string name = "";
            List<ScoreObject> scoreObjList;
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
            while (true)
            {
                Console.WriteLine("Write your name: ");
                name = Console.ReadLine();
                if (name.Length != 3)
                {
                    Console.WriteLine("Only 3 letters available");
                    continue;
                }
                else
                {
                    name = name.ToUpper();
                    break;
                }

            }
            ScoreObject scoreObj = new ScoreObject(name, guesses, time, score);
            if (!File.Exists("Score.json"))
            {
                scoreObjList = new List<ScoreObject>();
            }
            else
            {
                scoreObjList = DeserializeScore<ScoreObject>();
            }
            scoreObjList.Add(scoreObj);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            var s = JsonSerializer.Serialize<List<ScoreObject>>(scoreObjList, options);
            File.WriteAllText("Score.json", s);

        }

        public static List<T> DeserializeScore<T>()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            string json = File.ReadAllText("Score.json");
            Console.WriteLine(json);
            List<T> s = JsonSerializer.Deserialize<List<T>>(json, options);
            

            return s;
        }
    }
}
