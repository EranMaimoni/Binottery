using Binottery.Constants;
using Binottery.Model;
using System;
using System.IO;
using System.Linq;

namespace Binottery.View
{
    /// <summary>
    /// class that responsible for printing to console and reciving user input
    /// </summary>
    public static class GameConsole
    {
        public static void PrintOptions(GameActionOptions gao)
        {
            PrintOpeningMessage(gao.State);

            Console.WriteLine();

            PrintSelectionOpenMessage();

            PrintHorizontalSeperator(Settings.ConsoleSeperatorLineSize);

            foreach (var action in gao.Options)
            {
                Console.WriteLine(action);
            }

            PrintHorizontalSeperator(Settings.ConsoleSeperatorLineSize);

            PrintEndingMessage(gao.State);
        }

        public static void RaiseException(Exception e)
        {
            Console.WriteLine(Settings.ErrorSeperator);
            Console.WriteLine(e.Message);
            Console.WriteLine(Settings.ErrorSeperator);
        }

        public static void PrintSelectionOpenMessage()
        {
            Console.WriteLine("Please select option: ");
        }

        public static void PrintHorizontalSeperator(int lineSize)
        {
            Console.WriteLine(new string(Settings.SeperatorChar, lineSize));
        }

        public static void PrintEndingMessage(GameState state)
        {
            switch (state)
            {
                case GameState.EndGame:
                    {
                        Console.WriteLine("----END----");
                        Console.ReadLine();
                        break;
                    }

                default:
                    break;
            }
        }

        public static void PrintOpeningMessage(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                case GameState.Continue:
                    {
                        Console.WriteLine("-----------Binottery-----------");
                        break;
                    }
                case GameState.Exit:
                    {
                        Console.WriteLine("-----------Thank You-----------");
                        break;
                    }
                default:
                    break;

            }
        }

        public static void PrintInvalidMessage()
        {
            Console.WriteLine("Not a valid input, please select from the options above");
        }

        public static void PrintInvalidNumberInput(VerificationResult res)
        {
            switch (res)
            {
                case VerificationResult.NumberNotInBoard:
                    {
                        Console.WriteLine("This is not a valid number, please select one from the board");
                        break;
                    }
                case VerificationResult.DuplicateGuess:
                    {
                        Console.WriteLine("You already guess this number, please select different one");
                        break;
                    }
                default:
                    break;
            }
        }

        public static PlayerChoice GetPlayerChoice()
        {
            PlayerChoice ret = new PlayerChoice();
            string input = string.Empty;
            try
            {
                input = Console.ReadLine().ToLower().Trim();
            }
            catch (IOException e)
            {
                ExceptionManager.Instance.RaiseException(e);
            }

            if (int.TryParse(input, out int num))
            {
                ret.InputNumber = num;
            }
            else
            {
                ret.Action = input;
            }

            return ret;
        }

        public static void ClearScreen()
        {
            Console.Clear();
        }

        public static void PrintState(State state)
        {
            Console.WriteLine();
            Console.WriteLine("Board");
            PrintHorizontalSeperator(Settings.ConsoleSeperatorLongLineSize);
            PrintBoard(state);
            PrintHorizontalSeperator(Settings.ConsoleSeperatorLongLineSize);
            PrintScore(state.PlayerCredit);
        }

        public static void PrintScore(int credit)
        {
            Console.WriteLine("Credit: " + credit.ToString());
        }

        public static void PrintBoard(State state)
        {
            try
            {
                for (int i = 0; i < Settings.BoardRows; i++)
                {
                    for (int j = 0; j < Settings.BoardColumns; j++)
                    {
                        int num = state.BoardNumbers[i + j * Settings.BoardRows];
                        string sign = string.Empty;
                        if (state.PlayerGuesses.Contains(num))
                        {
                            if (state.LotteryNumbers.Contains(num))
                            {
                                sign = Settings.CorrectGuessSign;
                            }
                            else
                            {
                                sign = Settings.InCorrectGuessSign;
                            }
                        }

                        Console.Write((num.ToString() + sign).PadRight(8));
                    }

                    Console.WriteLine();
                }
            }
            catch (IndexOutOfRangeException e)
            {
                ExceptionManager.Instance.RaiseException(e);
            }
            catch (Exception e)
            {
                ExceptionManager.Instance.RaiseException(e);
            }
        }

        public static void PrintWonMessage()
        {
            Console.WriteLine("Congradulation! you won the lottery!!!");
        }
    }
}
