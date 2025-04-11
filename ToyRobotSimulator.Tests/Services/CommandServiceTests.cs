using ToyRobotSimulator.Models;
using ToyRobotSimulator.Services;

namespace ToyRobotSimulator.Tests.Services
{
    public class CommandServiceTests
    {
        private readonly CommandService _service;
        private readonly ToyRobot _robot;

        public CommandServiceTests()
        {
            var table = new Table(5, 5);
            _robot = new ToyRobot(table);
            _service = new CommandService(_robot);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("move")]
        [InlineData("left")]
        [InlineData("right")]
        [InlineData("report")]
        public void ShouldIgnoreCommandsBeforePlace(string cmd)
        {
            var result = _service.Process(cmd);

            Assert.False(result.Success);
            Assert.Null(result.ToString());
        }

        [Fact]
        public void ShouldFailPlaceWithInvalidCommandFormat()
        {
            var result = _service.Process("PLACE,1,1,East");

            Assert.False(result.Success);
            Assert.Contains(Message.INVALID_COMMAND, result.ToString());
            Assert.False(_robot.IsOnTable);
        }

        [Fact]
        public void ShouldPlaceWithValidCommand()
        {
            var result = _service.Process("PLACE 0,0,West");

            Assert.True(result.Success);
            Assert.Contains("OK", result.ToString());
            Assert.True(_robot.IsOnTable);
        }


        [Fact]
        public void ShouldHandleMultipleValidPlaceCommands()
        {
            _service.Process("PLACE 0,0,NORTH");
            _service.Process("PLACE 2,2,EAST");
            Assert.True(_robot.IsOnTable);

            var result = _service.Process("REPORT");
            Assert.Contains("2,2,EAST", result.ToString());
        }

        [Fact]
        public void ShouldFailPlaceWithInvalidCoordinatesFormat()
        {
            var result = _service.Process("PLACE 1,1,NorthEast");

            Assert.False(result.Success);
            Assert.Contains(Message.NOT_PLACED, result.ToString());
            Assert.False(_robot.IsOnTable);
        }

        [Fact]
        public void ShouldIgnoreInvalidPlaceCoordinates()
        {
            var result1 = _service.Process("PLACE 6,6,NORTH");
            Assert.False(_robot.IsOnTable);
            Assert.False(result1.Success);

            var result2 = _service.Process("REPORT");
            Assert.False(result2.Success);
            Assert.Null(result2.ResultMessage);
            Assert.False(_robot.IsOnTable);
        }

        [Fact]
        public void ShouldIgnoreCommandToFall()
        {
            _service.Process("PLACE 0,0,SOUTH");
            _service.Process("MOVE");
            var result = _service.Process("REPORT");

            Assert.Contains("0,0,SOUTH", result.ToString());
            Assert.True(_robot.IsOnTable);
        }

        [Fact]
        public void ShouldProcessValidPlaceMoveReportSequence()
        {
            _service.Process("PLACE 1,2,EAST");
            _service.Process("MOVE");
            _service.Process("MOVE");
            _service.Process("LEFT");
            _service.Process("MOVE");
            var result = _service.Process("REPORT");

            Assert.True(result.Success);
            Assert.Equal("Current position: 3,3,NORTH", result.ToString());
        }

        [Fact]
        public void ShouldWalkEdgeAndStop()
        {
            _service.Process("PLACE 0,0,North");
            _service.Process("MOVE");
            _service.Process("MOVE");
            _service.Process("MOVE");
            _service.Process("MOVE");
            _service.Process("MOVE");
            var result = _service.Process("REPORT");
            Assert.Contains("0,4,NORTH", result.ToString());
        }
    }

}
