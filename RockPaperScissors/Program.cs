using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadAndWriteFile;

namespace RockPaperScissors
{
    class Program
    {
        public static int playerWins = 0, computerWins = 0, tieWins = 0, totalPlays = 0;

        static List<string> runningStats = new List<string>();
        static void Main(string[] args)
        {
            FileWork.LoadGame();
            GamePlay();
        }

        private static void GamePlay()
        {
            Console.WriteLine("Lets play a game of Rock, Paper, or Scissors");
            Console.WriteLine("Are you ready? Yes or No ");
            string input = Console.ReadLine().ToLower();
            if (ValidateInput(input))
            {
                Player currentPlayer = new Player();
                Computer currentComputer = new Computer();
                RockPaperScissors(currentPlayer, currentComputer);
                while (PlayAgain())
                {
                    Console.Clear();
                    RockPaperScissors(currentPlayer, currentComputer);
                }
                EndGame();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Sorry that you don't want to play.  Maybe next time!");
                Console.ReadLine();
            }

        }

        private static void RockPaperScissors(Player currentPlayer, Computer currentComputer)
        {

            Console.Clear();
            Console.WriteLine("ROCK ON!! Lets play!");
            Console.WriteLine("Please pick 1 for Rock, 2 for Paper, or 3 for Scissors");

            currentPlayer.PlayerPick = IsNumberIsEntered(Console.ReadLine());
            currentComputer.ComputerPicks = (currentComputer.GenerateComputersPick());
            var winner = Winner(currentPlayer.PlayerPick, currentComputer.ComputerPicks);
            Stats(winner);

            Console.WriteLine(DisplayWinner(winner, currentPlayer.PlayerPick.ToString(), currentComputer.ComputerPicks.ToString()));
            runningStats.Add($"PlayerWins:{playerWins},ComputerWins:{computerWins},Ties:{tieWins},TotalPlays:{totalPlays};");
            Console.WriteLine($"\nPlayer Wins: {playerWins}, Computer Wins: {computerWins}, and Ties: {tieWins}, out of {totalPlays}");

        }

        private static bool PlayAgain()
        {
            Console.WriteLine($"Do you want to play again? Yes or No");
            string input = Console.ReadLine().ToLower();
            if (ValidateInput(input))
                return true;
            else
                return false;

        }

        private static void EndGame()
        {
            FileWork.SaveGame(runningStats);
            Console.Clear();
            Console.WriteLine($"Ok, Bye Bye. See you Next Time!!");
            Console.ReadLine();
        }

        private static HandPick ValidHandPick(int PlayerPick)
        {
            switch (PlayerPick)
            {
                case 1:
                    return HandPick.Rock;
                case 2:
                    return HandPick.Paper;
                case 3:
                    return HandPick.Scissors;
                default:
                    return HandPick.False;
            }

        }

        private static HandPick IsNumberIsEntered(string PlayerInput)
        {
            int number;
            bool result = int.TryParse(PlayerInput, out number);

            if (result)
            {
                var PlayerPick = ValidHandPick(number);
                if (PlayerPick == HandPick.False)
                {
                    Console.WriteLine($"Sorry, the number: {number} is a invalid input.");
                    Console.WriteLine($"Please Enter in 1 for Rock, 2 for Paper, or 3 for Scissors");
                    return IsNumberIsEntered(Console.ReadLine());
                }
                else
                {
                    return PlayerPick;
                }
            }
            else
            {
                Console.WriteLine($"Sorry, {PlayerInput} is not a number.\nPlease enter in 1 for Rock, 2 for Paper, or 3 for Scissors");
                return IsNumberIsEntered(Console.ReadLine());
            }



            //while (!result)
            //{
            //    if (!result)
            //    {
            //        Console.WriteLine($"Sorry, that is a invalid input.");
            //        Console.WriteLine($"Please Enter in 1 for Rock, 2 for Paper, or 3 for Scissors");
            //        result = int.TryParse(Console.ReadLine(), out number);
            //        if (result)
            //        {
            //            currentPlayer.PlayerPick = ValidHandPick(number);
            //            if (currentPlayer.PlayerPick == HandPick.False)
            //            {
            //                result = false;
            //            }
            //            else
            //            {
            //                return currentPlayer.PlayerPick;
            //            }
            //        }
            //        else
            //        {
            //            result = false;
            //        }
            //    }

            //    else if (result)
            //    {
            //        currentPlayer.PlayerPick = ValidHandPick(number);
            //        if (currentPlayer.PlayerPick == HandPick.False)
            //        {
            //            result = false;

            //        }

            //    }
            //}
        }

        private static string Winner(HandPick playersPick, HandPick computersPick)
        {

            if (playersPick == computersPick)
                return "tie";
            else if ((playersPick == HandPick.Rock && computersPick == HandPick.Scissors) || (playersPick == HandPick.Paper && computersPick == HandPick.Rock) || (playersPick == HandPick.Scissors && computersPick == HandPick.Paper))
                return "player";
            else
                return "computer";
        }

        private static string DisplayWinner(string winner, string playersPick, string computersPick)
        {
            if (winner == "player")
                return $"Great job you win!!! You picked {playersPick} and the Computer picked {computersPick}";
            else if (winner == "computer")
                return $"Sorry, Computer Won.. It picked {computersPick} and you picked {playersPick}";
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

        private static bool ValidateInput(string input)
        {
            switch (input.ToLower())
            {
                case "y":
                case "ye":
                case "yes":
                    return true;
                default:
                    return false;
            }
        }

    }
}