using System;
using System.Collections.Generic;

namespace Core
{
    public class BowlingGame
    {
        public Player[] Players = new Player[2];

        public BowlingGame()
        {
        }

        public BowlingGame(string player1Symbols, string player2Symbols)
        {
            Players[0] = new Player(player1Symbols);
            Players[1] = new Player(player2Symbols);
        }
    }

    public class Player
    {
        public List<Turn> Turns = new List<Turn>();

        public int TotalScore
        {
            get
            {
                int result = 0;

                foreach (Turn turn in Turns)
                {
                    result += turn.Score;
                }

                return result;
            }
        }

        public Player(string symbols)
        {
            string[] split = symbols.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string symbol in split)
            {
                Turns.Add(new Turn(symbol));
            }
        }
    }

    public class Turn
    {
        // Pins knocked down in each (try)
        public int Try1 { get; set; } = 0;
        public int Try2 { get; set; } = 0;

        public int Score
        {
            get => 0;
        }

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
                string[] parts = symbol.Split('/', StringSplitOptions.RemoveEmptyEntries);
                Try1 = (parts.Length != 0) ? int.Parse(parts[0].ToString()) : 0;
                Try2 = (parts.Length == 2) ? int.Parse(parts[1].ToString()) : 0;
            }
        }
    }
}
