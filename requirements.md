# Requirements: CLI Sokoban

## Project Title

CLI Sokoban

## Overview

This project is a command-line Sokoban puzzle game written in F# and .NET 10.

Sokoban is a grid-based puzzle game where the player pushes boxes onto goal tiles. The player clears a stage when every box is placed on a goal tile.

The game is played in the terminal. The board is displayed as an ASCII grid, and the player controls the character using keyboard input.

## Requirements

1. The game shall display a grid-based Sokoban board in the terminal.

2. The game shall display the current stage number and move count.

3. The player shall be represented by `P`.

4. A box shall be represented by `B`.

5. A goal tile shall be represented by `G`.

6. A wall shall be represented by `#`.

7. An empty tile shall be represented by `.`.

8. A box on a goal tile shall be represented by `*`.

9. A player on a goal tile shall be represented by `+`.

10. The player shall move up by entering `W`.

11. The player shall move left by entering `A`.

12. The player shall move down by entering `S`.

13. The player shall move right by entering `D`.

14. The player shall be able to move to an empty tile.

15. The player shall be able to move to a goal tile.

16. The player shall not be able to move through a wall.

17. If the player tries to move into a wall, the board state shall not change.

18. The player shall be able to push one box if the tile behind the box is empty.

19. The player shall be able to push one box if the tile behind the box is a goal tile.

20. The player shall not be able to push a box into a wall.

21. The player shall not be able to push a box into another box.

22. The player shall not be able to push two boxes at the same time.

23. The player shall not be able to pull boxes.

24. A valid movement shall increase the move count by 1.

25. An invalid movement shall not increase the move count.

26. The stage shall be cleared when every box is placed on a goal tile.

27. After a stage is cleared, the game shall show a stage clear message.

28. The player shall be able to restart the current stage by entering `R`.

29. Restarting a stage shall reset the board state and move count.

30. The player shall be able to undo the previous valid move by entering `U`.

31. If there is no previous move, undo shall not change the board state.

32. The player shall be able to quit the game by entering `Q`.

33. The game shall include at least one playable stage.

34. The final implementation shall include a README file explaining how to run the game.

## Example Interaction

The game starts by displaying the first stage.

```text
=== CLI Sokoban ===
Stage 1 / 3
Moves: 0

#######
#.....#
#.B.G.#
#..P..#
#######
```

The player enters `W`.

The player moves upward.

```text
#######
#.....#
#.BPG.#
#.....#
#######
```

The player enters `A`.

The player moves left.

```text
#######
#.....#
#PB.G.#
#.....#
#######
```

The player enters `D`.

The player pushes the box to the right.

```text
#######
#.....#
#.PBG.#
#.....#
#######
```

The player enters `D` again.

The player pushes the box onto the goal tile.

```text
#######
#.....#
#..P*.#
#.....#
#######
```

The stage is cleared because every box is on a goal tile.