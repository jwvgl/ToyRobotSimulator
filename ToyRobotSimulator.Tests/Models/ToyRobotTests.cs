using ToyRobotSimulator.Enums;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Tests.Models
{
    public class ToyRobotTests
    {
        private readonly ToyRobot _robot;

        public ToyRobotTests()
        {
            _robot = new ToyRobot(new Table(5, 5));
        }

        [Fact]
        public void ShouldFailToPlaceOffTable()
        {
            var result = _robot.Place(-1, -1, Facing.SOUTH);
            Assert.False(result.Success);
            Assert.Contains(Message.INVALID_COORDINATE, result.ToString());
        }

        [Fact]
        public void ShouldPlaceOnTable()
        {
            var result = _robot.Place(0, 0, Facing.SOUTH);
            Assert.True(result.Success);
            Assert.Equal("OK", result.ToString());
        }

        [Fact]
        public void ShouldIgnoreMoveOffTable()
        {
            _robot.Place(0, 0, Facing.SOUTH);
            var result = _robot.Move();
            Assert.False(result.Success);
            Assert.Null(result.ToString());
            Assert.Contains("0,0,SOUTH", _robot.Report());
        }

        [Fact]
        public void ShouldMoveOnTable()
        {
            _robot.Place(0, 0, Facing.NORTH);
            var result = _robot.Move();
            Assert.True(result.Success);
            Assert.Equal("OK", result.ToString());
            Assert.Contains("0,1,NORTH", _robot.Report());
        }

        [Fact]
        public void ShouldMoveOnTableWithoutFalling()
        {
            _robot.Place(0, 0, Facing.SOUTH);
            _robot.Move();
            _robot.Left();
            var result = _robot.Move();
            Assert.True(result.Success);
            Assert.Equal("OK", result.ToString());
            Assert.Contains("1,0,EAST", _robot.Report());
        }

        [Fact]
        public void ShouldTurnCorrectly()
        {
            _robot.Place(1, 1, Facing.NORTH);
            _robot.Left();
            Assert.Contains("1,1,WEST", _robot.Report());

            _robot.Right();
            Assert.Contains("1,1,NORTH", _robot.Report());
        }

        [Fact]
        public void ShouldReportCorrectly()
        {
            _robot.Place(4, 4, Facing.NORTH);
            var result = _robot.Report();
            Assert.Equal("Current position: 4,4,NORTH", result.ToString());
        }
    }
}
