using ToyRobotSimulator.Enums;

namespace ToyRobotSimulator.Models
{
    public class ToyRobot
    {
        private int x;
        private int y;
        private readonly Table _table;
        private Facing _facing;
        public bool IsOnTable { get; private set; }

        public ToyRobot(Table table)
        {
            _table = table;
        }

        public Result Place(int x, int y, Facing f)
        {
            if (!_table.IsValidPosition(x, y))
                return Result.Failure(Message.INVALID_COORDINATE);

            this.x = x;
            this.y = y;
            _facing = f;
            IsOnTable = true;
            
            return Result.Ok();
        }

        public Result Move()
        {
            int movedX = x, movedY = y;

            switch (_facing)
            {
                case Facing.NORTH: 
                    movedY++;
                    break;
                case Facing.EAST: 
                    movedX++; 
                    break;
                case Facing.SOUTH:
                    movedY--;
                    break;
                case Facing.WEST: 
                    movedX--; 
                    break;
            }

            if (_table.IsValidPosition(movedX, movedY))
            {
                x = movedX;
                y = movedY;

                return Result.Ok();
            }
            return Result.Ignore();
        }

        public void Left() => _facing = leftTurns[_facing];

        public void Right() =>  _facing = rightTurns[_facing];

        public string Report() => $"Current position: {x},{y},{_facing}";

        private static readonly Dictionary<Facing, Facing> leftTurns = new()
        {
            { Facing.NORTH, Facing.WEST },
            { Facing.WEST, Facing.SOUTH },
            { Facing.SOUTH, Facing.EAST },
            { Facing.EAST, Facing.NORTH }
        };

        private static readonly Dictionary<Facing, Facing> rightTurns = new()
        {
            { Facing.NORTH, Facing.EAST },
            { Facing.EAST, Facing.SOUTH },
            { Facing.SOUTH, Facing.WEST },
            { Facing.WEST, Facing.NORTH }
        };
    }
}