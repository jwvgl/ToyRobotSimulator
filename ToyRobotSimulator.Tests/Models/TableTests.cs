using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Tests.Models
{
    public class TableTests
    {
        private readonly Table _table = new Table(5, 5);

        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(4, 4, true)]
        [InlineData(0, -1, false)]
        [InlineData(5, 0, false)]
        public void ShouldValidatePositionCorrectly(int x, int y, bool expected)
        {
            Assert.Equal(expected, _table.IsValidPosition(x, y));
        }
    }
}
