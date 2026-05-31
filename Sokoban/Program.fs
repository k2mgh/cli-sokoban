namespace Sokoban

module Program =

    [<EntryPoint>]
    let main argv =
        let game = Board.createGameState 0
        Game.playLoop game