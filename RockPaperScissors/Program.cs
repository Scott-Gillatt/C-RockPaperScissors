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
            int number;
            bool result = int.TryParse(Console.ReadLine(), out number);
            if (!result)
            {
                while (!result)
                {
                    Console.WriteLine($"Sorry, that is a invalid input.");
                    Console.WriteLine($"Please Enter in 1 for Rock, 2 for Paper, or 3 for Scissors");
                    result = int.TryParse(Console.ReadLine(), out number);
                    if (result)
                    {
                        currentPlayer.PlayerPick = ValidInput(number);
                        if (currentPlayer.PlayerPick == HandPick.False)
                        {
                            result = false;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            else
            {
                currentPlayer.PlayerPick = ValidInput(number);
                if(currentPlayer.PlayerPick == HandPick.False)
                {

                }
            }

            //currentPlayer.PlayerPick = number;
            currentComputer.ComputerPicks = ((HandPick)currentComputer.GenerateComputersPick());
            string winner = Winner(currentPlayer, currentComputer);
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

        private static HandPick ValidInput(int PlayerPick)
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
                    //Console.WriteLine($"Sorry, {PlayerPick} is not a correct number\n please 1 for Rock, 2 for Paper, or 3 for Scissors");
                    return HandPick.False;
            }

        }



        private static string Winner(Player playersPick, Computer computersPick)
        {

            if (playersPick.PlayerPick == computersPick.ComputerPicks)
                return "tie";
            else if ((playersPick.PlayerPick == HandPick.Rock && computersPick.ComputerPicks == HandPick.Scissors) || (playersPick.PlayerPick == HandPick.Paper && computersPick.ComputerPicks == HandPick.Rock) || (playersPick.PlayerPick == HandPick.Scissors && computersPick.ComputerPicks == HandPick.Paper))
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