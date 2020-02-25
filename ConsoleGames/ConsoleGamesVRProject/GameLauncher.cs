using System;
using System.Collections.Generic;
using System.Threading; 

namespace Games
{
    class GameLauncher
    {
        Checkers currentGame;
        int gameId = 0;
        // 1 sec pause time
        int pauseTime = 6000;

        public void Play()
        {

            Setup();
            Clear();
            TellUser(" 1.Checkers \n 4.Exit\nPick a Game ? ");
            string answer;
            answer = AskUser();
            gameId = Convert.ToInt32(answer);
            switch (gameId)
            {
                case 1:
                    currentGame.Play();
                    break;
                default:
                    TellUser("Thanks !!!");
                    break;
            }
        }

        // setup and index all games
        protected void Setup()
        {
            currentGame = new Checkers(0);

            currentGame.OnDrawBoard += DrawBoard;
            currentGame.OnSetStartPlayer += TossCoin;
            currentGame.OnPlayerSelectPiece += PlayerSelectPiece;
            currentGame.OnPlayerMovePiece += PlayerMovement;
            currentGame.OnGameOver += GameOver;
        }

        // Piece Selection UI
        private void PlayerSelectPiece(Checkers.Tile[,] boardTile)
        {
            Checkers.Index piece = new Checkers.Index();
            do
            {
                DrawBoard(boardTile);
                TellUser("Please Choose a Pieces to move : (1-8,a-h)");
                string answer = AskUser();
                string[] position = answer.Split(",");
                if (position.Length != 2)
                {
                    Clear();
                    TellUser("Please enter a valid Pieces.");
                    Pause(pauseTime / 3);
                }
                else
                {
                    piece.xPos = Convert.ToInt32(position[0]) - 1;//Get int value and normalise to array index 0-7
                    piece.yPos = ((int)Convert.ToChar(position[1]) - 97);//Get ASCII value 97-104 (a-h)and normalise to arry index 0-7
                    if (!currentGame.isValidPiece(piece))
                    {
                        Clear();
                        TellUser("Please enter a valid Pieces.");
                        Pause(pauseTime / 3);
                    }
                }
            }
            while (!currentGame.isValidPiece(piece));

            TellUser("xPawn : " + piece.xPos);
            TellUser("yPawn : " + piece.yPos);
            currentGame.SetCurrentPiece(piece); 
        }

        // Tile Selection UI
        private void PlayerMovement(Checkers.Tile[,] moveTiles)
        {
            Clear();
            List<Checkers.Index> moves = currentGame.getValidMove(currentGame.currentPieceIndex);
            //if (moves != null)
            //{
            //    TellUser("Please Choose a move from the list: ");
            //    for (int i = 0; i < moves.Count; i++)
            //    {
            //        TellUser((i+1) + " : " + moves[i]);
            //    }
            //    string answer = AskUser();
            //    int moveIndex = Convert.ToInt32(answer) - 1;
            //    currentGame.PlayMove(moves[moveIndex]);
            //}
            //else
            //{
            //    TellUser("Invalid Moves...");
            //}
            Pause(pauseTime / 3);
            
        }

        // Toss a Coin UI
        private void TossCoin(bool isRand)
        {
            // Show toss coin animation
            if(isRand)
            {
                int tossTime = GameLogic.rnd.Next(4, 10);
                for (int tossTick = 0; tossTick < tossTime; tossTick++)
                {
                    Clear();
                    TellUser("Tossing Coin to see who starts.");
                    string progressBar = "";
                    for (int i = 0; i < tossTick; i++)
                    {
                        progressBar += currentGame.currentPlayer;
                    }
                    TellUser(progressBar);
                    if(NoiseGenerator.RollCoin())
                        currentGame.currentPlayer = Checkers.Playertype.X;
                    else
                        currentGame.currentPlayer = Checkers.Playertype.O;

                    Pause(1000);
                }
            }
            else
            {
                currentGame.currentPlayer = Checkers.Playertype.X;
            }
        }

        // Board UI
        private void DrawBoard(Checkers.Tile[,] boardTile)
        {
            Clear();
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
                    switch (boardTile[i, j].piece.pieceType)
                    {
                        case Checkers.PieceType.Pawn:
                            if (boardTile[i, j].piece.player == Checkers.Playertype.X) tile += "[x]";
                            else tile += "[o]";
                            break;
                        case Checkers.PieceType.Crown:
                            if (boardTile[i, j].piece.player == Checkers.Playertype.X) tile += "[X]";
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

            if (currentGame.currentPlayer == Checkers.Playertype.X) TellUser("Player x Turn :");
            else TellUser("Player o Turn :");
            Pause(pauseTime / 6);
        }

        // GameOver UI
        private void GameOver(Checkers.Playertype winner)
        {
            // state the winner
            if(winner != Checkers.Playertype.Empty) TellUser(winner +"Won");
            else TellUser(" Its a Draw...");
            Pause(pauseTime);

            //ask user if he want to play again
            bool isAskUser = true;
            while (isAskUser)
            {
                TellUser("Would you like to play again ? (y/n)");
                string answer = "";
                answer = AskUser();
                if (answer == "y")
                {
                    Play();
                }
                else if (answer == "n")
                {

                    //exite loop
                    isAskUser = false;
                }
                else
                {
                    TellUser("Please enter a valid input.");
                }
            }


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
