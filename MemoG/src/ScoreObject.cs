using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoG.src
{
    public class ScoreObject
    {
        public string Name { get; set; }
        public int GuessLeft { get; set; }
        public long Time { get; set; }
        public double Score { get; set; }

        public ScoreObject(string name, 
            int guessLeft, 
            long time, 
            double score)
        {
            Name = name;
            GuessLeft = guessLeft;
            Time = time;
            Score = score;
        }
        public ScoreObject() { }

        public override string ToString()
        {
            return $"Name: {Name} -- Time left: {Time} -- Score: {Score}";
        }
    }
}
