using System.Collections.Generic;

namespace Core
{
    public class BowlingGame
    {
        public Player[] Players = new Player[2];

        public BowlingGame()
        {
        }
    }

    public class Player
    {
        public Turn[] Turns = new Turn[11]; // 10 turns + bonus depending on spare or strike in 10th

        public int Score
        {
            get => 0;
        }

        public Player()
        {

        }
    }

    public class Turn
    {
        // Pins knocked down in each (try)
        public int Try1 { get; set; } = 0;
        public int Try2 { get; set; } = 0;

        public Turn()
        {

        }

        public Turn(string symbol)
        {
            symbol = symbol.ToLower();
            if (symbol == "x")
            {
                Try1 = 10;
                Try2 = 0;
            }
            else if (symbol.EndsWith("-"))
            {
                int.TryParse(symbol.Replace("-", ""), out int try1Check);
                Try1 = try1Check;
                Try2 = 0;
            }
            else if (symbol.StartsWith("-"))
            {
                Try1 = 0;
                int.TryParse(symbol.Replace("-", ""), out int try2Check);
                Try2 = try2Check;
            }
            else if (symbol.Contains("/"))
            {
                Try1 = int.Parse(symbol[0].ToString());
                Try2 = int.Parse(symbol[2].ToString());
            }
        }
    }
}
