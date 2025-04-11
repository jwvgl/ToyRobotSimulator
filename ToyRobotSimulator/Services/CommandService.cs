using ToyRobotSimulator.Enums;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Services
{
    public class CommandService
    {
        private readonly ToyRobot _robot;

        public CommandService(ToyRobot robot)
        {
            _robot = robot;
        }

        public Result Process(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                return Result.Ignore();

            command = command.Trim().ToUpper();

            if (command.StartsWith("PLACE")) 
                return Place(command);

            else if (!_robot.IsOnTable)
                return Result.Ignore();

            switch (command)
            {
                case "MOVE":
                    return _robot.Move();
                case "LEFT":
                    _robot.Left();
                    return Result.Ok();
                case "RIGHT":
                    _robot.Right();
                    return Result.Ok();
                case "REPORT":
                    return Result.Ok(_robot.Report());
                default:
                    return Result.Failure(Message.INVALID_COMMAND);
            }
        }

        private Result Place(string command)
        {
            var coordinate = command.Substring(5).Split(',');

            if (coordinate.Length != 3)
                return Result.Failure(Message.INVALID_COMMAND);

            if (int.TryParse(coordinate[0], out int x) &&
                int.TryParse(coordinate[1], out int y) &&
                Enum.TryParse(coordinate[2], out Facing f))
            {
                return _robot.Place(x, y, f);
            }
            else
                return Result.Failure(Message.NOT_PLACED);
        }
    }
}
