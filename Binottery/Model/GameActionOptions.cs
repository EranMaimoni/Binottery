using Binottery.Constants;
using System.Collections.Generic;

namespace Binottery.Model
{
    public class GameActionOptions
    {
        private HashSet<string> _options;
        private GameState _state;

        public GameActionOptions(GameState state)
        {
            this._options = new HashSet<string>();
            this._state = state;
        }

        public GameActionOptions(GameState state, HashSet<string> options)
        {
            this._options = new HashSet<string>(options);
            this._state = state;
        }

        public GameState State
        {
            get
            {
                return _state;
            }
        }

        public HashSet<string> Options 
        { 
            get
            {
                return this._options;
            }
        }

        public void AddAction(string action)
        {
            this._options.Add(action);
        }

        public void AddAction(GameAction action)
        {
            this._options.Add(action.ToString());
        }
    }
}