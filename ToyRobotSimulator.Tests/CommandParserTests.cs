using NUnit.Framework;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Tests;
[TestFixture]
public class CommandParserTests
{
    [Test]
    public void Parse_ValidPlaceCommand_ShouldReturnCorrectCommandObject()
    {
        CommandParser parser = new CommandParser();
        Command command = parser.Parse("PLACE 2,3,EAST");

        Assert.IsNotNull(command);
        Assert.AreEqual(CommandType.PLACE, command.Type);
        Assert.AreEqual(2, command.X);
        Assert.AreEqual(3, command.Y);
        Assert.AreEqual(Direction.EAST, command.Face);
    }

    [Test]
    public void Parse_InvalidPlaceCommand_ShouldReturnNull()
    {
        using (StringWriter sw = new StringWriter())
        {
            Console.SetOut(sw);
            CommandParser parser = new CommandParser();
            Command command = parser.Parse("PLACE 2,3");

            Assert.IsNull(command);
            string capturedOutput = sw.ToString().Trim();
            Assert.AreEqual("Invalid command.", capturedOutput.Trim());
        }
    }
}
