using System.Collections.Generic;

namespace Core
{
    public class TennisGame
    {
        public Dictionary<int, string> scoreNames = new Dictionary<int, string>()
            {
                {0, "love" },
                {1, "fifteen" },
                {2, "thirty" },
                {3, "fourty" }
            };

        public int Player1Score { get; set; } = 0;
        public string Player1ScoreText { get => scoreNames[Player1Score]; }
        
        public int Player2Score { get; set; } = 0;
        public string Player2ScoreText { get => scoreNames[Player2Score]; }
        
        public string Score { get => CalculateGameScore(); }

        public TennisGame()
        {
        }

        private string CalculateGameScore()
        {
            if ((Player1Score == Player2Score) && Player1Score >= 3)
            {
                return "deuce";
            }
            else if (Player1Score >= 4 && (Player1Score - Player2Score == 1))
            {
                return "Player1 Advantage";
            }
            else if (Player2Score >= 4 && (Player2Score - Player1Score == 1))
            {
                return "Player2 Advantage";
            }
            else if (Player1Score >= 4 && (Player1Score - Player2Score >= 2))
            {
                return "Player1 Wins";
            }
            else if (Player2Score >= 4 && (Player2Score - Player1Score >= 2))
            {
                return "Player2 Wins";
            }
            else
            {
                return $"{Player1ScoreText} - {Player2ScoreText}";
            }
        }
    }
}
