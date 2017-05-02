using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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