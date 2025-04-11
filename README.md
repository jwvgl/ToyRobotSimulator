# Toy Robot Simulator

A simple console application simulating a toy robot moving on a square tabletop, built using C# and .NET 8.0. 

## Problem Description

- The table is a 5x5 grid.
- There are no other obstructions on the table surface.
- All commands are ignored until a valid `PLACE` command is issued.
- The toy robot must not fall of the table during movement or at the initial placement.
- The toy robot ignores any command that would move it off the table.
- The toy robot can be placed, moved, and turned left and right.

## Commands

```text
PLACE X,Y,F      # Places toy robot on the table at position X,Y facing F (NORTH, EAST, SOUTH, WEST)
MOVE             # Moves toy robot one unit forward in the direction it is currently facing
LEFT             # Turn toy robot 90 degrees to the left
RIGHT            # Turn toy robot 90 degrees to the right
REPORT           # Outputs the current X,Y,F position of the toy robot
```

## Example Input

```
PLACE 1,2,EAST
MOVE
MOVE
LEFT
MOVE
REPORT
```

### Expected Output
```
Current position: 3,3,NORTH
```

## Running the App
### Using Visual Studio

1. Open in **Visual Studio 2022+**
2. Set `ToyRobotSimulator` as the startup project
3. Press **Ctrl+F5** to run without debugger

### Using .NET CLI (Bash / Terminal)

1. Navigate to the project root (where the `.csproj` file is)
2. Run:

```bash
dotnet run
```

## Testing

Uses `xUnit` with `.NET 8.0`

### Using Visual Studio

1. Run test explorer in Visual Studio


### Using .NET CLI (Bash / Terminal)

1. Navigate to the test project root (where the `.csproj` file is)
2. Run:

```bash
dotnet test
```

## Project Structure

```
ToyRobotSimulator/          # App logic and models
ToyRobotSimulator.Tests/    # xUnit test suite
```

## Design Notes

- Pure logic and validation in `ToyRobot`
- `CommandService` parses and coordinates commands
- No external dependencies