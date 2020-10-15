using Bow.Application;
using System;
using Xunit;

namespace Bow.Applicaton.Tests
{
    public class GameTests
    {
        [Fact]
        public void ExampleGame()
        {
            var game = new Game();
            AddRoleAndCheck(game, 1, 1, false);
            AddRoleAndCheck(game, 4, 5, false);

            AddRoleAndCheck(game, 4, 9, false);
            AddRoleAndCheck(game, 5, 14, false);

            AddRoleAndCheck(game, 6, 20, false);
            AddRoleAndCheck(game, 4, 24, false);

            AddRoleAndCheck(game, 5, 34, false);
            AddRoleAndCheck(game, 5, 39, false);

            AddRoleAndCheck(game, 10, 59, false);

            AddRoleAndCheck(game, 0, 59, false);
            AddRoleAndCheck(game, 1, 61, false);

            AddRoleAndCheck(game, 7, 68, false);
            AddRoleAndCheck(game, 3, 71, false);

            AddRoleAndCheck(game, 6, 83, false);
            AddRoleAndCheck(game, 4, 87, false);

            AddRoleAndCheck(game, 10, 107, false);

            AddRoleAndCheck(game, 2, 111, false);
            AddRoleAndCheck(game, 8, 127, false);
            AddRoleAndCheck(game, 6, 133, true);
        }

        [Fact]
        public void FirstRollInvalidTooBig()
        {
            var game = new Game();
            var exception = Assert.Throws<ArgumentException>(() => game.AddRoll(11));
            Assert.Equal("Wrong PinRolled Count 11. Should be between 1 and 10.", exception.Message);
        }

        [Fact]
        public void FirstRollInvalidTooSmall()
        {
            var game = new Game();
            var exception = Assert.Throws<ArgumentException>(() => game.AddRoll(-1));
            Assert.Equal("Wrong PinRolled Count -1. Should be between 1 and 10.", exception.Message);
        }

        [Fact]
        public void FirstRollOne()
        {
            var game = new Game();
            game.AddRoll(1);
            Assert.Equal(1, game.TotalScore());
            Assert.False(game.Over());
        }

        [Fact]
        public void FirstRollZero()
        {
            var game = new Game();
            game.AddRoll(0);
            Assert.Equal(0, game.TotalScore());
            Assert.False(game.Over());
        }

        [Fact]
        public void IsOverAfter10Frames()
        {
            var game = new Game();
            for (int i = 0; i < 19; i++)
            {
                AddRoleAndCheck(game, 0, 0, false);
            }
            AddRoleAndCheck(game, 0, 0, true);
        }

        [Fact]
        public void IsOverAfter10FramesWithStrikes()
        {
            var game = new Game();
            AddRoleAndCheck(game, 10, 10, false);
            AddRoleAndCheck(game, 10, 30, false);
            AddRoleAndCheck(game, 10, 60, false);
            AddRoleAndCheck(game, 10, 90, false);
            AddRoleAndCheck(game, 10, 120, false);
            AddRoleAndCheck(game, 10, 150, false);
            AddRoleAndCheck(game, 10, 180, false);
            AddRoleAndCheck(game, 10, 210, false);
            AddRoleAndCheck(game, 10, 240, false);
            AddRoleAndCheck(game, 10, 270, false);
            AddRoleAndCheck(game, 10, 290, false);
            AddRoleAndCheck(game, 10, 300, true);
        }

        [Fact]
        public void NoRoll()
        {
            var game = new Game();
            Assert.Equal(0, game.TotalScore());
            Assert.False(game.Over());
        }

        [Fact]
        public void SpareAddNextRole()
        {
            var game = new Game();
            game.AddRoll(5);
            game.AddRoll(5);
            game.AddRoll(1);
            Assert.Equal(12, game.TotalScore());
            Assert.False(game.Over());
        }

        [Fact]
        public void ThreeRolls()
        {
            var game = new Game();
            game.AddRoll(1);
            game.AddRoll(1);
            game.AddRoll(1);
            Assert.Equal(3, game.TotalScore());
            Assert.False(game.Over());
        }

        [Fact]
        public void TwoRolls()
        {
            var game = new Game();
            game.AddRoll(1);
            game.AddRoll(1);
            Assert.Equal(2, game.TotalScore());
            Assert.False(game.Over());
        }

        private static void AddRoleAndCheck(Game game, int pins, int totalScore, bool over)
        {
            game.AddRoll(pins);
            Assert.Equal(totalScore, game.TotalScore());
            Assert.Equal(over, game.Over());
        }
    }
}