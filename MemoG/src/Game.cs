using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MemoG.src;

namespace MemoG
{
    public class Game
    {
        private Level _diffLevel = Level.score;
        private const string Line = "------------------------------";
        //zmienic na private
        public List<string> _words = new List<string>();
        private int _guessLeft = 0;
        private bool[] aLine = new bool[4] { false, false, false, false };
        private bool[] bLine = new bool[4] { false, false, false, false };
        //zmień na private
        public string[] aWords = new string[4] { "", "", "", "" };
        public string[] bWords = new string[4] { "", "", "", "" };
        Random rnd = new Random();


        public Game()
        {
            _diffLevel = ChoseDifficultyLevel();
            RandomWordsPolupate();

        }
        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Screen();
            }
        }
        public void Screen()
        {
            Board();
            Guess("A");
            Guess("B");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ab">A or B Line</param>
        /// <returns>index of chosen column from line</returns>
        public int InputChoice(string ab)
        {
            ab = ab.ToUpper();
            Console.WriteLine($"Chose field {ab}:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    continue;
                }
                string x = input[0].ToString();
                if (x != ab)
                {
                    Console.WriteLine($"First character should be {ab}");
                    continue;
                }
                if (input.Count() != 2)
                {
                    Console.WriteLine($"Wrong input. Try something like this: {ab}2");
                    continue;
                }
                int y = int.Parse(input[1].ToString());
                if (y != 1 && y != 2 && y != 3 && y != 4)
                {
                    Console.WriteLine("Second character should be a number 1, 2, 3 or 4");
                    continue;
                }
                if (aLine[y - 1] && ab == "A")
                {
                    Console.WriteLine($"{input} is uncovered already. Chose another one.");
                    continue;
                }
                if (bLine[y - 1] && ab == "B")
                {
                    Console.WriteLine($"{input} is uncovered already. Chose another one.");
                    continue;
                }
                return y - 1;

            }
        }

        private void Guess(string guess)
        {
            int guessIndex = InputChoice(guess);
            if (guess == "A")
            {
                aLine[guessIndex] = true;
                //refresh
                Board();
            }
            else if (guess == "B")
            {
                bLine[guessIndex] = true;
                //refresh
                Board();
            }
            else
            {
                throw new Exception("Something went wrong. ;/");
            }
        }

        /// <summary>
        /// populate both lines (A and B) randomly without repeat
        /// Musze jeszcze zrobić dla harda
        /// </summary>
        private void RandomWordsPolupate()
        {
            Console.WriteLine("Populate Data in progress...");
            int count = 0;
            if (_diffLevel == Level.easy)
            {
                count = 4;
            }
            else if (_diffLevel == Level.hard)
            {
                count = 8;
            }

            int[] numbers = new int[count];
            int r = 0;
            while (count > 0)
            {
                r = rnd.Next(100);
                if (!numbers.Contains(r))
                {
                    numbers[count - 1] = r;
                    count -= 1;
                }
                
            }

            if (_diffLevel == Level.easy)
            {
                List<string> temp = new List<string>();
                temp = FileManager.ReadWords("Words.txt");
                foreach (var item in numbers)
                {
                    _words.Add(temp[item]);
                    Console.WriteLine(item);
                }

                aWords = RandomPopulate(aWords, _words);
                bWords = RandomPopulate(bWords, _words);

            }
            else if (_diffLevel == Level.hard)
            {

            }
        }

        private string[] RandomPopulate(string[] array, List<string> collection)
        {
            int r = 0;
            foreach (var item in collection)
            {
                while (true)
                {
                    r = rnd.Next(4);
                    if (array[r] == "")
                    {
                        array[r] = item;
                        break;
                    }
                }
            }
            return array;
        }

        
        
        /// <summary>
        /// Chosing between difficulty and looking on score
        /// </summary>
        /// <returns>Level enum type</returns>
        private Level ChoseDifficultyLevel()
        {
            Console.WriteLine("Choose diffuculty Level:");
            Console.WriteLine("1 - I am beginer");
            Console.WriteLine("2 - Challenge me!");
            Console.WriteLine("3 - I just want to see highest score");

            string reply = Console.ReadKey(true).Key.ToString();

            switch (reply)
            {
                case "D1":
                    Console.WriteLine("Easy game for beginer.");
                    _guessLeft = 10;
                    return Level.easy;
                case "D2":
                    Console.WriteLine("Ummm, ok...");
                    _guessLeft = 15;
                    return Level.hard;
                case "D3":
                    Console.WriteLine("");
                    return Level.score;
                default:
                    Console.WriteLine("You are true beginer! Easy mode ON");
                    return Level.easy;
            }
        }
        /// <summary>
        /// change state for indexed place
        /// </summary>
        /// <param name="index">column index</param>
        /// <param name="line">boolean array</param>
        /// <returns></returns>
        private string IsTrue(int index, bool[] boolLine, string[] wordsLine )
        {
            if (boolLine[index])
            {
                return wordsLine[index];
            }
            else
            {
                return "X";
            }
        }

        public void Board()
        {
            Console.Clear();
            string A1 = IsTrue(0, aLine, aWords);
            string A2 = IsTrue(1, aLine, aWords);
            string A3 = IsTrue(2, aLine, aWords);
            string A4 = IsTrue(3, aLine, aWords);

            string B1 = IsTrue(0, bLine, bWords);
            string B2 = IsTrue(1, bLine, bWords);
            string B3 = IsTrue(2, bLine, bWords);
            string B4 = IsTrue(3, bLine, bWords);

            Console.WriteLine(Line);
            Console.WriteLine($"    Level: {_diffLevel}");
            Console.WriteLine($"    Guess Left: {_guessLeft}");
            Console.WriteLine();
            Console.WriteLine("      1 2 3 4");
            Console.WriteLine($"    A {A1} {A2} {A3} {A4}");
            Console.WriteLine($"    B {B1} {B2} {B3} {B4}");
            Console.WriteLine(Line);
        }

        public void ScoreBoard()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }


        //te dwie poniżej chyba do usunięcia
        List<string> ChoseWords()
        {
            List<string> words = ReadWords();

            List<string> chosenWords = new List<string>();
            List<int> chosenNumbers = new List<int>();



            return words;
        }

        public static List<string> ReadWords()
        {
            List<string> words = new List<string>();
            string pathToFile = Path.Combine(Environment.CurrentDirectory, "Words.txt");
            string file = File.ReadAllText(pathToFile);
            var split = file.Split('\n');
            foreach (var item in split)
            {
                words.Add(item);
            }

            return words;
        }
    }
}
