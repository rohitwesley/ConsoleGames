using System;
using System.Collections.Generic;
using System.Threading; 

namespace Games
{
    class GameLauncher
    {
        // Controller manages the lifecycle of the model and view.
        Checkers checkersModel;
        CheckersView view;

        public void PlayLoop()
        {

            // Setup the game
            Setup();

            // Game loop.
            bool isEndofGame = false;
            while (!isEndofGame)
            {
                view.Clear();
                view.DrawBoard();
                view.ShowBoardState();

                // Get player inputs;
                SelectPlayerPiece();
                //SelectPlayerPieceMovement();

                // TODO Debugging each stage of game loop after refactoring.
                //// Get input, try to move the piece.
                //if (checkersModel.TryMovePiece())
                //{
                //    // Piece was successfully moved. 
                //    view.DrawBoard();

                //    // Check if Game Over
                //    if (checkersModel.IsGameOver())
                //    {
                //        // Print something, exit program, whatever.
                //        isEndofGame = true;
                //        GameOver();
                //    }
                //}
                //else
                //{
                //    // Print an error, allow the loop to repeat.
                //    view.Clear();
                //    view.TellUser("Please enter a valid Move.");
                //    view.Pause(view.pauseTime / 3);
                //}


                NextPlayer();
                //isEndofGame = true;

            }

        }

        // setup and index all games
        protected void Setup()
        {
            // Controller manages the lifecycle of the model and view.
            checkersModel = new Checkers();
            view = new CheckersView(checkersModel);
            // Reset the board.
            checkersModel.ResetBoardPieces();
            // Toss a Coin to see who starts
            // set to false to use finit state for ditermistic results for debuging
            // or set to true to use random
            TossCoin(false);
        }

        // Piece Selection State
        private void SelectPlayerPiece()
        {
            Checkers.Index piece = new Checkers.Index();
            do
            {
                view.Clear();
                view.DrawBoard();
                view.ShowBoardState();
                view.TellUser("Please Choose a Pieces to move : (1-8,a-h)");
                string answer = view.AskUser();
                string[] position = answer.Split(",");
                if (position.Length != 2)
                {
                    view.Clear();
                    view.TellUser("Please enter a valid Pieces.");
                    view.Pause(view.pauseTime / 3);
                }
                else
                {
                    piece.xPos = Convert.ToInt32(position[0]) - 1;//Get int value and normalise to array index 0-7
                    piece.yPos = ((int)Convert.ToChar(position[1]) - 97);//Get ASCII value 97-104 (a-h)and normalise to arry index 0-7
                    if (!checkersModel.isValidPiece(piece))
                    {
                        view.Clear();
                        view.TellUser("Please enter a valid Pieces.");
                        view.Pause(view.pauseTime / 3);
                    }
                }
            }
            while (!checkersModel.isValidPiece(piece));

            checkersModel.SetCurrentPiece(piece);

        }

        // Tile Selection State
        private void SelectPlayerPieceMovement()
        {
            view.Clear();
            view.DrawBoard();
            view.ShowBoardState();
            List<Checkers.Index> moves = checkersModel.getValidMove(checkersModel.currentPieceIndex);
            if (moves != null)
            {
                view.TellUser("Please Choose a move from the list: ");
                for (int i = 0; i < moves.Count; i++)
                {
                    view.TellUser((i + 1) + " : " + (moves[i].xPos+1) + " , " + (moves[i].yPos+1));
                }
                string answer = view.AskUser();
                int moveIndex = Convert.ToInt32(answer) - 1;
                checkersModel.SetCurrentMove(moves[moveIndex]);
            }
            else
            {
                view.TellUser("Invalid Moves...");
            }
            view.Pause(view.pauseTime / 3);
            
        }

        // Toss a Coin State
        private void TossCoin(bool isRand)
        {
            // Show toss coin animation
            if(isRand)
            {
                Random rnd = new Random();
                int tossTime = rnd.Next(4, 10);
                for (int tossTick = 0; tossTick < tossTime; tossTick++)
                {
                    view.Clear();
                    view.TellUser("Tossing Coin to see who starts.");
                    string progressBar = "";
                    for (int i = 0; i < tossTick; i++)
                    {
                        progressBar += checkersModel.currentPlayer;
                    }
                    view.TellUser(progressBar);
                    Checkers.Playertype nextPlayer = new Checkers.Playertype();
                    if (NoiseGenerator.RollCoin())
                        nextPlayer = Checkers.Playertype.X;
                    else
                        nextPlayer = Checkers.Playertype.O;

                    checkersModel.SetCurrentPlayer(nextPlayer);
                    view.Pause(1000);
                }
            }
            else
            {
                Checkers.Playertype nextPlayer = new Checkers.Playertype();
                nextPlayer = Checkers.Playertype.X;
                checkersModel.SetCurrentPlayer(nextPlayer);
            }
        }

        private void NextPlayer()
        {
            Checkers.Playertype nextPlayer = new Checkers.Playertype();
            if (checkersModel.currentPlayer != Checkers.Playertype.X)
                nextPlayer = Checkers.Playertype.X;
            else
                nextPlayer = Checkers.Playertype.O;
            checkersModel.SetCurrentPlayer(nextPlayer);
        }

        // GameOver State
        private void GameOver()
        {
            Checkers.Playertype winner = checkersModel.GetWinner();
            // state the winner
            if (winner != Checkers.Playertype.Empty) view.TellUser(winner +"Won");
            else view.TellUser(" Its a Draw...");
            view.Pause(view.pauseTime);
        }


    }

    class CheckersView
    {
        Checkers checkersModel;
        // 1 sec pause time
        public int pauseTime { get; private set; } = 6000;

        public CheckersView(Checkers checkers)
        {
            this.checkersModel = checkers;
        }

        // Board UI
        public void DrawBoard()
        {
            // 8x8 board tiles
            string tile = "  [a][b][c][d][e][f][g][h]";
            TellUser(tile);
            // columns
            for (int i = 0; i < 8; i++)
            {
                tile = (i + 1) + " ";
                // rows
                for (int j = 0; j < 8; j++)
                {
                    switch (checkersModel.boardTile[i, j].piece.pieceType)
                    {
                        case Checkers.PieceType.Pawn:
                            if (checkersModel.boardTile[i, j].piece.player == Checkers.Playertype.X) tile += "[x]";
                            else tile += "[o]";
                            break;
                        case Checkers.PieceType.Crown:
                            if (checkersModel.boardTile[i, j].piece.player == Checkers.Playertype.X) tile += "[X]";
                            else tile += "[O]";
                            break;
                        case Checkers.PieceType.Empty:
                        default:
                            tile += "[ ]";
                            break;
                    }
                }

                TellUser(tile);
            }

            Pause(pauseTime / 6);
        }

        // Board UI
        public void ShowBoardState()
        {
            TellUser($"It is {(checkersModel.currentPlayer == Checkers.Playertype.X ? "white's" : "black's")} turn.");
            if(checkersModel.currentPieceIndex != null) TellUser("Selected Piece : "
                + "[" + checkersModel.currentPieceIndex.xPos + "]"
                + "[" + checkersModel.currentPieceIndex.yPos + "]");
            if (checkersModel.currentMoveIndex != null) TellUser("Selected Move : "
                + "[" + checkersModel.currentMoveIndex.xPos + "]"
                + "[" + checkersModel.currentMoveIndex.yPos + "]");
        }

        // Abstract the out to user statment for different platforms
        public void TellUser(string statment)
        {
            Console.WriteLine(statment + "\n");
        }

        // Abstract the get user input statment for different platforms
        public string AskUser()
        {
            return Console.ReadLine();
        }

        // Abstract pauseing the game.
        public void Pause(int duration)
        {
            Thread.Sleep(duration);
        }

        // Abstract clear screen.
        public void Clear()
        {
            Console.Clear();
        }

    }
}
