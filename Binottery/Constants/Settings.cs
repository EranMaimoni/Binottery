namespace Binottery.Constants
{
    public static class Settings
    {
        public const int LotteryNumbersAmount = 6;
        public const int PlayerGuessesAmount = 5;
        public const int BoardRows = 3;
        public const int BoardColumns = 9;
        public const int InitInputNumber = -1;
        public const int ConsoleSeperatorLineSize = 31;
        public const int ConsoleSeperatorLongLineSize = 67;
        public const int CreditForCorrectGuess = 1;
        public const int WonCreditMultiplier = 2;
        public const int MinRowNumberFactor = 0;
        public const int MaxRowNumberFactor = 9;
        public const int ColumnNumberJump = 10;

        public const string SavedGameFileName = "game.xml";
        public const string InputNumberText = "Select Number [0-89]";
        public const string ErrorSeperator = "----Error----";
        public const string CorrectGuessSign = "*+*";
        public const string InCorrectGuessSign = "*-*";
        public const char SeperatorChar = '-';
    }
}
