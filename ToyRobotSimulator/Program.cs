using System.Diagnostics;
using ToyRobotSimulator.Models;
using ToyRobotSimulator.Services;

var table = new Table(5, 5);
var robot = new ToyRobot(table);
var service = new CommandService(robot);

ShowWelcomePage();
RunMainMenu();

void RunMainMenu()
{
    while (true)
    { 
        ShowMainMenu();

        var option = Console.ReadLine()?.Trim();
        Console.WriteLine();

        switch (option)
        {
            case "1":
                RunCommandsFromFile();
                break;

            case "2":
                RunManualCommands();
                break;
            case "3":
                Console.WriteLine("Exiting application. Goodbye!");
                return;

            default:
                Console.WriteLine(Message.INVALID_OPTION);
                break;
        }
    }
}
void ShowWelcomePage()
{
    Console.Clear();
    Console.WriteLine(
        $@"
  _____           ___     _         _     ___ _           _      _           
 |_   _|__ _  _  | _ \___| |__  ___| |_  / __(_)_ __ _  _| |__ _| |_ ___ _ _ 
   | |/ _ \ || | |   / _ \ '_ \/ _ \  _| \__ \ | '  \ || | / _` |  _/ _ \ '_|
   |_|\___/\_, | |_|_\___/_.__/\___/\__| |___/_|_|_|_\_,_|_\__,_|\__\___/_|  
           |__/                                                              

Toy Robot Simulator simulates a toy robot moving on a 5x5 table.
Available commands:
- PLACE X,Y,F   => Places toy robot on the table at position X,Y facing F (NORTH, EAST, SOUTH, WEST)
- MOVE          => Moves toy robot one unit forward in the direction it is currently facing
- LEFT          => Turn toy robot 90 degrees to the left
- RIGHT         => Turn toy robot 90 degrees to the right
- REPORT        => Outputs the current X,Y,F position of the toy robot\n
            ");
}

void ShowMainMenu()
{
    Console.WriteLine("\nChoose input mode option:");
    Console.WriteLine("1. Load commands from a file");
    Console.WriteLine("2. Enter commands manually");
    Console.WriteLine("3. Exit");
    Console.Write("Enter option: ");
}

void ShowCommandInstructions()
{
    Console.WriteLine("Enter commands line by line (enter 'BACK' to return to main menu):");
    Console.WriteLine("----------------------------------\n");
    Console.WriteLine("PLACE X,Y,F (e.g. PLACE 0,0,NORTH)");
    Console.WriteLine("MOVE - move forward in the current direction");
    Console.WriteLine("LEFT - turn 90 degrees to the left");
    Console.WriteLine("RIGHT - turn 90 degrees to the right");
    Console.WriteLine("REPORT - print current position and direction\n");
    Console.WriteLine("----------------------------------\n");

}

void RunCommandsFromFile()
{
    while (true)
    {
        string path = string.Empty;
        int attempts = 0;
        const int maxAttempts = 3;

        while (attempts < maxAttempts)
        {
            Console.Write("Enter full file path: ");
            path = Console.ReadLine()?.Trim();
            
            if (string.Equals(path, "BACK", StringComparison.OrdinalIgnoreCase))
            {
                path = null;
                break;
            }

            if (File.Exists(path)) break;

            Console.WriteLine("File not found. Try again or enter 'BACK' to return.\n");
            attempts++;

            if (attempts >= maxAttempts)
            {
                Console.WriteLine("Too many failed attempts. Returning to main menu.\n");
                path = null;
                break;
            }            
        }

        if (string.IsNullOrWhiteSpace(path)) break;

        var commands = File.ReadAllLines(path);

        foreach (var cmd in commands)
        {
            Console.WriteLine(cmd);

            var result = service.Process(cmd);
            if (!result.Success && result.ResultMessage == null) continue;
            Console.WriteLine(result);
        }

        Console.WriteLine("\nFile processed.\n");
        ShowReport();
        break;
    }
}

void RunManualCommands()
{
    ShowCommandInstructions();

    while (true)
    {
        Console.Write("Enter command: ");

        var command = Console.ReadLine()?.Trim();
        
        if (string.Equals(command, "BACK", StringComparison.OrdinalIgnoreCase)) break;
        
        if (string.IsNullOrWhiteSpace(command)) continue;

        var result = service.Process(command);
        if ( !result.Success && result.ResultMessage == null) continue;
        Console.WriteLine(result);
    }
}

void ShowReport()
{
    while(true)
    {
        Console.WriteLine("Would you like a report on current position of the toy robot before returning to main menu?");
        Console.WriteLine("1. Yes");
        Console.WriteLine("2. No");
        Console.Write("Enter option: ");
        var option = Console.ReadLine()?.Trim();
        if (option == "1")
        {
            Console.WriteLine(service.Process("Report"));
            Console.WriteLine();
            break;
        }
        if (option == "2") break;
    }
}