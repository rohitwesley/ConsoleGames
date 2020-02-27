using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGamesVRProject
{
    // Simple MVC example for Checkers. 

    // Key idea: The model is neither coupled to the view or controller.
    // As well, the model is highly modular—it is not a model of "checkers
    // for command console", or "checkers for Unity", or even "checkers for
    // human players". This game of checkers could be played by two AIs, for example.

    // Note that none of this is event-driven;
    // We *could* use events, but because the actual loop of checkers is so simple
    // it's not really that bad of an idea to just check the state every turn and
    // react accordingly.
    class ErikCheckers
    {
        // In this case, we do *not* create a controller class!
        // Instead, we treat the Main() function as the controller.
        // This could be made into its own class down the line.
        private static void Main()
        {
            // Controller manages the lifecycle of the model and view.
            Checkers checkers = new Checkers();
            CheckersView view = new CheckersView(checkers);

            // Setup the game, i.e.,
            // checkers.Initialize or whatever.
            // Could randomly select which player is white/black.

            // Game loop.
            while (true)
            {
                Console.WriteLine($"It is {(checkers.IsWhiteTurn ? "white's" : "black's")} turn.");

                // Read input, parse the input.
                // string input = Console.Read();
                // int x, y, targetX, targetY.
                // All input parsing can be done outside of the model.

                // Get input, try to move the piece.
                if (checkers.TryMovePiece())
                {
                    // Piece was successfully moved. 
                    view.Draw();

                    // Maybe check if the game is over.
                    if (checkers.IsGameOver())
                    {
                        // Print something, exit program, whatever.
                    }
                }
                else
                {
                    // Print an error, allow the loop to repeat.
                }
            }
        }
    }

    class Checkers
    {
        public bool IsWhiteTurn { get; private set; }

        // Store the game state in a list of pieces.
        // We could also store it as a 2D array of tiles.
        // public List<Checker> pieces;

        public bool TryMovePiece()
        {
            // Check if it's the moving piece's turn.
            // Check if it is a valid move.
            // Update the game state if it is (board state and player's turn), otherwise return false.
        }

        public bool IsGameOver()
        {
            // Check game state.
        }
    }

    class CheckersView
    {
        Checkers checkers;

        public CheckersView(Checkers checkers)
        {
            this.checkers = checkers;
        }

        public void Draw()
        {
            // Access checker's board state, display the board on screen.
        }
    }
}
