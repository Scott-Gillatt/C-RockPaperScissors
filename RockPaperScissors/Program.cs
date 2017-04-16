using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        public static int playerWins = 0, computerWins = 0, tieWins = 0, totalPlays = 0;
        static Random r = new Random();
        static List<string> runningStats = new List<string>();
        static void Main(string[] args)
        {
            ReadAndWriteFile.FileWork.LoadGame();
            GamePlay();
        }

        private static void GamePlay()
        {
            Console.WriteLine("Lets play a game of Rock, Paper, or Scissors");
            Console.WriteLine("Are you ready? Yes or No ");
            string input = Console.ReadLine().ToLower();
            if (ValidateInput(input))
            {
                RockPaperScissors();
                while (PlayAgain())
                {
                    Console.Clear();
                    RockPaperScissors();
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

        private static void RockPaperScissors()
        {
            Console.Clear();
            Console.WriteLine("ROCK ON!! Lets play!");
            Console.WriteLine("Please pick Rock, Paper, or Scissors");
            string playersPick = ValidInput(Console.ReadLine());
            string computerPick = ComputersPick();
            string winner = Winner(playersPick, computerPick);
            Stats(winner);

            Console.WriteLine(DisplayWinner(winner, playersPick, computerPick));
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
            ReadAndWriteFile.FileWork.SaveGame(runningStats);
            Console.Clear();
            Console.WriteLine($"Ok, Bye Bye. See you Next Time!!");
            Console.ReadLine();
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

namespace ReadAndWriteFile
{
    class FileWork
    {
        static string filePath = $@"C:\Users\{Environment.UserName}\Desktop\RockPaperScissors.txt";
        public static void LoadGame()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                string lastLine = lines[lines.Length - 1];

                List<int> lastStats = new List<int>();
                //for (int x = 0; x < lastLine.Length; x++)
                //{
                //    string total = "";
                //    if ((lastLine[x] > 47) && lastLine[x] < 58)
                //    {

                //        Console.Write($"{lastLine[x]} is a Integer ");
                //    }
                //    else
                //    {
                //        Console.WriteLine($"{lastLine[x]} is a string");
                //    }
                //}
                for (int x = 0; x < lastLine.Length; x++)
                {
                    if (lastLine[x] == 58)
                    {
                        StringBuilder temp = new StringBuilder();
                        for (int j = x + 1; j < lastLine.Length + 1; j++)
                        {
                            if ((lastLine[j] == 44) || (lastLine[j] == 59))
                            {
                                lastStats.Add(Convert.ToInt32(temp.ToString()));
                                x += (j - x);
                                break;
                            }
                            else
                            {
                                temp.Append(lastLine[j]);
                            }
                        }
                    }
                }
                RockPaperScissors.Program.playerWins = lastStats[0];
                RockPaperScissors.Program.computerWins = lastStats[1];
                RockPaperScissors.Program.tieWins = lastStats[2];
                RockPaperScissors.Program.totalPlays = lastStats[3];
            }
            else
            {
                CreateGame();
            }
        }

        private static void CreateGame()
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine("PlayerWins:0,ComputerWins:0,Ties:0,TotalPlays:0;");
            }
        }

        public static void SaveGame(List<string> Data)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                foreach(string data in Data)
                {
                    sw.WriteLine(data);
                }
            }
        }
    }
}
