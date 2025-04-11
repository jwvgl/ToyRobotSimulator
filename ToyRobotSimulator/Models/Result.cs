namespace ToyRobotSimulator.Models
{
    public class Result
    {
        public bool Success { get; init; }
        public string ResultMessage { get; init; }

        public static Result Ok(string message = null) => new() { Success = true, ResultMessage = message };
        public static Result Ignore() => new() { Success = false };
        public static Result Failure(string message) => new() { Success = false, ResultMessage = message };

        public override string ToString()
        {
            if (Success)
            {
                return string.IsNullOrWhiteSpace(ResultMessage)
                    ? "OK"
                    : ResultMessage;
            }
            else
            {
                return string.IsNullOrWhiteSpace(ResultMessage)
                    ? ResultMessage
                    : $"Error: {ResultMessage}";
            }
        }
    }
}
