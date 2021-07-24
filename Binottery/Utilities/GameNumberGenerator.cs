using System;
using System.Collections.Generic;
using System.Linq;

namespace Binottery.Utilities
{
    public static class GameNumberGenerator
    {
        private static Random Rand = new Random(DateTime.Now.Ticks.GetHashCode());

        /// <summary>
        /// method that generates an array of random numbers based on the given inputs
        /// </summary>
        /// <param name="amount">amount of numbers to generate</param>
        /// <param name="min">min number</param>
        /// <param name="max">max number</param>
        /// <returns>array of random numbers</returns>
        public static int[] GenerateNumbers(int amount, int min, int max)
        {
            int[] ret = null;
            try
            {
                List<int> temp = Enumerable.Range(min, max-min).ToList();

                ret = new int[amount];
                for (int i=0; i<amount; i++)
                {
                    int val = temp[Rand.Next(0, temp.Count-1)];
                    ret[i] = val;
                    temp.Remove(val);
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

            return ret;
        }
    }
}
