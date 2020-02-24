using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    /// <summary>
    /// Game Logic to play Checkers
    /// </summary>
    class Checkers : GameLogic
    {
        public event Action<Tile[,]> OnDrawBoard;
        public event Action<bool> OnSetStartPlayer;
        public event Action<Tile[,]> OnPlayerSelectPiece;
        public event Action<Tile[,]> OnPlayerSelectTile;

        private Tile[,] boardTile;

        // active move
        public int xPawn,yPawn, xTile, yTile;

        public bool isP1Turn;

        // Initialise Game Type ID
        public Checkers(int gameId) : base(gameId)
        {

        }

        public override void Play()
        {
            Setup();
            PlayerTurnLoop();
        }

        protected override void Setup()
        {
            ResetBoardPieces();
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

            // Setup X Pieces for checkers
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

            // Setup O Pieces for checkers
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
                        boardTile[i, j].value = "o";

                    }
                    else if (!isOdd && j % 2 == 0)
                    {
                        boardTile[i, j].value = "o";
                    }
                }
 

           }

        }

        private bool PlayerTurnLoop()
        {
            // Show user the board
            OnDrawBoard?.Invoke(boardTile);
            OnSetStartPlayer?.Invoke(true);
            // ask user if he wants to play again
            bool isEndofGame = false;
            while (!isEndofGame)
            {
                OnDrawBoard?.Invoke(boardTile);
                OnPlayerSelectPiece?.Invoke(boardTile);
                OnPlayerSelectTile?.Invoke(boardTile);
                playMove(xPawn, yPawn, xTile, yTile, isP1Turn);
                isEndofGame = isWinGame();
                //next players turn
                isP1Turn = !isP1Turn;
            }

            return isEndofGame;

        }

        private void playMove(int xPawn, int yPawn, int xTile, int yTile, bool isP1Turn)
        {
            if((boardTile[xTile, yTile].value == " ")
                && (xTile == xPawn + 1 || xTile == xPawn - 1) && (yTile == yPawn + 1 || yTile == yPawn - 1))
            {
                boardTile[xTile, yTile].value = boardTile[xPawn, yPawn].value;
                boardTile[xPawn, yPawn].value = " ";
            }
        }

        private bool isWinGame()
        {
            return false;
        }

        public bool isValidMove(int xPawn, int yPawn, int xTile, int yTile, bool isP1Turn)
        {
            // check if is diagonal tile
            if ((boardTile[xTile, yTile].value == " ")
                && (xTile == xPawn + 1 || xTile == xPawn - 1) && (yTile == yPawn + 1 || yTile == yPawn - 1))
            {
                return true;
            }
            else
                return false;

        }

        public bool isValidPawn(int xPawn, int yPawn, bool isP1Turn)
        {
            // check if is a valid place on the board
            if (xPawn >= 0 && xPawn <= 7 && yPawn >= 0 && yPawn <= 7)
            {
                // check if is a valid players Piece
                if (isP1Turn)
                {
                    if (boardTile[xPawn, yPawn].value == "x")
                        return true;
                    else
                        return false;
                }
                else
                {

                    if (boardTile[xPawn, yPawn].value == "o")
                        return true;
                    else
                        return false;
                }
            }
            else return false;
        }

        public class Tile
        {
            public string value;
        }

    }
}
