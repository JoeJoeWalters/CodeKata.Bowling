using System;
using System.Collections.Generic;
using System.Linq;

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
        public List<Frame> Frames = new List<Frame>();

        public int TotalScore
        {
            get
            {
                List<KeyValuePair<int, int>> pins = new List<KeyValuePair<int, int>>();

                int position = 0;
                for (int frameId = 0; frameId < Frames.Count; frameId ++)
                {
                    Frame Frame = Frames[frameId];
                    position = frameId + 1;

                    if (Frame.Scores[0] == 10)
                    {
                        pins.Add(new KeyValuePair<int, int>(position, Frame.Scores[0]));
                    }
                    else
                    {
                        pins.Add(new KeyValuePair<int, int>(position, Frame.Scores[0]));
                        pins.Add(new KeyValuePair<int, int>(position, Frame.Scores[1]));
                    }

                    if (Frame.Scores.Count > 2)
                    {
                        pins.Add(new KeyValuePair<int, int>(position, Frame.Scores[2]));
                    }
                }

                int result = 0;
                position = 0;
                while (position < pins.Count)
                {
                    int frameId = pins[position].Key;
                    int pinScore = pins[position].Value;

                    // Last game?
                    if (frameId == 10)
                    {
                        // Add the sum of all remaining throws (should be only up to 2 more)
                        result += pins.Where(x => x.Key >= 10).Sum(x => x.Value);

                        // Must be finished
                        break;
                    }
                    else if (frameId < 10)
                    {
                        result += pinScore;

                        // Strike? Add next two balls
                        if (pinScore == 10)
                        {
                            if (position < pins.Count - 1)
                                result += pins[position + 1].Value;

                            if (position < pins.Count - 2)
                                result += pins[position + 2].Value;
                        }
                    }
                    
                    position++;
                }

                return result;
            }
        }

        public Player(string symbols)
        {
            string[] split = symbols.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string symbol in split)
            {
                Frames.Add(new Frame(symbol));
            }
        }
    }

    public class Frame
    {
        // Pins knocked down in each (try)
        public List<int> Scores = new List<int>();

        public Frame()
        {

        }

        public Frame(string symbol)
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
                string remainder = symbol.Replace("-", "");
                if (remainder == "/")
                {
                    Scores.Add(10);
                }
                else
                {
                    int.TryParse(remainder, out int try2Check);
                    Scores.Add(try2Check);
                }
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
