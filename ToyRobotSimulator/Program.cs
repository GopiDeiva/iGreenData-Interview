using ToyRobotSimulator.Models;

namespace ToyRobotSimulator
{
    class Program
    {
        static void Main()
        {
            var robot = new Robot();
            var table = new Table();
            var commandParser = new CommandParser();
            var commandExecutor = new CommandExecutor(robot, table);

            string[] commands = File.ReadAllLines("commands.txt"); // Assuming the commands are stored in a file

            foreach (string commandString in commands)
            {
                Command command = commandParser.Parse(commandString);
                if (command != null)
                {
                    commandExecutor.ExecuteCommand(command);
                }
            }
        }
    }
}
