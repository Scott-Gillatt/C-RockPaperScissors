﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        static int playerWins = 0, computerWins = 0, tieWins = 0, totalPlays = 0;
        static Random r = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("How Many Times do you want to play Rock, Paper, Scissors?");
            PlayManyTimes(Console.ReadLine());
            //Console.WriteLine("Hello What is your name? ");
            //string name = Console.ReadLine();
            //Console.WriteLine($"Hello {name} its nice to meet you!");
            //GamePlay(name);
        }

        private static string GamePlay(string name)
        {
            Console.WriteLine("Lets play a game of Rock, Paper, or Scissors");
            Console.WriteLine("Are you ready? Yes or No ");
            string input = Console.ReadLine().ToLower();
            if (input == "yes" || input == "y" || input == "ye")
            {
                return RockPaperScissors(name);
            }
            else
            {
                return $"Sorry to hear {name}, that you don't want to play.  Maybe next time!";
            }
        }

        private static string RockPaperScissors(string name)
        {
            Console.Clear();
            Console.WriteLine("ROCK ON!! Lets play!");
            Console.WriteLine("Please pick Rock, Paper, or Scissors");
            string playersPick = ValidInput(Console.ReadLine());
            string computerPick = ComputersPick();
            string winner = Winner(playersPick, computerPick);
            Stats(winner);

            Console.WriteLine(DisplayWinner(winner, playersPick, computerPick, name));

            Console.WriteLine($"Player Wins: {playerWins}, Computer Wins: {computerWins}, and Ties: {tieWins}, out of {totalPlays}");
            Console.WriteLine($"Do you want to play again {name}? Yes or No");
            string input = Console.ReadLine().ToLower();
            if (input == "yes" || input == "y" || input == "ye")
            {
                Console.Clear();
                return RockPaperScissors(name);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Ok, Bye Bye {name}, See you Next Time!!");
                return Console.ReadLine();
            }
        }

        private static void RockPaperScissors()
        {
            string playersPick = ComputersPick();
            string computerPick = ComputersPick();
            string winner = Winner(playersPick, computerPick);
            Stats(winner);
        }

        private static string ValidInput(string theirPick)
        {
            switch (theirPick.ToLower())
            {
                case "r":
                case "rock":
                case "ro":
                case "roc":
                    return "Rock";
                case "p":
                case "pa":
                case "pap":
                case "pape":
                case "paper":
                    return "Paper";
                case "s":
                case "sc":
                case "sci":
                case "scis":
                case "sciss":
                case "scisso":
                case "scissor":
                case "scissors":
                    return "Scissors";
                default:
                    Console.WriteLine($"Sorry, {theirPick} is a invalid input.");
                    Console.WriteLine("Please Enter in Rock, Paper, or Scissors");
                    return ValidInput(Console.ReadLine());
            }
        }

        private static string ComputersPick()
        {
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

        private static int Stats(string winner)
        {
            totalPlays++;
            if (winner == "player")
                return playerWins++;
            else if (winner == "computer")
                return computerWins++;
            else
                return tieWins++;
        }

        private static void PlayManyTimes(string input)
        {
            int length = Convert.ToInt32(input);

            for (int i = 0; i < length; i++)
            {
                RockPaperScissors();
            }
            Console.WriteLine($"Player Wins: {playerWins}, Computer Wins: {computerWins}, and Ties: {tieWins}, out of {totalPlays}");
            Console.ReadLine();
        }

    }
}
