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
                List<KeyValuePair<int, int>> pins = new List<KeyValuePair<int, int>>();

                for (int turnId = 0; turnId < Turns.Count; turnId ++)
                {
                    Turn turn = Turns[turnId];
                    int position = turnId + 1;

                    if (turn.Scores[0] == 10)
                    {
                        pins.Add(new KeyValuePair<int, int>(position, turn.Scores[0]));
                    }
                    else
                    {
                        pins.Add(new KeyValuePair<int, int>(position, turn.Scores[0]));
                        pins.Add(new KeyValuePair<int, int>(position, turn.Scores[1]));
                    }

                    if (turn.Scores.Count > 2)
                    {
                        pins.Add(new KeyValuePair<int, int>(position, turn.Scores[2]));
                    }
                }

                int result = 0;

                for (int position = 0; position < pins.Count; position++)
                {
                    int turnId = pins[position].Key;
                    int pinScore = pins[position].Value;

                    result += pinScore;

                    // Last game?
                    if (turnId == 10)
                    {
                    }
                    else
                    {
                        // Strike? Add next two balls
                        if (pinScore == 10)
                        {
                            if (position < pins.Count - 1)
                                result += pins[position + 1].Value;

                            if (position < pins.Count - 2)
                                result += pins[position + 2].Value;
                        }
                    }
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
        public List<int> Scores = new List<int>();

        public Turn()
        {

        }

        public Turn(string symbol)
        {
            symbol = symbol.ToLower();
            if (symbol == "x")
            {
                Scores.Add(10);
            }
            else if (symbol.EndsWith("-"))
            {
                int.TryParse(symbol.Replace("-", ""), out int try1Check);
                Scores.Add(try1Check);
                Scores.Add(0);
            }
            else if (symbol.StartsWith("-"))
            {
                Scores.Add(0);
                int.TryParse(symbol.Replace("-", ""), out int try2Check);
                Scores.Add(try2Check);
            }
            else if (symbol.Contains("/"))
            {
                string[] parts = symbol.Split('/', StringSplitOptions.RemoveEmptyEntries);
                Scores.Add((parts.Length != 0) ? int.Parse(parts[0].ToString()) : 0);
                Scores.Add(10 - Scores[0]);
                if (parts.Length == 2)
                    Scores.Add(int.Parse(parts[1]));
            }
        }
    }
}
