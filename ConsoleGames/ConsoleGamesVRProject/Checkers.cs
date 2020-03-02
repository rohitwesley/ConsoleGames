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
            bool hasMoved = false;
            List<Checkers.Index> moves = getValidMove(currentPieceIndex);
            if (moves.Count > 0)
            {
                // check if move is possible
                for (int i = 0; i < moves.Count; i++)
                {
                    if (moves[i].xPos == currentMoveIndex.xPos
                        &&
                        moves[i].yPos == currentMoveIndex.yPos)
                    {
                        hasMoved = true;
                    }
                }
                // move piece
                if (hasMoved)
                {
                    Piece piece = boardTile[currentPieceIndex.xPos, currentPieceIndex.yPos].piece;
                    Piece pieceTile = new Piece
                    {
                        isActive = piece.isActive,
                        player = piece.player,
                        pieceType = piece.pieceType
                    };
                    piece = boardTile[currentMoveIndex.xPos, currentMoveIndex.yPos].piece;
                    Piece moveTile = new Piece
                    {
                        isActive = piece.isActive,
                        player = piece.player,
                        pieceType = piece.pieceType
                    };

                    boardTile[currentPieceIndex.xPos, currentPieceIndex.yPos].Reset();
                    if(boardTile[currentMoveIndex.xPos, currentMoveIndex.yPos].isEmpty)
                    {
                        boardTile[currentMoveIndex.xPos, currentMoveIndex.yPos].Set(pieceTile);
                    }
                    // if is opponent piece attack
                    else if (moveTile.player != currentPlayer)
                    {
                        // TODO follow down the best path attacking max opponents pieces till you reach a empty tile
                        boardTile[currentMoveIndex.xPos, currentMoveIndex.yPos].Set(pieceTile);

                        Index direction = new Index
                        {
                            xPos = currentMoveIndex.xPos - currentPieceIndex.xPos,
                            yPos = currentMoveIndex.yPos - currentPieceIndex.yPos
                        };

                        currentPieceIndex.xPos = currentMoveIndex.xPos;
                        currentPieceIndex.yPos = currentMoveIndex.yPos;

                        currentMoveIndex.xPos = currentPieceIndex.xPos + direction.xPos;
                        currentMoveIndex.yPos = currentPieceIndex.yPos + direction.yPos;
                        TryMovePiece();
                    }
                }
            }
            else
            {
                hasMoved = false;
            }
            return hasMoved;
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

        public List<Index> getValidMove(Index piece)
        {
            List<Index> legalMoves = new List<Index>();
            switch (boardTile[piece.xPos, piece.yPos].piece.pieceType)
            {
                case PieceType.Pawn:
                    legalMoves = isPawnMove(piece);
                    break;
                case PieceType.Crown:
                    legalMoves = isCrownMove(piece);
                    break;
                default:
                    legalMoves = getValidEmptyTiles(getAdjucentTiles(piece));
                    break;
            }
            return legalMoves;
        }

        // Get Adjucent Tiles without board bounds
        private List<Index> getAdjucentTiles(Index piece)
        {
            List<Index> legalMoves = new List<Index>();

            // check if is diagonal tile based on player
            Index direction = new Index();

            // Row Above Piece
            direction.xPos = piece.xPos + 1;
            direction.yPos = piece.yPos - 1;
            legalMoves.Add(direction);

            direction = new Index();
            direction.xPos = piece.xPos + 1;
            direction.yPos = piece.yPos;
            legalMoves.Add(direction);

            direction = new Index();
            direction.xPos = piece.xPos + 1;
            direction.yPos = piece.yPos + 1;
            legalMoves.Add(direction);


            // Same Row as Piece
            direction = new Index();
            direction.xPos = piece.xPos;
            direction.yPos = piece.yPos - 1;
            legalMoves.Add(direction);

            // This is where the piece is. just put for illustratoin purpose.
            //direction = new Index();
            //direction.xPos = piece.xPos;
            //direction.yPos = piece.yPos;
            //legalMoves.Add(direction);

            direction = new Index();
            direction.xPos = piece.xPos;
            direction.yPos = piece.yPos + 1;
            legalMoves.Add(direction);


            // Row Below Piece
            direction = new Index();
            direction.xPos = piece.xPos - 1;
            direction.yPos = piece.yPos - 1;
            legalMoves.Add(direction);

            direction = new Index();
            direction.xPos = piece.xPos - 1;
            direction.yPos = piece.yPos;
            legalMoves.Add(direction);

            direction = new Index();
            direction.xPos = piece.xPos - 1;
            direction.yPos = piece.yPos + 1;
            legalMoves.Add(direction);

            return legalMoves;
        }

        // Get Diagnol Tiles without board bounds
        private List<Index> getDiagnolTiles(Index piece)
        {
            List<Index> adjacentMoves = getAdjucentTiles(piece);

            List<Index> legalMoves = new List<Index>();
            //ignore other tiles and select only diagnols
            legalMoves.Add(adjacentMoves[0]);
            legalMoves.Add(adjacentMoves[2]);
            legalMoves.Add(adjacentMoves[5]);
            legalMoves.Add(adjacentMoves[7]);
            return legalMoves;
        }

        // Get Empty Tiles within board bounds
        private List<Index> getValidEmptyTiles(List<Index> moves)
        {
            List<Index> legalMoves = new List<Index>();
            for (int i = 0; i < moves.Count; i++)
            {
                if (isValidEmptyTile(moves[i]))
                    legalMoves.Add(moves[i]);
            }
            return legalMoves;
        }

        // Get Occupied Tiles within board bounds
        private List<Index> getValidOccupiedTiles(List<Index> moves)
        {
            List<Index> legalMoves = new List<Index>();
            for (int i = 0; i < moves.Count; i++)
            {
                if (isValidOccupiedTile(moves[i]))
                    legalMoves.Add(moves[i]);
            }
            return legalMoves;
        }


        // is a checkers pawn piece that can only move diagnoly in one direction depending on whos piece
        private List<Index> isPawnMove(Index piece)
        {
            List<Index> legalMoves = new List<Index>();

            List<Index> diagnolPlayerTiles = getDiagnolPlayerTiles(piece);

            List<Index> diagnolEmptyTiles = getValidEmptyTiles(diagnolPlayerTiles);
            for (int j = 0; j < diagnolEmptyTiles.Count; j++)
            {
                legalMoves.Add(diagnolEmptyTiles[j]);
            }
            List<Index> diagnolOccupiedTiles = getValidOccupiedTiles(diagnolPlayerTiles);
            List<Index> diagnolAttackTiles = getValidOccupiedTiles(diagnolPlayerTiles);


            // TODO check logic to account for attack moves (recursive) currently check if ony 1st depth has empty tile
            int i = 0;
            bool isSearching = true;
            while (isSearching)
            {
                if (i >= diagnolOccupiedTiles.Count)
                    isSearching = false;
                else
                {
                    // if occupied tile has opponent piece add to search tree
                    if (boardTile[diagnolOccupiedTiles[i].xPos, diagnolOccupiedTiles[i].yPos].piece.player != currentPlayer)
                    {
                        diagnolPlayerTiles = getDiagnolPlayerTiles(diagnolOccupiedTiles[i]);

                    }

                    diagnolEmptyTiles = getValidEmptyTiles(diagnolPlayerTiles);
                    if(diagnolEmptyTiles.Count>0)
                        legalMoves.Add(diagnolOccupiedTiles[i]);
                    i++;
                }
            }


            return legalMoves;
        }

        private List<Index> getDiagnolPlayerTiles(Index piece)
        {
            List<Index> diagnolTiles = getDiagnolTiles(piece);

            List<Index> diagnolPlayerTiles = new List<Index>();
            if (currentPlayer == Playertype.X)
            {
                diagnolPlayerTiles.Add(diagnolTiles[0]);
                diagnolPlayerTiles.Add(diagnolTiles[1]);
            }
            else
            {
                diagnolPlayerTiles.Add(diagnolTiles[2]);
                diagnolPlayerTiles.Add(diagnolTiles[3]);
            }
            return diagnolPlayerTiles;
        }

        // is a checkers crown piece that can go in any direction diagnoly.
        private List<Index> isCrownMove(Index piece)
        {
            List<Index> legalMoves = new List<Index>();
            legalMoves = getDiagnolTiles(piece);
            return getValidEmptyTiles(legalMoves);

        }


        // check is a empty tile within the bounds of the board
        private bool isValidEmptyTile(Index direction)
        {
            if (isValidMove(direction) && boardTile[direction.xPos, direction.yPos].isEmpty)
                return true;
            else
                return false;
        }

        // check is a occupied tile within the bounds of the board
        private bool isValidOccupiedTile(Index direction)
        {
            if (isValidMove(direction) && !boardTile[direction.xPos, direction.yPos].isEmpty)
                return true;
            else
                return false;
        }

        // check for board bounds
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
                isEmpty = true;
                piece.EmptyPiece();
            }

            public void Set(Piece updatePiece)
            {
                isEmpty = false;
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
