namespace ToyRobotSimulator.Models;
public class Robot
{
    public int? X { get; set; }
    public int? Y { get; set; }
    public Direction Face { get; set; }
}

/// <summary>
/// aligned in a way to calculate some math to turn 90 degrees
///          EAST
///           |
///  NORTH -- o -- SOUTH
///           |
///          WEST
/// </summary>

public enum Direction
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}
