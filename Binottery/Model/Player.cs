using System.Collections.Generic;

namespace Binottery.Model
{
    public class Player
    {
        private int _credit;
        private List<int> _guesses;


        public Player() 
        {
            this._credit = 0;
            this._guesses = new List<int>();
            this.Won = false;
        }

        public Player(int credit, List<int> guesses) 
        {
            this._credit = credit;
            this._guesses = new List<int>(guesses);
            this.Won = false;
        }

        public int Credit
        {
            get
            {
                return this._credit;
            }
        }

        public List<int> Guesses
        {
            get
            {
                return this._guesses;
            }
        }

        public bool Won { get; set; }

        public void AddCredit(int val)
        {
            this._credit += val; 
        }

        public void SetCredit(int val)
        {
            this._credit = val;
        }

        public void AddGuess(int val)
        {
            this._guesses.Add(val);
        }
    }
}
