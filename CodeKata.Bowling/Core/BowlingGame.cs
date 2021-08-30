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
    }
}
