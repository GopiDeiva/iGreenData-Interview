using ToyRobotSimulator.Models;
namespace ToyRobotSimulator;

public class CommandParser
{
    public Command Parse(string inputCommand)
    {
        if (string.IsNullOrWhiteSpace(inputCommand))
            return null;

        string[] commandParts = inputCommand.Trim().Split(' ');

        if (commandParts.Length == 0)
            return null;

        Command command = new Command { Type = Enum.Parse<CommandType>(commandParts[0]) };

        if (command.Type == CommandType.PLACE)
        {
            if (commandParts.Length == 2)
            {
                string[] placeCommandArgs = commandParts[1].Split(',');
                if (placeCommandArgs.Length == 3)
                {
                    command.X = int.Parse(placeCommandArgs[0]);
                    command.Y = int.Parse(placeCommandArgs[1]);
                    command.Face = Enum.Parse<Direction>(placeCommandArgs[2]);
                }
                else
                {
                    Console.WriteLine("Invalid command.");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Invalid command.");
                return null;
            }
        }

        return command;
    }
}
