using NUnit.Framework;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Tests;
[TestFixture]
public class RobotTests
{
    [Test]
    public void RobotInitialization()
    {
        Robot robot = new Robot();
        Assert.AreEqual(null, robot.X);
        Assert.AreEqual(null, robot.Y);
        Assert.AreEqual(Direction.NORTH, robot.Face);
    }

    [Test]
    public void RobotPositionUpdate()
    {
        Robot robot = new Robot { X = 2, Y = 3, Face = Direction.EAST };
        Assert.AreEqual(2, robot.X);
        Assert.AreEqual(3, robot.Y);
        Assert.AreEqual(Direction.EAST, robot.Face);
    }
}