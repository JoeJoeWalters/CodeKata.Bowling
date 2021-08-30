using Core;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class BowlingGameTests
    {

        [Theory]
        [InlineData("X", 10, 0)]
        [InlineData("-10", 0, 10)]
        [InlineData("9-", 9, 0)]
        [InlineData("--", 0, 0)]
        [InlineData("0/", 0, 10)]
        [InlineData("5/", 5, 5)]
        [InlineData("2/", 2, 8)]
        [InlineData("-/", 0, 10)]
        public void Symbol_To_Score_Checks(string symbol, int try1, int try2)
        {
            // ARRANGE
            var frame = new Frame(symbol);

            // ACT

            // ASSERT
            frame.Scores[0].Should().Be(try1);
            if (frame.Scores.Count > 1)
                frame.Scores[1].Should().Be(try2);
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("X X X X X X X X X X X X", 300)] // Maximum score, (12 rolls, 12 strikes = 10 frames * 30 points = 300)
        [InlineData("-- X X X X X X X X X X X", 270)] // No score on first frame, strikes on all remaining including 2x bonus throws at end
        [InlineData("9- 9- 9- 9- 9- 9- 9- 9- 9- 9-", 90)] // (20 rolls: 10 pairs of 9 and miss) = 10 frames * 9 points = 90
        [InlineData("5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/5", 150)] // (21 rolls: 10 pairs of 5 and spare, with a final 5) = 10 frames * 15 points = 150
        [InlineData("-/ 5/ -- -- -- -- -- -- -- --", 20)] // No pins then spare in first frame, 5 pins then spare in next frame then no score on remaining = 20
        [InlineData("X -/ 5/ -- -- -- -- -- -- --", 40)] // Strike, so 10 + next 2 balls, next two balls are 0 and 10 so 20 + 0 + 10 + 5 + 5 = 40
        public void ScoreText_To_Score(string symbols, int expectedScore)
        {
            // ARRANGE
            BowlingGame game = new BowlingGame(symbols, "");

            // ACT

            // ASSERT
            game.Players[0].TotalScore.Should().Be(expectedScore);

        }
    }
}
