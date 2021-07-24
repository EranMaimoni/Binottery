using Binottery.Constants;
using Binottery.Utilities;
using System;

namespace Binottery.Model
{
    public class Board
    {
        /// <summary>
        /// holds the board numbers
        /// </summary>
        private int[] _boardNumbers;

        /// <summary>
        /// true if the board in initialized
        /// </summary>
        private bool _initialized;


        public Board()
        {
            this._initialized = false;
        }

        /// <summary>
        /// constructor used to initialize the board with an existing board numbers
        /// </summary>
        /// <param name="numbers">board numbers</param>
        public Board(int[] numbers)
        {
            this._boardNumbers = new int[Settings.BoardRows * Settings.BoardColumns];
            
            try
            {
                Array.Copy(numbers, this._boardNumbers, numbers.Length);
                this._initialized = true;
            }
            catch (ArgumentOutOfRangeException e)
            {
                ExceptionManager.Instance.RaiseException(e);
            }
            catch (Exception e)
            {
                ExceptionManager.Instance.RaiseException(e);
            }
        }

        public int[] BoardNumbers
        {
            get
            {
                return this._boardNumbers;
            }
        }

        public bool Initialized
        { 
            get
            {
                return this._initialized;
            }
        }

        /// <summary>
        /// creates a new board
        /// </summary>
        public void CreateNew()
        {
            this.GenerateBoard();
            this._initialized = true;
        }

        /// <summary>
        /// returns a random set of numbers from the board
        /// </summary>
        /// <param name="amount">amount of random numbers to be generated</param>
        /// <returns>array of random numbers</returns>
        public int[] GetRandomNumbers(int amount)
        {
            int[] ret = null;

            if(this._initialized)
            {
                try
                {
                    int[] indexes = GameNumberGenerator.GenerateNumbers(amount, 0, this._boardNumbers.Length - 1);

                    ret = new int[amount];
                    for (int i = 0; i < amount; i++)
                    {
                        ret[i] = this._boardNumbers[indexes[i]];
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    ExceptionManager.Instance.RaiseException(e);
                }
                catch (Exception e)
                {
                    ExceptionManager.Instance.RaiseException(e);
                }
            }

            return ret;
        }

       /// <summary>
       /// generates a new board
       /// </summary>
        private void GenerateBoard()
        {
            this._boardNumbers = new int[Settings.BoardRows * Settings.BoardColumns];
            int min = Settings.MinRowNumberFactor, max = Settings.MaxRowNumberFactor;
            try
            {
                for(int i=0; i<Settings.BoardColumns; i++)
                {
                    int[] col = GameNumberGenerator.GenerateNumbers(Settings.BoardRows, min, max);
                    for (int j = 0; j < Settings.BoardRows; j++)
                    {
                        this._boardNumbers[i * Settings.BoardRows + j] = col[j];
                    }

                    min += Settings.ColumnNumberJump;
                    max += Settings.ColumnNumberJump;
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                ExceptionManager.Instance.RaiseException(e);
            }
            catch (Exception e)
            {
                ExceptionManager.Instance.RaiseException(e);
            }
        }
    }
}