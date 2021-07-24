using Binottery.Constants;
using Binottery.Log;
using Binottery.Utilities.Serializer;
using Binottery.View;
using System;
using System.IO;
using System.Linq;

namespace Binottery.Model
{
    public class GameEngine
    {
        /// <summary>
        /// State class, holding the current data representing the state of the game
        /// </summary>
        private State _state;

        /// <summary>
        /// Board class, holding the board data and methods
        /// </summary>
        private Board _board;

        /// <summary>
        /// Player class, holding the player data and methods
        /// </summary>
        private Player _player;

        /// <summary>
        /// Lottery Numbers, holding the generated lottery numbers
        /// </summary>
        private int[] _lotteryNumbers;

        /// <summary>
        /// Action Manager class, responsible for actions and states
        /// </summary>
        private ActionManager _actionManager;

        /// <summary>
        /// Serializer used to load and save the game
        /// </summary>
        private IGameSerializer _serializer;

        /// <summary>
        /// Constructor
        /// </summary>
        public GameEngine()
        {
            Init();
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void Start()
        {
            this._state.GameState = this._actionManager.GetStartState(this._serializer.HasGameSaved());
            LogManager.Log.Debug("On Start | GameState=" + this._state.GameState.ToString());
            GameConsole.PrintOptions(this._actionManager[this._state.GameState]);
            this.Run();
        }

        /// <summary>
        /// Initialize the local variables
        /// </summary>
        private void Init()
        {
            LogManager.Log.Debug("On Init");
            this._serializer = new XMLGameSerializer();
            this._actionManager = new ActionManager();
            this._state = new State() { GameState = GameState.Start };
            this._board = new Board();
            this._player = new Player();
            ExceptionManager.Instance.OnError += new ErrorEventHandler(OnErrorOccured);
        }

        /// <summary>
        /// Engine flow method, called after each action 
        /// </summary>
        private void Run()
        {
            PlayerChoice choice = GameConsole.GetPlayerChoice();
            GameAction action = this.ParsePlayerChoice(choice);

            LogManager.Log.Debug("On Run | " + choice.ToString());

            switch (action)
            {
                case GameAction.New:
                    {
                        this.NewGame();
                        GameConsole.ClearScreen();
                        GameConsole.PrintState(this._state);
                        GameConsole.PrintOptions(this._actionManager[this._state.GameState]);
                        Run();
                        break;
                    }
                case GameAction.Continue:
                    {
                        this.Load();
                        GameConsole.ClearScreen();
                        GameConsole.PrintState(this._state);
                        GameConsole.PrintOptions(this._actionManager[this._state.GameState]);
                        Run();
                        break;
                    }
                case GameAction.Show:
                    {
                        if (!this._board.Initialized)
                        {
                            this._board.CreateNew();
                            this._state = this.CreateGameState();
                        }

                        GameConsole.ClearScreen();
                        GameConsole.PrintState(this._state);
                        GameConsole.PrintOptions(this._actionManager[this._state.GameState]);
                        Run();
                        break;
                    }
                case GameAction.InputNumber:
                    {
                        if (this._actionManager.AvaliableForInput(this._state.GameState))
                        {
                            VerificationResult res = this.VerifyNumber(choice.InputNumber);
                            if (res == VerificationResult.Verified)
                            {
                                this._player.AddGuess(choice.InputNumber);
                                this.UpdateCredit(choice.InputNumber);
                                this._state = this.CreateGameState();
                                this._state.GameState = this._actionManager.CalculateGameState(this._state);
                                GameConsole.ClearScreen();
                                GameConsole.PrintState(this._state);
                                if (this._state.GameState == GameState.EndGame)
                                {
                                    if (this._player.Won)
                                        GameConsole.PrintWonMessage();

                                    this.EndGame();
                                }

                                GameConsole.PrintOptions(this._actionManager[this._state.GameState]);
                            }
                            else
                            {
                                GameConsole.PrintInvalidNumberInput(res);
                            }
                        }
                        else
                        {
                            GameConsole.PrintInvalidMessage();
                        }

                        Run();
                        break;
                    }
                case GameAction.End:
                    {
                        this.EndGame(true);
                        GameConsole.PrintOptions(this._actionManager[this._state.GameState]);
                        Run();
                        break;
                    }
                case GameAction.Exit:
                    {
                        GameConsole.PrintOpeningMessage(GameState.Exit);
                        this.ExitGame();
                        break;
                    }
                case GameAction.Invalid:
                    {
                        GameConsole.PrintInvalidMessage();
                        Run();
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// Updates the player's credit based on the given number
        /// </summary>
        /// <param name="guess">guess of the player</param>
        private void UpdateCredit(int guess)
        {
            this._player.Won = this._lotteryNumbers.Count(x => this._player.Guesses.Contains(x)) == Settings.PlayerGuessesAmount;

            if (this._player.Won)
            {
                this._player.SetCredit(this._player.Credit*Settings.WonCreditMultiplier);
            }
            else if (this._lotteryNumbers.Contains(guess))
            {
                this._player.AddCredit(Settings.CreditForCorrectGuess);
            }
        }

        /// <summary>
        /// parse the player choice and return the equivalent action
        /// </summary>
        /// <param name="choice">player choice</param>
        /// <returns>game action based on the player choice</returns>
        private GameAction ParsePlayerChoice(PlayerChoice choice)
        {
            GameAction ga = GameAction.Invalid;
            if (choice.Action == string.Empty && choice.InputNumber != Settings.InitInputNumber)
            {
                ga = GameAction.InputNumber;
            }
            else
            {
                if (false == Enum.TryParse(choice.Action, true, out ga))
                    ga = GameAction.Invalid;
            }

            return ga;
        }

        /// <summary>
        /// verifies the given number (player guess) and returns a result 
        /// </summary>
        /// <param name="guess">player guess</param>
        /// <returns>result type based on the input</returns>
        private VerificationResult VerifyNumber(int guess)
        {
            VerificationResult ret = VerificationResult.Verified;
            if (this._player.Guesses.Contains(guess))
            {
                ret = VerificationResult.DuplicateGuess;
            }

            if (!this._board.BoardNumbers.Contains(guess))
            {
                ret = VerificationResult.NumberNotInBoard;
            }

            return ret; 
        }

        /// <summary>
        /// exit the game, if the game state is in progress it'll be save to the disk
        /// </summary>
        private void ExitGame()
        {
            if (this._state.GameState == GameState.InProgress)
            {
                this.Save(this._state);
            }

            Environment.Exit(0);
        }

        /// <summary>
        /// ends the current game
        /// </summary>
        /// <param name="directRequest">true if the player requsted to end the game, false in case he completed his guesses</param>
        private void EndGame(bool directRequest = false)
        {
            this._state.GameState = this._actionManager.GetStartState(this._serializer.HasGameSaved());
            
            if (directRequest)
                GameConsole.PrintScore(this._state.PlayerCredit);

            GameConsole.PrintEndingMessage(GameState.EndGame);
            GameConsole.ClearScreen();
        }

        /// <summary>
        /// creates a new game
        /// </summary>
        private void NewGame()
        {
            this._board = new Board();
            this._player = new Player();
            this._board.CreateNew();
            this.CreateLotteryNumbers(_board.GetRandomNumbers(Settings.LotteryNumbersAmount));
            this._state = this.CreateGameState();
            this._state.GameState = GameState.Started;
        }

        /// <summary>
        /// creates lottery numbers array from the given array
        /// </summary>
        /// <param name="numbers">array of lottery numbers</param>
        private void CreateLotteryNumbers(int[] numbers)
        {
            int amt = Settings.LotteryNumbersAmount;
            this._lotteryNumbers = new int[amt];
            try
            {
                Array.Copy(numbers, this._lotteryNumbers, amt);
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

        /// <summary>
        /// creates a state object from the current game data
        /// </summary>
        /// <returns>state object</returns>
        private State CreateGameState()
        {
            State state = new State()
            {
                BoardNumbers = this._board.BoardNumbers,
                LotteryNumbers = this._lotteryNumbers,
                PlayerCredit = this._player.Credit,
                PlayerGuesses = this._player.Guesses,
                GameState = this._state.GameState
            };

            return state;
        }

        /// <summary>
        /// save the current state of the game to the disk
        /// </summary>
        /// <param name="state">current game state</param>
        private void Save(State state)
        {
            try
            {
                this._serializer.Save(state);
            }
            catch (Exception e)
            {
                ExceptionManager.Instance.RaiseException(e);
            }
        }

        /// <summary>
        /// load a game from the disk if exist and populate the game's objects
        /// </summary>
        private void Load()
        {
            State state = null;
            try
            {
                state = _serializer.Load();
            }
            catch (Exception e)
            {
                ExceptionManager.Instance.RaiseException(e);
            }

            if (state != null)
            {
                this._board = new Board(state.BoardNumbers);
                this._player = new Player(state.PlayerCredit, state.PlayerGuesses);
                this.CreateLotteryNumbers(state.LotteryNumbers);
                this._state.GameState = state.GameState;
                this._state = this.CreateGameState();
            }
        }

        /// <summary>
        /// method that registered to the ExceptionManager 'OnError' event to handle errors
        /// </summary>
        /// <param name="sender">ExceptionManager</param>
        /// <param name="e">Exception parameter</param>
        private void OnErrorOccured(object sender, ErrorEventArgs e)
        {
            GameConsole.RaiseException(e.GetException());
            this.ExitGame();
        }
    }
}
