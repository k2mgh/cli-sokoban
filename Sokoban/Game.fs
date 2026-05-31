namespace Sokoban

open System

module Game =

    let parseCommand (input: string) =
        match input.Trim().ToUpper() with
        | "W" -> Move Up
        | "A" -> Move Left
        | "S" -> Move Down
        | "D" -> Move Right
        | "U" -> Undo
        | "R" -> Restart
        | "Q" -> QuitGame
        | _ -> Invalid

    let applyCommand command game =
        match command with
        | Move dir ->
            let newBoard, validMove, message = Board.move dir game.Current

            if validMove then
                {
                    game with
                        Current = newBoard
                        History = game.Current :: game.History
                },
                message
            else
                game, message

        | Restart ->
            let restarted =
                {
                    game with
                        Current = game.Initial
                        History = []
                }

            restarted, "Stage restarted."

        | Undo ->
            match game.History with
            | previous :: rest ->
                let undone =
                    {
                        game with
                            Current = previous
                            History = rest
                    }

                undone, "Undo completed."
            | [] ->
                game, "Nothing to undo."

        | QuitGame ->
            let quitBoard = { game.Current with Status = Quit }

            {
                game with Current = quitBoard
            },
            "Quit."

        | Invalid ->
            game, "Invalid input. Please enter W, A, S, D, U, R, or Q."

    let waitForEnter () =
        printfn "Press Enter to continue..."
        Console.ReadLine() |> ignore

    let rec playLoop game =
        Board.render game.Current game.StageIndex

        match game.Current.Status with
        | Quit ->
            printfn "Game ended."
            0

        | Cleared ->
            printfn "Stage Cleared!"
            printfn "You solved this stage in %d moves." game.Current.MoveCount
            printfn ""

            if game.StageIndex + 1 < Levels.count then
                printf "Go to next stage? (Y/N): "
                let answer = Console.ReadLine().Trim().ToUpper()

                if answer = "Y" then
                    Board.createGameState (game.StageIndex + 1)
                    |> playLoop
                else
                    printfn "Thanks for playing!"
                    0
            else
                printfn "Congratulations! You cleared all stages."
                printfn "Thanks for playing!"
                0

        | Playing ->
            printf "Enter your command: "
            let input = Console.ReadLine()
            let command = parseCommand input
            let newGame, message = applyCommand command game

            printfn "%s" message
            waitForEnter()
            playLoop newGame