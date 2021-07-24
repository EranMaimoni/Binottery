namespace Binottery.Constants
{
    public enum VerificationResult
    {
        /// <summary>
        /// verified result
        /// </summary>
        Verified,

        /// <summary>
        /// the number is not on the board
        /// </summary>
        NumberNotInBoard,

        /// <summary>
        /// player already guess this number
        /// </summary>
        DuplicateGuess
    }
}