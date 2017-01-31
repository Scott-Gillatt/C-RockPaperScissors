using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello What is your name? ");
            string name = Console.ReadLine();
            Console.WriteLine($"Hello {name} its nice to meet you!");
            int playerWins = 0, computerWins = 0, tieWins = 0;
            GamePlay(name, playerWins, computerWins, tieWins);
        }

        private static string GamePlay(string name, int playerWins, int computerWins, int tieWins)
        {
            Console.WriteLine("Lets play a game of Rock, Paper, or Scissors");
            Console.WriteLine("Are you ready? Yes or No ");
            string input = Console.ReadLine().ToLower();
            if (input == "yes" || input == "y")
            {
                return RockPaperScissors(name, playerWins, computerWins, tieWins);
            }
            else
            {
                return $"Sorry to hear {name}, that you don't want to play.  Maybe next time!";
            }
        }

        private static string RockPaperScissors(string name, int playerWins, int computerWins, int tieWins)
        {
            Console.Clear();
            Console.WriteLine("ROCK ON!! Lets play!");
            Console.WriteLine("Please pick Rock, Paper, or Scissors");
            string playersPick = ValidInput(Console.ReadLine());
            string computerPick = ComputersPick();
            string winner = Winner(playersPick, computerPick);


            Console.WriteLine(DisplayWinner(winner, playersPick, computerPick, name));
            if (winner == "player")
                playerWins++;
            else if (winner == "computer")
                computerWins++;
            else
                tieWins++;
            Console.WriteLine($"Player Wins: {playerWins}, Computer Wins: {computerWins}, and Ties: {tieWins}");
            Console.WriteLine($"Do you want to play again {name}? Yes or No");
            string input = Console.ReadLine().ToLower();
            if (input == "yes" || input == "y")
            {
                Console.Clear();
                return RockPaperScissors(name, playerWins, computerWins, tieWins);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Ok, Bye Bye {name}, See you Next Time!!");
                return Console.ReadLine();
            }
        }

        private static string ValidInput(string theirPick)
        {
            if (theirPick.ToLower() == "rock" || theirPick.ToLower() == "paper" || theirPick.ToLower() == "scissors")
            {
                return theirPick;
            }
            else
            {
                Console.WriteLine($"Sorry, {theirPick} is a invalid input.");
                Console.WriteLine("Please Enter in Rock, Paper, or Scissors");
                return ValidInput(Console.ReadLine());
            }
        }

        private static string ComputersPick()
        {
            Random r = new Random();
            int number = r.Next(1, 4);

            if (number == 1)
                return "Rock";
            else if (number == 2)
                return "Paper";
            else
                return "Scissors";
        }

        private static string Winner(string playersPick, string computersPick)
        {
            string playersPickLower = playersPick.ToLower();
            if (playersPickLower == computersPick.ToLower())
                return "tie";
            else if ((playersPickLower == "rock" && computersPick == "Scissors") || (playersPickLower == "paper" && computersPick == "Rock") || (playersPickLower == "scissors" && computersPick == "Rock"))
                return "player";
            else
                return "computer";
        }

        private static string DisplayWinner(string winner, string playersPick, string computersPick, string name)
        {
            if (winner == "player")
                return $"Great job {name} you win!!! You picked {playersPick} and the Computer picked {computersPick}";
            else if (winner == "computer")
                return $"Sorry {name}, Computer Won.. It picked {computersPick} and you picked {playersPick}";
            else
                return "Games Result was a Tie.  Try Again.";
        }

    }
}
