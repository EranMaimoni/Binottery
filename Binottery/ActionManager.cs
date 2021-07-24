using Binottery.Constants;
using Binottery.Model;
using Binottery.Utilities;
using System;
using System.Collections.Generic;

namespace Binottery
{
    public class ActionManager
    {
        private Dictionary<GameState, GameActionOptions> _stateOptions;

        public ActionManager()
        {
            this.Init();
        }

        private void Init()
        {
            this._stateOptions = new Dictionary<GameState, GameActionOptions>();
            this._stateOptions[GameState.Start] = this.GetOptions(GameState.Start);
            this._stateOptions[GameState.Continue] = this.GetOptions(GameState.Continue);
            this._stateOptions[GameState.Started] = this.GetOptions(GameState.Started);
            this._stateOptions[GameState.InProgress] = this.GetOptions(GameState.InProgress);
            this._stateOptions[GameState.EndGame] = this.GetOptions(GameState.EndGame);
        }

        /// <summary>
        /// returns the GameActionOptions object from the dictionary based on the input
        /// </summary>
        /// <param name="state">state</param>
        /// <returns>GameActionOptions object</returns>
        public GameActionOptions this[GameState state]
        {
            get
            {
                GameActionOptions ret = new GameActionOptions(state);
                if(this._stateOptions.ContainsKey(state))
                {
                    ret = this._stateOptions[state];
                }

                return ret;
            }
        }

        /// <summary>
        /// method that returns an object with the next options based on the given state
        /// </summary>
        /// <param name="state">current state</param>
        /// <returns>next options object</returns>
        private GameActionOptions GetOptions(GameState state)
        {
            GameActionOptions ret = new GameActionOptions(state);
            switch (state)
            {
                case GameState.Start:
                    {
                        ret.AddAction(GameAction.New);
                        ret.AddAction(GameAction.Show);
                        ret.AddAction(GameAction.Exit);
                        break;
                    }
                case GameState.Continue:
                    {
                        ret.AddAction(GameAction.New);
                        ret.AddAction(GameAction.Continue);
                        ret.AddAction(GameAction.Show);
                        ret.AddAction(GameAction.Exit);
                        break;
                    }
                case GameState.Started:
                case GameState.InProgress:
                    {
                        ret.AddAction(Settings.InputNumberText);
                        ret.AddAction(GameAction.New);
                        ret.AddAction(GameAction.Show);
                        ret.AddAction(GameAction.End);
                        ret.AddAction(GameAction.Exit);
                        break;
                    }
                case GameState.EndGame:
                    {
                        ret.AddAction(GameAction.New);
                        ret.AddAction(GameAction.Show);
                        ret.AddAction(GameAction.Exit);
                        break;
                    };
                default:
                    break;
            }

            return ret;
        }

        /// <summary>
        /// method that calculates the next game state based on the given state
        /// </summary>
        /// <param name="state">current state</param>
        /// <returns>next game state</returns>
        public GameState CalculateGameState(State state)
        {
            GameState ret = state.GameState;

            if(state.PlayerGuesses != null)
            {
                if (state.PlayerGuesses.Count == Settings.PlayerGuessesAmount)
                {
                    ret = GameState.EndGame;
                }
                else if (state.PlayerGuesses.Count >= 1 && state.PlayerGuesses.Count <= Settings.PlayerGuessesAmount - 1)
                {
                    ret = GameState.InProgress;
                }
            }

            return ret;
        }

        /// <summary>
        /// method the calculates the next start state based on whether there is a saved game
        /// </summary>
        /// <param name="hasSave">true if there is a saved game, otherwise false</param>
        /// <returns>next start state</returns>
        public GameState GetStartState(bool hasSave)
        {
            return hasSave? GameState.Continue : GameState.Start;
        }

        /// <summary>
        /// method that checks if the given game state is avaliable for player input
        /// </summary>
        /// <param name="gameState">current state</param>
        /// <returns>true if the state is avaliable for input</returns>
        public bool AvaliableForInput(GameState gameState)
        {
            return gameState.In(GameState.InProgress, GameState.Started);
        }
    }
}
