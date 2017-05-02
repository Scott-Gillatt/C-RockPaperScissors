using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Player
    {
        public HandPick PlayerPick { get; set; }
    }
    class Computer
    {
        static Random r = new Random();
        public HandPick ComputerPicks { get; set; }
        public HandPick GenerateComputersPick()
        {
            int number = r.Next(0, 3);
            if (number == 0)
                return HandPick.Rock;
            if (number == 1)
                return HandPick.Paper;
            else
                return HandPick.Scissors;
        }
    }

    enum HandPick
    {
        Rock,
        Paper,
        Scissors,
        False
    }
}