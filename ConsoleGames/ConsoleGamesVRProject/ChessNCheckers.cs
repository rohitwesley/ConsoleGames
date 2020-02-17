using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    class ChessNCheckers : Game
    {
        public event Action<int> OnCnCComplete;

        private Tile[,] boardTile;

        bool isChess;

        // Initialise Game Type ID
        public ChessNCheckers(int gameId) : base(gameId)
        {

        }

        public override void Play()
        {
            Setup();
            OnCnCComplete(0);
        }

        protected override void Setup()
        {
            isChess = AskToPlay();
            ResetBoardPieces();
            DrawBoard();
        }

        private void ResetBoardPieces()
        {
            boardTile = new Tile[8, 8];
            // Reset Tiles to empty
            //columns
            for (int i = 0; i < 8; i++)
            {
                // rows
                for (int j = 0; j < 8; j++)
                {
                    Tile tile = new Tile();
                    tile.value = " ";
                    boardTile[i, j] = tile;
                }
            }

            if(isChess)
            {
                // Setup Black Main Pieces
                boardTile[0, 0].value = "R";
                boardTile[0, 1].value = "N";
                boardTile[0, 2].value = "B";
                boardTile[0, 3].value = "Q";
                boardTile[0, 4].value = "K";
                boardTile[0, 5].value = "B";
                boardTile[0, 6].value = "N";
                boardTile[0, 7].value = "R";

                // Setup Black Pawn Pieces
                boardTile[1, 0].value = "P";
                boardTile[1, 1].value = "P";
                boardTile[1, 2].value = "P";
                boardTile[1, 3].value = "P";
                boardTile[1, 4].value = "P";
                boardTile[1, 5].value = "P";
                boardTile[1, 6].value = "P";
                boardTile[1, 7].value = "P";

                // Setup White Pawn Pieces
                boardTile[6, 0].value = "p";
                boardTile[6, 1].value = "p";
                boardTile[6, 2].value = "p";
                boardTile[6, 3].value = "p";
                boardTile[6, 4].value = "p";
                boardTile[6, 5].value = "p";
                boardTile[6, 6].value = "p";
                boardTile[6, 7].value = "p";

                // Setup White Main Pieces
                boardTile[7, 0].value = "r";
                boardTile[7, 1].value = "n";
                boardTile[7, 2].value = "b";
                boardTile[7, 3].value = "q";
                boardTile[7, 4].value = "k";
                boardTile[7, 5].value = "b";
                boardTile[7, 6].value = "n";
                boardTile[7, 7].value = "r";
            }
            else
            {

                // Setup X pieces for checkers
                bool isOdd = true;
                //columns
                for (int i = 0; i < 3; i++)
                {
                    isOdd = !isOdd;
                    // rows
                    for (int j = 0; j < 8; j++)
                    {
                        if (isOdd && j % 2 != 0)
                        {
                            boardTile[i, j].value = "x";

                        }
                        else if (!isOdd && j % 2 == 0)
                        {
                            boardTile[i, j].value = "x";
                        }
                    }
                }

                // Setup O pieces for checkers
                isOdd = false;
                //columns
                for (int i = 5; i < 8; i++)
                {
                    isOdd = !isOdd;
                    // rows
                    for (int j = 0; j < 8; j++)
                    {
                        if (isOdd && j % 2 != 0)
                        {
                            boardTile[i, j].value = "O";

                        }
                        else if (!isOdd && j % 2 == 0)
                        {
                            boardTile[i, j].value = "O";
                        }
                    }
                }

            }



        }

        private void DrawBoard()
        {
            Clear();
            // 8x8 board tiles
            string tile = "  [a][b][c][d][e][f][g][h]";
            TellUser(tile);
            // columns
            for (int i = 0; i < 8; i++)
            {
                tile = (i+1) + " ";
                // rows
                for (int j = 0; j < 8; j++)
                {
                    tile += "["+ boardTile[i,j].value + "]";
                }

                TellUser(tile);
            }

        }

        private bool AskToPlay()
        {
            Clear();
            //aske user if he want to play again
            bool isAskUser = true;
            while (isAskUser)
            {
                TellUser("Would you like to play\n 1.Chess or 2.Checkers ? (1-2)");
                string answer = "";
                answer = AskUser();
                if (answer == "1")
                {
                    return isAskUser;
                }
                else if (answer == "2")
                {

                    //exite loop
                    isAskUser = false;
                }
                else
                {
                    TellUser("Please enter a valid input.");
                }
            }

            return isAskUser;

        }

        class Tile
        {
            public string value;
        }

    }
}
