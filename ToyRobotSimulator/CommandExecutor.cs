using ToyRobotSimulator.Models;

namespace ToyRobotSimulator;

public class CommandExecutor
{
    private readonly Robot robot;
    private readonly Table table;

    public CommandExecutor(Robot robot, Table table)
    {
        this.robot = robot;
        this.table = table;
    }

    public void ExecuteCommand(Command command)
    {
        switch (command.Type)
        {
            case CommandType.PLACE:
                ExecutePlaceCommand(command);
                break;
            case CommandType.MOVE:
                ExecuteMoveCommand();
                break;
            case CommandType.LEFT:
                ExecuteTurnLeftCommand();
                break;
            case CommandType.RIGHT:
                ExecuteTurnRightCommand();
                break;
            case CommandType.REPORT:
                ExecuteReportCommand();
                break;
        }
    }

    private void ExecutePlaceCommand(Command command)
    {
        if (table.IsOnTable(command.X ?? 0, command.Y ?? 0))
        {
            robot.X = command.X ?? 0;
            robot.Y = command.Y ?? 0;
            robot.Face = command.Face ?? Direction.NORTH;
        }
        else
        {
            PositionRevertBack();
        }
    }

    private void ExecuteMoveCommand()
    {
        if (robot.X == null || robot.Y == null)
            return;

        int newX = robot.X ?? 0;
        int newY = robot.Y ?? 0;

        switch (robot.Face)
        {
            case Direction.NORTH:
                newY++;
                break;
            case Direction.EAST:
                newX++;
                break;
            case Direction.SOUTH:
                newY--;
                break;
            case Direction.WEST:
                newX--;
                break;
        }

        if (table.IsOnTable(newX, newY))
        {
            robot.X = newX;
            robot.Y = newY;
        }
        else
        {
            PositionRevertBack();
        }
    }

    private void ExecuteTurnLeftCommand()
    {
        robot.Face = (Direction)(((int)robot.Face + 3) % 4); // Rotate 90 degrees left
        PrintFacing();
    }

    private void ExecuteTurnRightCommand()
    {
        robot.Face = (Direction)(((int)robot.Face + 1) % 4); // Rotate 90 degrees right
        PrintFacing();
    }

    private void ExecuteReportCommand()
    {
        Console.WriteLine($"Output: {robot.X},{robot.Y},{robot.Face}");
    }

    private void PositionRevertBack()
    {
        Console.WriteLine("Robot falling from the table. Reverting back...");
    }

    private void PrintFacing()
    {
        Console.WriteLine($"Robot facing {robot.Face}.");
    }
}
