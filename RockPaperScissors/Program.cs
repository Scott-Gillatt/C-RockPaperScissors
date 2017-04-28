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


            var playersPick = ValidInput(Convert.ToInt16(Console.ReadLine()));

            //if(!int.TryParse()


            currentPlayer.PlayerPick = playersPick;
            currentComputer.ComputerPicks = ((HandPick) currentComputer.GenerateComputersPick());
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
            //switch (theirPick.ToLower())
            //{
            //    case "r":
            //    case "rock":
            //    case "ro":
            //    case "roc":
            //        return "Rock";
            //    case "p":
            //    case "pa":
            //    case "pap":
            //    case "pape":
            //    case "paper":
            //        return "Paper";
            //    case "s":
            //    case "sc":
            //    case "sci":
            //    case "scis":
            //    case "sciss":
            //    case "scisso":
            //    case "scissor":
            //    case "scissors":
            //        return "Scissors";
            //    default:
            //        Console.WriteLine($"Sorry, {theirPick} is a invalid input.");
            //        Console.WriteLine("Please Enter in Rock, Paper, or Scissors");
            //        return ValidInput(Console.ReadLine());
            //}
            switch (PlayerPick) 
            {
                case 1:
                    return HandPick.Rock;
                case 2:
                    return HandPick.Paper;
                case 3:
                    return HandPick.Scissors;
                default:
                    //int.tryParse() should clean this up.
                    Console.WriteLine($"Sorry, {PlayerPick} is a invalid input.");
                    Console.WriteLine($"Please Enter in 1 for Rock, 2 for Paper, or 3 for Scissors");
                    return ValidInput(Convert.ToInt16(Console.ReadLine()));
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
            int number = r.Next(0,3);
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
        Scissors
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
                foreach (string data in Data)
                {
                    sw.WriteLine(data);
                }
            }
        }
    }
}
