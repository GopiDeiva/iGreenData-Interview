// ToyRobotSimulator.Tests/CommandExecutorTests.cs
using NUnit.Framework;
using ToyRobotSimulator;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Tests;
[TestFixture]
public class CommandExecutorTests
{
    #region Success tests cases
    [Test]
    public void Execute_PlaceCommand_ShouldUpdateRobotPosition()
    {
        Robot robot = new Robot();
        Table table = new Table();
        CommandExecutor executor = new CommandExecutor(robot, table);
        CommandParser parser = new CommandParser();
        Command command = parser.Parse("PLACE 2,3,EAST");
        executor.ExecuteCommand(command);

        Assert.AreEqual(2, robot.X);
        Assert.AreEqual(3, robot.Y);
        Assert.AreEqual(Direction.EAST, robot.Face);
    }

    [Test]
    public void Execute_MoveCommand_ShouldUpdateRobotPosition()
    {
        Robot robot = new Robot { X = 2, Y = 3, Face = Direction.EAST };
        Table table = new Table();
        CommandExecutor executor = new CommandExecutor(robot, table);
        CommandParser parser = new CommandParser();
        Command command = parser.Parse("MOVE");
        executor.ExecuteCommand(command);

        Assert.AreEqual(3, robot.X);
        Assert.AreEqual(3, robot.Y);
        Assert.AreEqual(Direction.EAST, robot.Face);
    }

    [Test]
    public void Execute_LeftCommand_ShouldRotateRobotLeft()
    {
        Robot robot = new Robot { X = 1, Y = 1, Face = Direction.EAST };
        Table table = new Table();
        CommandExecutor executor = new CommandExecutor(robot, table);
        CommandParser parser = new CommandParser();
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);

            Command command = parser.Parse("LEFT");
            executor.ExecuteCommand(command);

            Assert.AreEqual(1, robot.X);
            Assert.AreEqual(1, robot.Y);
            Assert.AreEqual(Direction.NORTH, robot.Face);
            string capturedOutput = sw.ToString().Trim();
            Assert.AreEqual("Robot facing NORTH.", capturedOutput.Trim());
        }
    }

    [Test]
    public void Execute_RightCommand_ShouldRotateRobotRight()
    {
        Robot robot = new Robot { X = 1, Y = 1, Face = Direction.SOUTH };
        Table table = new Table();
        CommandExecutor executor = new CommandExecutor(robot, table);
        CommandParser parser = new CommandParser();
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);

            Command command = parser.Parse("RIGHT");
            executor.ExecuteCommand(command);
            Assert.AreEqual(1, robot.X);
            Assert.AreEqual(1, robot.Y);
            Assert.AreEqual(Direction.WEST, robot.Face);
            string capturedOutput = sw.ToString().Trim();
            Assert.AreEqual("Robot facing WEST.", capturedOutput.Trim());
        }
    }

    [Test]
    public void Execute_ReportCommand_ShouldPrintRobotPosition()
    {
        Robot robot = new Robot { X = 2, Y = 3, Face = Direction.WEST };
        Table table = new Table();
        CommandExecutor executor = new CommandExecutor(robot, table);

        // Redirect Console output to capture the result
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);

            CommandParser parser = new CommandParser();
            Command command = parser.Parse("REPORT");
            executor.ExecuteCommand(command);

            // Get the captured output from the StringWriter
            string capturedOutput = sw.ToString().Trim();
            Assert.AreEqual("Output: 2,3,WEST", capturedOutput.Trim());
        }
    }
    #endregion

    #region failiure test cases
    [Test]
    public void Execute_InvalidPlaceCommand_ShouldNotUpdateRobotPosition()
    {
        Robot robot = new Robot();
        Table table = new Table();
        CommandExecutor executor = new CommandExecutor(robot, table);

        CommandParser parser = new CommandParser();
        Command command = parser.Parse("PLACE");
        Assert.IsNull(command);

        Assert.AreEqual(null, robot.X);
        Assert.AreEqual(null, robot.Y);
        Assert.AreEqual(Direction.NORTH, robot.Face);
    }

    [Test]
    public void Execute_MoveCommand_WhenRobotNotPlaced_ShouldNotUpdateRobotPosition()
    {
        Robot robot = new Robot();
        Table table = new Table();
        CommandExecutor executor = new CommandExecutor(robot, table);

        CommandParser parser = new CommandParser();
        Command command = parser.Parse("MOVE");
        executor.ExecuteCommand(command);

        Assert.AreEqual(null, robot.X);
        Assert.AreEqual(null, robot.Y);
        Assert.AreEqual(Direction.NORTH, robot.Face);
    }

    [Test]
    public void Execute_PlaceCommand_OutsideTableBounds_ShouldNotUpdateRobotPosition()
    {
        Robot robot = new Robot();
        Table table = new Table();
        CommandExecutor executor = new CommandExecutor(robot, table);
        CommandParser parser = new CommandParser();

        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            Command command = parser.Parse("PLACE 5,6,WEST");
            executor.ExecuteCommand(command);

            Assert.AreEqual(null, robot.X);
            Assert.AreEqual(null, robot.Y);
            Assert.AreEqual(Direction.NORTH, robot.Face);
            Assert.AreEqual("Robot falling from the table. Reverting back...", sw.ToString().Trim());
        }
    }

    [Test]
    public void Execute_MoveCommand_WhenAtTableEdge_ShouldNotFallOff()
    {
        Robot robot = new Robot { X = 4, Y = 4, Face = Direction.NORTH };
        Table table = new Table();
        CommandExecutor executor = new CommandExecutor(robot, table);
        CommandParser parser = new CommandParser();

        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            Command moveCommand = parser.Parse("MOVE");
            executor.ExecuteCommand(moveCommand);

            Assert.AreEqual(4, robot.X);
            Assert.AreEqual(4, robot.Y);
            Assert.AreEqual(Direction.NORTH, robot.Face);
            Assert.AreEqual("Robot falling from the table. Reverting back...", sw.ToString().Trim());
        }
    }
    #endregion
}

