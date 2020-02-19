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
        bool isP1Turn;

        // Initialise Game Type ID
        public ChessNCheckers(int gameId) : base(gameId)
        {

        }

        public override void Play()
        {
            Setup();
            PlayerTurnLoop();
            OnCnCComplete(0);
        }

        protected override void Setup()
        {
            isChess = AskToPlay();
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


        private bool PlayerTurnLoop()
        {
            // 1 sec pause time
            int pauseTime = 6000;
            // Show user the board
            Clear();
            DrawBoard();
            Pause(pauseTime/6);

            // Show toss coin animation
            int tossTime = rnd.Next(4,10);
            for (int tossTick = 0; tossTick < tossTime; tossTick++)
            {
                Clear();
                TellUser("Tossing Coin to see who starts.");
                string progressBar = "";
                for (int i = 0; i < tossTick; i++)
                {
                    progressBar += ".";
                }
                TellUser(progressBar); 
                isP1Turn = RollCoin();
                Pause(1000);
                if (isP1Turn) TellUser("Is it Player 1 ?");
                else TellUser("Is it Player 2 ?");
                Pause(1000);
            }

            // ask user if he wants to play again
            bool isEndofGame = false;
            while (!isEndofGame)
            {
                Clear();
                DrawBoard();
                Pause(pauseTime/4);
                if (isP1Turn) TellUser("Player 1's Turn :");
                else TellUser("Player 2's Turn :");

                TellUser("Please Choose a Pieces to move : (1-8,a-h)");
                string answer = "";
                answer = AskUser();
                string[] position = answer.Split(",");

                //clear the screen before next state
                Clear();

                if (position.Length != 2)
                {
                    TellUser("Please enter a valid input.");
                }
                else
                {
                    DrawBoard();
                    int xPawn = Convert.ToInt32(position[0]) - 1;//Get int value and normalise to array index 0-7
                    int yPawn = ((int)Convert.ToChar(position[1]) - 97);//Get ASCII value 97-104 (a-h)and normalise to arry index 0-7

                    Clear();
                    
                    if (isValidPawn(xPawn, yPawn, isP1Turn))
                    {
                        DrawBoard();
                        TellUser("The ASCII value of '" + position[1] + "' using (int)character: " + (int)Convert.ToChar(position[1]));
                        TellUser("Please Choose a diagnol tile to move to: (1-8,a-h)");
                        answer = AskUser();
                        string[] movePawn = answer.Split(",");

                        //clear the screen before next state
                        Clear();

                        if (movePawn.Length != 2)
                        {
                            TellUser("Please enter a valid input.");
                        }
                        else
                        {
                            DrawBoard();
                            int xTile = Convert.ToInt32(movePawn[0]) - 1;//Get int value and normalise to array index 0-7
                            int yTile = ((int)Convert.ToChar(movePawn[1]) - 97);//Get ASCII value 97-104 (a-h)and normalise to arry index 0-7

                            //clear the screen before next state
                            Clear();

                            if(isValidMove(xPawn, yPawn, xTile, yTile, isP1Turn))
                            {
                                TellUser("Valid Move");
                                // TODO place end of game state in the write place
                                isEndofGame = playMove(xPawn, yPawn, xTile, yTile, isP1Turn);
                                //next players turn
                                isP1Turn = !isP1Turn;
                            }
                            else
                            {
                                TellUser("Please select a valid tile to move your Pieces on the board.");
                            }

                        }

                        
                    }
                    else
                    {
                        TellUser("Please select one of your Pieces from the board.");
                    }


                }
                // pause at the end of every turn.
                Pause(pauseTime/2);
            }

            return isEndofGame;

        }

        private bool playMove(int xPawn, int yPawn, int xTile, int yTile, bool isP1Turn)
        {
            int xMove, yMove;
            if (xTile > xPawn) xMove = 1;
            else xMove = -1;
            if (yTile > yPawn) yMove = 1;
            else yMove = -1;

            // check if is chess or checkers
            if (isChess)
            {
                // TODO chess logic for valid pawn
                return false;
            }
            else
            {
                // check if is a valid players Piece
                if (isP1Turn)
                {
                    boardTile[xPawn, xPawn].value = " ";
                    boardTile[xTile, yTile].value = "x";
                    // valid if is oponent tile 
                    while (boardTile[xTile, yTile].value == "o" && ((xTile < 8 || xTile >= 0) && (yTile < 8 || yTile >= 0)))
                    {
                        int xNewTile = xTile + xMove;
                        int yNewTile = yTile + yMove;
                        //update traversing move
                        xPawn = xTile;
                        yPawn = yTile;
                        xTile = xNewTile;
                        yTile = yNewTile;
                        boardTile[xPawn, xPawn].value = " ";
                        boardTile[xTile, yTile].value = "x";

                    }
                    return isWinGame();
                }
                else
                {
                    boardTile[xPawn, xPawn].value = " ";
                    boardTile[xTile, yTile].value = "o";
                    // valid if is oponent tile 
                    while (boardTile[xTile, yTile].value == "x" && ((xTile < 8 || xTile >= 0) && (yTile < 8 || yTile >= 0)))
                    {
                        int xNewTile = xTile + xMove;
                        int yNewTile = yTile + yMove;
                        //update traversing move
                        xPawn = xTile;
                        yPawn = yTile;
                        xTile = xNewTile;
                        yTile = yNewTile;
                        boardTile[xPawn, xPawn].value = " ";
                        boardTile[xTile, yTile].value = "o";

                    }
                    return isWinGame();
                }

            }
        }

        private bool isWinGame()
        {
            return false;
        }

        private bool isValidMove(int xPawn, int yPawn, int xTile, int yTile, bool isP1Turn)
        {
            Clear();
            Pause(1000);
            TellUser("Piece : [" + xPawn + "," + yPawn + "]");
            TellUser("Options : [" + (xPawn + 1) + "," + (yPawn + 1) + "]");
            TellUser("Options : [" + (xPawn + 1) + "," + (yPawn - 1) + "]");
            TellUser("Options : [" + (xPawn - 1) + "," + (yPawn + 1) + "]");
            TellUser("Options : [" + (xPawn - 1) + "," + (yPawn - 1) + "]");
            TellUser("Move : [" + xTile + "," + yTile + "]");
            Pause(1000);
            int xMove, yMove;
            if (xTile > xPawn) xMove = 1;
            else xMove = -1;
            if (yTile > yPawn) yMove = 1;
            else yMove = -1;

            // check if is chess or checkers
            if (isChess)
            {
                // TODO chess logic for valid pawn
                return false;
            }
            else
            {
                // check if is a valid players Piece
                if (isP1Turn)
                {
                    // check if is diagonal tile
                    if ((xTile == xPawn + 1 || xTile == xPawn - 1) && (yTile == yPawn + 1 || yTile == yPawn - 1))
                    {
                        // valid if is oponent tile 
                        while (boardTile[xTile, yTile].value == "o" && ((xTile < 8 || xTile >= 0) && (yTile < 8 || yTile >= 0)))
                        {
                            int xNewTile = xTile + xMove;
                            int yNewTile = yTile + yMove;


                            //update traversing move
                            xPawn = xTile;
                            yPawn = yTile;
                            xTile = xNewTile;
                            yTile = yNewTile;

                            Clear();
                            Pause(1000);
                            TellUser("Piece : [" + xPawn + "," + yPawn + "]");
                            TellUser("Options : [" + (xPawn + 1) + "," + (yPawn + 1) + "]");
                            TellUser("Options : [" + (xPawn + 1) + "," + (yPawn - 1) + "]");
                            TellUser("Options : [" + (xPawn - 1) + "," + (yPawn + 1) + "]");
                            TellUser("Options : [" + (xPawn - 1) + "," + (yPawn - 1) + "]");
                            TellUser("Move : [" + xTile + "," + yTile + "]");
                            Pause(1000);

                        }
                        // valid if is empty tile 
                        if (boardTile[xTile, yTile].value == " ")
                        {
                            Clear();
                            Pause(1000);
                            TellUser("Piece : [" + xPawn + "," + yPawn + "]");
                            TellUser("Options : [" + (xPawn + 1) + "," + (yPawn + 1) + "]");
                            TellUser("Options : [" + (xPawn + 1) + "," + (yPawn - 1) + "]");
                            TellUser("Options : [" + (xPawn - 1) + "," + (yPawn + 1) + "]");
                            TellUser("Options : [" + (xPawn - 1) + "," + (yPawn - 1) + "]");
                            TellUser("Move : [" + xTile + "," + yTile + "]");
                            Pause(1000);

                            return true;
                        }
                        else
                        {
                            Clear();
                            Pause(1000);
                            TellUser("Piece : [" + xPawn + "," + yPawn + "]");
                            TellUser("Options : [" + (xPawn + 1) + "," + (yPawn + 1) + "]");
                            TellUser("Options : [" + (xPawn + 1) + "," + (yPawn - 1) + "]");
                            TellUser("Options : [" + (xPawn - 1) + "," + (yPawn + 1) + "]");
                            TellUser("Options : [" + (xPawn - 1) + "," + (yPawn - 1) + "]");
                            TellUser("Move : [" + xTile + "," + yTile + "]");
                            Pause(1000);

                            return false;
                        }
                    }
                    else
                        return false;
                }
                else
                {

                    // check if is diagonal tile
                    if ((xTile == xPawn + 1 || xTile == xPawn - 1) && (yTile == yPawn + 1 || yTile == yPawn - 1))
                    {
                        // valid if is oponent tile 
                        while (boardTile[xTile, yTile].value == "x" && ((xTile < 8 || xTile >= 0) && (yTile < 8 || yTile >= 0)))
                        {
                            int xNewTile = xTile + xMove;
                            int yNewTile = yTile + yMove;


                            //update traversing move
                            xPawn = xTile;
                            yPawn = yTile;
                            xTile = xNewTile;
                            yTile = yNewTile;

                            Clear();
                            Pause(1000);
                            TellUser("Piece : [" + xPawn + "," + yPawn + "]");
                            TellUser("Options : [" + (xPawn + 1) + "," + (yPawn + 1) + "]");
                            TellUser("Options : [" + (xPawn + 1) + "," + (yPawn - 1) + "]");
                            TellUser("Options : [" + (xPawn - 1) + "," + (yPawn + 1) + "]");
                            TellUser("Options : [" + (xPawn - 1) + "," + (yPawn - 1) + "]");
                            TellUser("Move : [" + xTile + "," + yTile + "]");
                            Pause(1000);

                        }
                        // valid if is empty tile 
                        if (boardTile[xTile, yTile].value == " ")
                        {
                            Clear();
                            Pause(1000);
                            TellUser("Piece : [" + xPawn + "," + yPawn + "]");
                            TellUser("Options : [" + (xPawn + 1) + "," + (yPawn + 1) + "]");
                            TellUser("Options : [" + (xPawn + 1) + "," + (yPawn - 1) + "]");
                            TellUser("Options : [" + (xPawn - 1) + "," + (yPawn + 1) + "]");
                            TellUser("Options : [" + (xPawn - 1) + "," + (yPawn - 1) + "]");
                            TellUser("Move : [" + xTile + "," + yTile + "]");
                            Pause(1000);

                            return true;
                        }
                        else
                        {
                            Clear();
                            Pause(1000);
                            TellUser("Piece : [" + xPawn + "," + yPawn + "]");
                            TellUser("Options : [" + (xPawn + 1) + "," + (yPawn + 1) + "]");
                            TellUser("Options : [" + (xPawn + 1) + "," + (yPawn - 1) + "]");
                            TellUser("Options : [" + (xPawn - 1) + "," + (yPawn + 1) + "]");
                            TellUser("Options : [" + (xPawn - 1) + "," + (yPawn - 1) + "]");
                            TellUser("Move : [" + xTile + "," + yTile + "]");
                            Pause(1000);

                            return false;
                        }
                    }
                    else
                        return false;
                }

            }

        }

        private bool isValidPawn(int xPawn, int yPawn, bool isP1Turn)
        {
            // check if is a valid place on the board
            if (xPawn >= 0 && xPawn <= 7 && yPawn >= 0 && yPawn <= 7)
            {
                // check if is chess or checkers
                if(isChess)
                {
                    // TODO chess logic for valid pawn
                    return false;
                }
                else
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
            }
            else return false;
        }

        class Tile
        {
            public string value;
        }

    }
}
