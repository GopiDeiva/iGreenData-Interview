namespace ToyRobotSimulator.Models;

public enum CommandType
{
    PLACE,
    MOVE,
    LEFT,
    RIGHT,
    REPORT
}

public class Command
{
    public CommandType Type { get; set; }
    public int? X { get; set; }
    public int? Y { get; set; }
    public Direction? Face { get; set; }
}
