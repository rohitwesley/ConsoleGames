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
    class Checkers
    {

        public Tile[,] boardTile;

        // active move
        public Index currentMoveIndex;
        public Index currentPieceIndex;
        public Playertype currentPlayer;

        public void ResetBoardPieces()
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
                    tile.piece = new Piece();
                    tile.Reset();
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
                        Piece piece = new Piece
                        {
                            isActive = true,
                            player = Playertype.X,
                            pieceType = PieceType.Pawn
                        };
                        boardTile[i, j].Set(piece);

                    }
                    else if (!isOdd && j % 2 == 0)
                    {
                        Piece piece = new Piece
                        {
                            isActive = true,
                            player = Playertype.X,
                            pieceType = PieceType.Pawn
                        };
                        boardTile[i, j].Set(piece);
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
                        Piece piece = new Piece
                        {
                            isActive = true,
                            player = Playertype.O,
                            pieceType = PieceType.Pawn
                        };
                        boardTile[i, j].Set(piece);

                    }
                    else if (!isOdd && j % 2 == 0)
                    {
                        Piece piece = new Piece
                        {
                            isActive = true,
                            player = Playertype.O,
                            pieceType = PieceType.Pawn
                        };
                        boardTile[i, j].Set(piece);
                    }
                }
 

           }

        }

        public void SetCurrentPlayer(Playertype player)
        {
            currentPlayer = player;
            currentMoveIndex = null;
            currentPieceIndex = null;
        }

        public void SetCurrentPiece(Index piece)
        {
            currentPieceIndex = piece;
        }

        public void SetCurrentMove(Index move)
        {
            currentMoveIndex = move;
        }

        public bool TryMovePiece()
        {
            return false;
        }

        public bool IsGameOver()
        {
            bool isWon = false;
            //TODO check win state logic
            if (isWon)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Playertype GetWinner()
        {
            //TODO logic for setting winner
            return currentPlayer;
        }



        public void PlayMove(Index direction)
        {
            Playertype opponentPlayer = Playertype.X;
            if (currentPlayer == Playertype.X)
                opponentPlayer = Playertype.O;
            if (boardTile[currentPieceIndex.xPos + direction.xPos, currentPieceIndex.yPos + direction.yPos].isEmpty)
            {
                Index upperLeft = new Index
                {
                    xPos = currentPieceIndex.xPos + direction.xPos,
                    yPos = currentPieceIndex.yPos + direction.yPos,
                };
                boardTile[upperLeft.xPos, upperLeft.yPos].Set(boardTile[currentPieceIndex.xPos, currentPieceIndex.yPos].piece);
                boardTile[currentPieceIndex.xPos, currentPieceIndex.yPos].Reset();
            }
            else
            {
                Index tempMove = new Index
                {
                    xPos = currentPieceIndex.xPos,
                    yPos = currentPieceIndex.yPos,
                };
                do
                {
                    Index newMove = new Index
                    {
                        xPos = tempMove.xPos + direction.xPos,
                        yPos = tempMove.yPos + direction.yPos,
                    };
                    // move piece and delete opponent tile till you reach an empty tile
                    boardTile[newMove.xPos, newMove.yPos].Set(boardTile[tempMove.xPos, tempMove.yPos].piece);
                    boardTile[tempMove.xPos, tempMove.yPos].Reset();
                    tempMove = newMove;
                }
                while (boardTile[tempMove.xPos + direction.xPos, tempMove.yPos + direction.yPos].piece.player == opponentPlayer);

                Index emptyTile = new Index
                {
                    xPos = tempMove.xPos + direction.xPos,
                    yPos = tempMove.yPos + direction.yPos,
                };
                // move piece and delete opponent tile till you reach an empty tile
                boardTile[emptyTile.xPos, emptyTile.yPos].Set(boardTile[tempMove.xPos, tempMove.yPos].piece);
                boardTile[tempMove.xPos, tempMove.yPos].Reset();
            }
        }

        public List<Index> getValidMove(Index piece)
        {
            switch (boardTile[piece.xPos, piece.yPos].piece.pieceType)
            {
                case PieceType.Pawn:
                    return isPawnMove(piece);
                case PieceType.Crown:
                    return isCrownMove(piece);
                default:
                    return null;
            }

        }

        private List<Index> isPawnMove(Index piece)
        {
            List<Index> legalMoves = new List<Index>();
            Playertype opponentPlayer = Playertype.X;
            int rowDirection = 1;
            if (currentPlayer == Playertype.X)
            {
                opponentPlayer = Playertype.O;
                rowDirection = -1;
            }

            // check if is diagonal tile based on player
            Index direction = new Index
            {
                xPos = rowDirection,
                yPos = -1,
            };
            if (isDiagnolMove(piece, direction, opponentPlayer) != null)
                legalMoves.Add(isDiagnolMove(piece, direction, opponentPlayer));

            direction = new Index
            {
                xPos = rowDirection,
                yPos = 1,
            };
            if (isDiagnolMove(piece, direction, opponentPlayer) != null)
                legalMoves.Add(isDiagnolMove(piece, direction, opponentPlayer));

            return legalMoves;

        }

        private List<Index> isCrownMove(Index piece)
        {
            List<Index> legalMoves = null;
            Playertype opponentPlayer = Playertype.X;
            if (boardTile[piece.xPos, piece.yPos].piece.player == Playertype.X)
            {
                opponentPlayer = Playertype.O;
            }

            // check if is diagonal tile based on player
            Index direction = new Index
            {
                xPos = 1,
                yPos = -1,
            };
            if (isDiagnolMove(piece, direction, opponentPlayer) != null)
                legalMoves.Add(isDiagnolMove(piece, direction, opponentPlayer));

            direction = new Index
            {
                xPos = 1,
                yPos = 1,
            };
            if (isDiagnolMove(piece, direction, opponentPlayer) != null)
                legalMoves.Add(isDiagnolMove(piece, direction, opponentPlayer));

            direction = new Index
            {
                xPos = -1,
                yPos = 1,
            };
            if (isDiagnolMove(piece, direction, opponentPlayer) != null)
                legalMoves.Add(isDiagnolMove(piece, direction, opponentPlayer));

            direction = new Index
            {
                xPos = -1,
                yPos = -1,
            };
            if (isDiagnolMove(piece, direction, opponentPlayer) != null)
                legalMoves.Add(isDiagnolMove(piece, direction, opponentPlayer));

            return legalMoves;

        }

        private Index isDiagnolMove(Index piece,Index direction, Playertype opponentPlayer)
        {
            Index moveTile = new Index
            {
                xPos = piece.xPos + direction.xPos,
                yPos = piece.yPos + direction.yPos,
            };
            if (isValidMove(moveTile))
            {
                if (boardTile[moveTile.xPos, moveTile.yPos].isEmpty)
                {
                    return moveTile;
                }
                else
                {
                    Index tempMove = new Index
                    {
                        xPos = piece.xPos + direction.xPos,
                        yPos = piece.yPos + direction.yPos,
                    };
                    while (boardTile[tempMove.xPos, tempMove.yPos].piece.player == opponentPlayer)
                    {
                        Index newMove = new Index
                        {
                            xPos = tempMove.xPos + direction.xPos,
                            yPos = tempMove.yPos + direction.yPos,
                        };
                        if (!isValidMove(moveTile))
                            return null;
                        tempMove = newMove;
                    }
                    if (boardTile[tempMove.xPos, tempMove.yPos].isEmpty)
                    {
                        Index upperLeft = new Index
                        {
                            xPos = piece.xPos + direction.xPos,
                            yPos = piece.yPos + direction.yPos,
                        };
                        return upperLeft;
                    }
                    else
                        return null;
                }
            }
            else
                return null;

            
        }

        private bool isValidMove(Index piece)
        {
            if (((piece.xPos) >= 0 && (piece.xPos) <= 7)
                && ((piece.yPos) >= 0 && (piece.yPos) <= 7))
                return true;
            else
                return false;
        }

        // check if valid piece on the board and is the current players piece
        public bool isValidPiece(Index piece)
        {
            // check if is a valid place on the board
            if (piece.xPos >= 0 && piece.xPos <= 7 && piece.yPos >= 0 && piece.yPos <= 7)
            {
                // check if is a valid players Piece
                if (boardTile[piece.xPos, piece.yPos].piece.player == currentPlayer)
                    return true;
                else
                    return false;
            }
            else return false;
        }

        /// <summary>
        /// Board DataTypes
        /// </summary>

        // Type of Board Pieces in Checkers
        public enum PieceType
        {
            Empty,
            Pawn,
            Crown
        }

        // Type of Board Pieces in Checkers
        public enum Playertype
        {
            Empty,
            X,
            O
        }

        public class Tile
        {
            public bool isEmpty = true;
            public Piece piece;

            public void Reset()
            {
                isEmpty = false;
                piece.EmptyPiece();
            }

            public void Set(Piece updatePiece)
            {
                isEmpty = true;
                piece = updatePiece;
            }
        }

        // Piece class abstracted for account for 1D,2D,3D,NthD space 
        public class Piece
        {
            public bool isActive;
            public Playertype player;
            public PieceType pieceType;

            public void EmptyPiece()
            {
                isActive = false;
                player = Playertype.Empty;
                pieceType = PieceType.Empty;
            }

        }

        public class Index
        {
            public int xPos, yPos;
        }

    }
}
