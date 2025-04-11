namespace ToyRobotSimulator.Models
{
    public class Message
    {
        public const string NOT_PLACED = "Toy robot is not placed on the table. Place the toy robot by using command: PLACE x, y, f";
        public const string INVALID_COORDINATE = "The coordinate given for the robot is outside of the table.";
        public const string INVALID_COMMAND = "The command is not valid. Command using instructions.";
        public const string INVALID_OPTION = "The input is not valid option. Please enter valid option.\n";
        public const string ENTER_COMMAND = "Enter command:";
    }
}
