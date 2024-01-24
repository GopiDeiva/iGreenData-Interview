namespace ToyRobotSimulator.Models;
public class Table
{
    public const int Width = 5;
    public const int Height = 5;

    public bool IsOnTable(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
}
