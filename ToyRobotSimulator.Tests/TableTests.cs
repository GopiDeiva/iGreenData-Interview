using NUnit.Framework;
using ToyRobotSimulator.Models;

namespace ToyRobotSimulator.Tests;
[TestFixture]
public class TableTests
{
    [Test]
    public void IsOnTable_ValidPosition_ShouldReturnTrue()
    {
        Table table = new Table();
        Assert.IsTrue(table.IsOnTable(3, 4));
    }

    [Test]
    public void IsOnTable_InvalidPosition_ShouldReturnFalse()
    {
        Table table = new Table();
        Assert.IsFalse(table.IsOnTable(6, 2));
    }
}
