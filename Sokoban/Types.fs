namespace Sokoban

type Position = {
    Row: int
    Col: int
}

type Direction =
    | Up
    | Down
    | Left
    | Right

type GameStatus =
    | Playing
    | Cleared
    | Quit

type BoardState = {
    Player: Position
    Boxes: Set<Position>
    Walls: Set<Position>
    Goals: Set<Position>
    Height: int
    Width: int
    MoveCount: int
    Status: GameStatus
}

type GameState = {
    Current: BoardState
    Initial: BoardState
    History: BoardState list
    StageIndex: int
}

type Command =
    | Move of Direction
    | Restart
    | Undo
    | QuitGame
    | Invalid