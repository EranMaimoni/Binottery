using Binottery.Constants;
using System;
using System.Collections.Generic;

namespace Binottery.Model
{
    [Serializable]
    public class State
    {
        public int[] BoardNumbers { get; set; }
        public int[] LotteryNumbers { get; set; }
        public int PlayerCredit { get; set; }
        public GameState GameState { get; set; }
        public List<int> PlayerGuesses { get; set; }
    }
}
