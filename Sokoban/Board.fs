namespace Sokoban

open System

module Board =

    let addPosition pos dir =
        match dir with
        | Up -> { Row = pos.Row - 1; Col = pos.Col }
        | Down -> { Row = pos.Row + 1; Col = pos.Col }
        | Left -> { Row = pos.Row; Col = pos.Col - 1 }
        | Right -> { Row = pos.Row; Col = pos.Col + 1 }

    let isOutside board pos =
        pos.Row < 0 ||
        pos.Row >= board.Height ||
        pos.Col < 0 ||
        pos.Col >= board.Width

    let isCleared board =
        board.Boxes
        |> Set.forall (fun boxPosition -> board.Goals.Contains boxPosition)

    let parseLevel (lines: string array) =
        let height = lines.Length
        let width =
            lines
            |> Array.map (fun line -> line.Length)
            |> Array.max

        let mutable player = { Row = 0; Col = 0 }
        let mutable walls = Set.empty<Position>
        let mutable boxes = Set.empty<Position>
        let mutable goals = Set.empty<Position>

        for r in 0 .. height - 1 do
            for c in 0 .. lines.[r].Length - 1 do
                let pos = { Row = r; Col = c }

                match lines.[r].[c] with
                | '#' ->
                    walls <- walls.Add pos
                | 'P' ->
                    player <- pos
                | 'B' ->
                    boxes <- boxes.Add pos
                | 'G' ->
                    goals <- goals.Add pos
                | '*' ->
                    boxes <- boxes.Add pos
                    goals <- goals.Add pos
                | '+' ->
                    player <- pos
                    goals <- goals.Add pos
                | '.' ->
                    ()
                | ' ' ->
                    ()
                | _ ->
                    ()

        {
            Player = player
            Boxes = boxes
            Walls = walls
            Goals = goals
            Height = height
            Width = width
            MoveCount = 0
            Status = Playing
        }

    let loadStage index =
        Levels.get index
        |> parseLevel

    let createGameState index =
        let board = loadStage index

        {
            Current = board
            Initial = board
            History = []
            StageIndex = index
        }

    let tileChar board pos =
        let hasPlayer = board.Player = pos
        let hasBox = board.Boxes.Contains pos
        let hasWall = board.Walls.Contains pos
        let hasGoal = board.Goals.Contains pos

        if hasPlayer && hasGoal then
            '+'
        elif hasPlayer then
            'P'
        elif hasBox && hasGoal then
            '*'
        elif hasBox then
            'B'
        elif hasWall then
            '#'
        elif hasGoal then
            'G'
        else
            '.'

    let render board stageIndex =
        Console.Clear()

        printfn "=== CLI Sokoban ==="
        printfn "Stage %d / %d" (stageIndex + 1) Levels.count
        printfn "Moves: %d" board.MoveCount
        printfn ""

        for r in 0 .. board.Height - 1 do
            for c in 0 .. board.Width - 1 do
                let pos = { Row = r; Col = c }
                printf "%c" (tileChar board pos)
            printfn ""

        printfn ""
        printfn "Symbols:"
        printfn "P = Player, B = Box, G = Goal, # = Wall, . = Empty"
        printfn "* = Box on Goal, + = Player on Goal"
        printfn ""
        printfn "Controls:"
        printfn "W = Up, A = Left, S = Down, D = Right"
        printfn "U = Undo, R = Restart, Q = Quit"
        printfn ""

    let move dir board =
        if board.Status <> Playing then
            board, false, "The stage is already cleared."
        else
            let nextPlayer = addPosition board.Player dir

            if isOutside board nextPlayer || board.Walls.Contains nextPlayer then
                board, false, "Invalid move: wall."

            elif board.Boxes.Contains nextPlayer then
                let nextBox = addPosition nextPlayer dir

                if isOutside board nextBox ||
                   board.Walls.Contains nextBox ||
                   board.Boxes.Contains nextBox then
                    board, false, "Invalid move: the box cannot be pushed."
                else
                    let newBoxes =
                        board.Boxes
                        |> Set.remove nextPlayer
                        |> Set.add nextBox

                    let movedBoard =
                        {
                            board with
                                Player = nextPlayer
                                Boxes = newBoxes
                                MoveCount = board.MoveCount + 1
                        }

                    let updatedBoard =
                        if isCleared movedBoard then
                            { movedBoard with Status = Cleared }
                        else
                            movedBoard

                    updatedBoard, true, "Box pushed."

            else
                let movedBoard =
                    {
                        board with
                            Player = nextPlayer
                            MoveCount = board.MoveCount + 1
                    }

                let updatedBoard =
                    if isCleared movedBoard then
                        { movedBoard with Status = Cleared }
                    else
                        movedBoard

                updatedBoard, true, "Moved."