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
        private List<string> _words = new List<string>();
        private int _guessLeft = 0;
        #region easy
        private bool[] aLine = new bool[4] { false, false, false, false };
        private bool[] bLine = new bool[4] { false, false, false, false };
        private string[] aWords = new string[4] { "", "", "", "" };
        private string[] bWords = new string[4] { "", "", "", "" };
        #endregion easy
        #region hard
        private bool[] aHardLine = new bool[8] { false, false, false, false, false, false, false, false };
        private bool[] bHardLine = new bool[8] { false, false, false, false, false, false, false, false };
        private string[] aHardWords = new string[8] { "", "", "", "", "", "", "", "" };
        private string[] bHardWords = new string[8] { "", "", "", "", "", "", "", "" };
        #endregion hard
        Random rnd = new Random();
        Watch watch = new Watch();


        public Game()
        {
            _diffLevel = ChoseDifficultyLevel();
            RandomWordsPolupate();

        }
        public bool Start()
        {
            while (true)
            {
                Console.Clear();
                watch.StartWatch();
                Screen();
                if (IsWin())
                {
                    watch.StopWatch();
                    return Win(watch.GetTime());
                }
                else if (_guessLeft == 0)
                {
                    Console.Clear();
                    return Lose();
                }
                else
                {
                    continue;
                }
            }
        }
        public void Screen()
        {
            Board();
            int first = Guess("A");
            int second = Guess("B");
            AreTheSame(first, second);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ab">A or B Line</param>
        /// <returns>index of chosen column from line</returns>
        public int InputChoice(string ab)
        {
            Console.WriteLine($"Chose field {ab}:");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    continue;
                }
                if (input.Count() != 2)
                {
                    Console.WriteLine($"Wrong input. Try something like this: {ab}2");
                    continue;
                }
                string x = input[0].ToString().ToUpper();
                if (x != ab)
                {
                    Console.WriteLine($"First character should be {ab}");
                    continue;
                }
                int y = int.Parse(input[1].ToString());
                if (_diffLevel == Level.hard)
                {
                    if (y != 1 && y != 2 && y != 3 && y != 4 && y != 5 && y != 6 && y != 7 && y != 8)
                    {
                        Console.WriteLine("Second character should be a number 1, 2, 3, 4, 5, 6, 7, 8.");
                        continue;
                    }
                    if (aHardLine[y - 1] && ab == "A")
                    {
                        Console.WriteLine($"{input} is uncovered already. Chose another one.");
                        continue;
                    }
                    if (bHardLine[y - 1] && ab == "B")
                    {
                        Console.WriteLine($"{input} is uncovered already. Chose another one.");
                        continue;
                    }
                }
                if (_diffLevel == Level.easy)
                {
                    if (y != 1 && y != 2 && y != 3 && y != 4)
                    {
                        Console.WriteLine("Second character should be a number 1, 2, 3, 4.");
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
                }
                return y - 1;

            }
        }

        private int Guess(string guess)
        {
            int guessIndex = InputChoice(guess);
            if (guess == "A")
            {
                if (_diffLevel == Level.easy)
                {
                    aLine[guessIndex] = true;
                }
                else if (_diffLevel == Level.hard)
                {
                    aHardLine[guessIndex] = true;
                }
                //refresh
                Board();
            }
            else if (guess == "B")
            {
                if (_diffLevel == Level.easy)
                {
                    bLine[guessIndex] = true;
                }
                else if (_diffLevel == Level.hard)
                {
                    bHardLine[guessIndex] = true;
                }
                //refresh
                Board();
            }
            else
            {
                throw new Exception("Something went wrong. ;/");
            }
            return guessIndex;
        }

        private void AreTheSame(int first, int second)
        {
            if (_diffLevel == Level.easy)
            {
                if (aWords[first] == bWords[second])
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Click to continue...");
                    Console.ReadKey(true);
                    aLine[first] = false;
                    bLine[second] = false;
                    _guessLeft -= 1;
                }
            }
            if (_diffLevel == Level.hard)
            {
                if (aHardWords[first] == bHardWords[second])
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Click to continue...");
                    Console.ReadKey(true);
                    aHardLine[first] = false;
                    bHardLine[second] = false;
                    _guessLeft -= 1;
                }
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
                }

                aWords = RandomPopulate(aWords, _words);
                bWords = RandomPopulate(bWords, _words);

            }
            else if (_diffLevel == Level.hard)
            {
                List<string> temp = new List<string>();
                temp = FileManager.ReadWords("Words.txt");
                foreach (var item in numbers)
                {
                    _words.Add(temp[item]);
                }

                aHardWords = RandomPopulate(aHardWords, _words);
                bHardWords = RandomPopulate(bHardWords, _words);
            }
            else
            {
                throw new Exception("Jeszcze score!!!");
            }
        }

        private string[] RandomPopulate(string[] array, List<string> collection)
        {
            int r = 0;
            foreach (var item in collection)
            {
                while (true)
                {
                    r = rnd.Next(array.Count());
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
                    _guessLeft = 10;
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
            string A1 = "X", A2 = "X", A3 = "X", A4 = "X", A5 = "X", A6 = "X", A7 = "X", A8 = "X", B1 = "X", B2 = "X", B3 = "X", B4 = "X", B5 = "X", B6 = "X", B7 = "X", B8 = "X";
            Console.Clear();
            ScoreBoard();
            if (_diffLevel == Level.easy)
            {
                A1 = IsTrue(0, aLine, aWords);
                A2 = IsTrue(1, aLine, aWords);
                A3 = IsTrue(2, aLine, aWords);
                A4 = IsTrue(3, aLine, aWords);

                B1 = IsTrue(0, bLine, bWords);
                B2 = IsTrue(1, bLine, bWords);
                B3 = IsTrue(2, bLine, bWords);
                B4 = IsTrue(3, bLine, bWords);
            }
            if (_diffLevel == Level.hard)
            {
                A1 = IsTrue(0, aHardLine, aHardWords);
                A2 = IsTrue(1, aHardLine, aHardWords);
                A3 = IsTrue(2, aHardLine, aHardWords);
                A4 = IsTrue(3, aHardLine, aHardWords);
                A5 = IsTrue(4, aHardLine, aHardWords);
                A6 = IsTrue(5, aHardLine, aHardWords);
                A7 = IsTrue(6, aHardLine, aHardWords);
                A8 = IsTrue(7, aHardLine, aHardWords);

                B1 = IsTrue(0, bHardLine, bHardWords);
                B2 = IsTrue(1, bHardLine, bHardWords);
                B3 = IsTrue(2, bHardLine, bHardWords);
                B4 = IsTrue(3, bHardLine, bHardWords);
                B5 = IsTrue(4, bHardLine, bHardWords);
                B6 = IsTrue(5, bHardLine, bHardWords);
                B7 = IsTrue(6, bHardLine, bHardWords);
                B8 = IsTrue(7, bHardLine, bHardWords);
            }

            Console.WriteLine(Line);
            Console.WriteLine($"    Level: {_diffLevel}");
            Console.WriteLine($"    Guess Left: {_guessLeft}");
            Console.WriteLine();
            if (_diffLevel == Level.easy)
            {
                Console.WriteLine("      1 2 3 4");
                Console.WriteLine($"    A {A1} {A2} {A3} {A4}");
                Console.WriteLine($"    B {B1} {B2} {B3} {B4}");
            }
            if (_diffLevel == Level.hard)
            {
                Console.WriteLine("      1 2 3 4 5 6 7 8");
                Console.WriteLine($"    A {A1} {A2} {A3} {A4} {A5} {A6} {A7} {A8}");
                Console.WriteLine($"    B {B1} {B2} {B3} {B4} {B5} {B6} {B7} {B8}");
            }
            Console.WriteLine(Line);
        }

        public void ScoreBoard()
        {
            try
            {
                List<ScoreObject> scores = FileManager.DeserializeScore<ScoreObject>();
                if (_diffLevel == Level.easy || _diffLevel == Level.hard)
                {
                    Console.WriteLine("LatestScores");
                    if (scores.Count() <= 4)
                    {
                        foreach (var item in scores)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Console.WriteLine(scores[scores.Count() - 1 - i]);
                        }
                    }
                }
                if (_diffLevel == Level.score)
                {
                    foreach (var item in scores)
                    {
                        Console.WriteLine(item);
                    }
                }
                Console.WriteLine();
            }
            catch (Exception)
            {

                Console.WriteLine("You are first Player :)");
            }
        }


        private bool IsWin()
        {
            if (_diffLevel == Level.easy)
            {
                foreach (var item in aLine)
                {
                    if (!item)
                    {
                        return false;
                    }
                }
            }
            if (_diffLevel == Level.hard)
            {
                foreach (var item in aHardLine)
                {
                    if (!item)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private double Score(long time, int guessLeft, Level level)
        {
            double score = 0;
            if (level == Level.hard)
            {
                score += 100;
            }
            score += guessLeft * 10 + 3;
            score -= time / 10;
            return score;
        }

        private bool Win(long time)
        {
            while (true)
            {
                double score = Score(time, _guessLeft, _diffLevel);
                Console.WriteLine("Congratulation! You have successfully completed the game.");
                Console.WriteLine($"Your score is: {score}");
                FileManager.SaveHighScore(_guessLeft, time, score);
                Console.WriteLine("Wanna try again? Y/N");
                string reply = Console.ReadKey(true).Key.ToString();
                if (reply.Length > 1)
                {
                    continue;
                }
                if (reply == "y" || reply == "Y")
                {
                    return true;
                }
                if (reply == "n" || reply == "N")
                {
                    return false;
                }
                Console.Clear();


            }
        }

        private bool Lose()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Unfortunately, you failed.");
                Console.WriteLine("Wanna try again? Y/N");
                string reply = Console.ReadKey(true).Key.ToString();
                if (reply.Length > 1)
                {
                    continue;
                }
                if (reply == "y" || reply == "Y")
                {
                    return true;
                }
                if (reply == "n" || reply == "N")
                {
                    return false;
                }
                Console.Clear();
            }
        }
    }
}
