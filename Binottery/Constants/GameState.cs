namespace Binottery.Constants
{
    public enum GameState
    {
        /// <summary>
        /// initiale state of the game
        /// </summary>
        Start,

        /// <summary>
        /// state that represent a loaded game at initialization
        /// </summary>
        Continue,

        /// <summary>
        /// state of a new/loaded game
        /// </summary>
        Started,

        /// <summary>
        /// state represents a game in progress - player already guess a number
        /// </summary>
        InProgress,

        /// <summary>
        /// state of ended game or player finised guessing
        /// </summary>
        EndGame,

        /// <summary>
        /// state of exit game
        /// </summary>
        Exit
    }
}
