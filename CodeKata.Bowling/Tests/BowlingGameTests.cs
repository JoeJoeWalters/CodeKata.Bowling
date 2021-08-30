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
        [InlineData("0/0", 0, 0)]
        [InlineData("5/5", 5, 5)]
        [InlineData("2/5", 2, 5)]
        public void Symbol_To_Score_Checks(string symbol, int try1, int try2)
        {
            // ARRANGE
            var turn = new Turn(symbol);

            // ACT

            // ASSERT
            turn.Try1.Should().Be(try1);
            turn.Try2.Should().Be(try2);
        }
    }
}
