using Binottery.Constants;

namespace Binottery.Model
{
    public class PlayerChoice
    {
        public PlayerChoice()
        {
            this.InputNumber = Settings.InitInputNumber;
            this.Action = string.Empty;
        }

        public int InputNumber { get; set; }
        public string Action { get; set; }

        public override string ToString()
        {
            return "Action= [" + this.Action + "] | InputNumber= [" + this.InputNumber.ToString() + "]";
        }
    }
}
