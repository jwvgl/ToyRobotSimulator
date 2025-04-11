namespace ToyRobotSimulator.Models
{
    public class Table
    {
        private readonly int width;
        private readonly int height;

        public Table(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public bool IsValidPosition(int x, int y) => x >= 0 && x < width && y >= 0 && y < height;
    }
}
