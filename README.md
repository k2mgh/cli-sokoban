# CLI Sokoban

CLI Sokoban is a command-line Sokoban puzzle game written in F# and .NET 10.

Sokoban is a grid-based puzzle game where the player pushes boxes onto goal tiles. The player clears a stage when every box is placed on a goal tile.

## Requirements

- .NET 10 SDK

You can check your .NET version with:

```bash
dotnet --version
```

The version should start with `10`.

## How to Run

Clone this repository and run the following command in the project directory:

```bash
dotnet run
```

You can also use the provided script.

### Windows

```bash
run.bat
```

### macOS / Linux

```bash
chmod +x run.sh
./run.sh
```

## Game Rules

The game board is displayed as an ASCII grid in the terminal.

The player can move in four directions using `W`, `A`, `S`, and `D`.

The player can move to an empty tile or a goal tile.

The player cannot move through walls.

The player can push one box if the tile behind the box is empty or a goal tile.

The player cannot pull boxes.

The player cannot push two boxes at the same time.

The player cannot push a box into a wall or another box.

The stage is cleared when every box is placed on a goal tile.

The game counts the number of valid moves.

## Controls

| Key | Action |
|---|---|
| W | Move up |
| A | Move left |
| S | Move down |
| D | Move right |
| U | Undo previous valid move |
| R | Restart current stage |
| Q | Quit game |

## Symbols

| Symbol | Meaning |
|---|---|
| P | Player |
| B | Box |
| G | Goal |
| # | Wall |
| . | Empty space |
| * | Box on goal |
| + | Player on goal |

## Features

- Command-line interface
- ASCII-based board rendering
- Player movement using W, A, S, and D
- Wall collision detection
- Box-pushing logic
- Goal detection
- Stage clear condition
- Move counter
- Restart option
- Undo option
- Quit option
- Multiple stages

## Example Gameplay

```text
=== CLI Sokoban ===
Stage 1 / 3
Moves: 0

#######
#.....#
#.B.G.#
#..P..#
#######

Controls:
W = Up, A = Left, S = Down, D = Right
U = Undo, R = Restart, Q = Quit

Enter your command:
```

If the player enters `W`, the player moves upward.

```text
#######
#.....#
#.BPG.#
#.....#
#######
```

If the player enters `A`, the player moves left.

```text
#######
#.....#
#PB.G.#
#.....#
#######
```

If the player enters `D`, the player pushes the box to the right.

```text
#######
#.....#
#.PBG.#
#.....#
#######
```

If the player enters `D` again, the box is pushed onto the goal tile.

```text
#######
#.....#
#..P*.#
#.....#
#######
```

Then the stage is cleared.

## Project Structure

```text
cli-sokoban/
├── Sokoban.fsproj
├── README.md
├── requirements.md
├── run.bat
├── run.sh
└── Sokoban/
    ├── Types.fs
    ├── Levels.fs
    ├── Board.fs
    ├── Game.fs
    └── Program.fs
```

## Implementation Notes

The game is implemented using F# records, discriminated unions, sets, pattern matching, and a recursive game loop.

The game state is stored in immutable records. Each valid player action creates an updated game state instead of directly modifying the previous state.

The undo feature is implemented by storing previous board states in a history list.

The project is separated into several modules:

| Module | Responsibility |
|---|---|
| `Types` | Defines core types such as `Position`, `Direction`, `BoardState`, `GameState`, and `Command` |
| `Levels` | Stores the stage maps |
| `Board` | Handles level parsing, board rendering, movement, box pushing, and clear checking |
| `Game` | Handles command parsing, restart, undo, quit, and the main game loop |
| `Program` | Entry point of the program |

## LLM Usage

I used ChatGPT to help design the project structure, write the initial requirements document, and debug parts of the F# implementation.

I manually reviewed and modified the final code to make sure it satisfies the requirements of my submitted proposal. I also simplified some suggested features to keep the final implementation stable and faithful to the requirements.

The LLM sometimes suggested unnecessary optional features such as random map generation and more complex stage systems. I decided not to include those features in the final version because the project specification emphasizes clear and well-defined behavior over complexity.