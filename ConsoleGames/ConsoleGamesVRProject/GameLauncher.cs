using System;
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
            string answer = "";
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
            currentGame.OnPlayerSelectTile += PlayerSelectTile;
        }

        // Piece Selection UI
        private void PlayerSelectPiece(Checkers.Tile[,] boardTile)
        {
            int xPawn = -1;
            int yPawn = -1;
            while (!currentGame.isValidPawn(xPawn, yPawn, currentGame.isP1Turn))
            {
                DrawBoard(boardTile);
                TellUser("Please Choose a Pieces to move : (1-8,a-h)");
                string answer = AskUser();
                string[] position = answer.Split(",");
                if (position.Length != 2)
                {
                    TellUser("Please enter a valid input.");
                }
                else
                {
                    xPawn = Convert.ToInt32(position[0]) - 1;//Get int value and normalise to array index 0-7
                    yPawn = ((int)Convert.ToChar(position[1]) - 97);//Get ASCII value 97-104 (a-h)and normalise to arry index 0-7

                }
            }
            TellUser("xPawn : " + xPawn);
            TellUser("yPawn : " + yPawn);
            currentGame.xPawn = xPawn;
            currentGame.yPawn = yPawn;
        }

        // Tile Selection UI
        private void PlayerSelectTile(Checkers.Tile[,] boardTile)
        {
            int xTile = -1;
            int yTile = -1;
            //check condition when updated with user values.-
            do
            {
                DrawBoard(boardTile);
                TellUser("Please Choose a diagnol tile to move to: (1-8,a-h)");
                string answer = AskUser();
                string[] movePawn = answer.Split(",");
                if (movePawn.Length != 2)
                {
                    TellUser("Please enter a valid input.");
                }
                else
                {
                    xTile = Convert.ToInt32(movePawn[0]) - 1;//Get int value and normalise to array index 0-7
                    yTile = ((int)Convert.ToChar(movePawn[1]) - 97);//Get ASCII value 97-104 (a-h)and normalise to arry index 0-7

                }
            }
            while (!currentGame.isValidMove(currentGame.xPawn, currentGame.yPawn, xTile, yTile, currentGame.isP1Turn));

            currentGame.xTile = xTile;
            currentGame.yTile = yTile;
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
                        progressBar += ".";
                    }
                    TellUser(progressBar);
                    currentGame.isP1Turn = NoiseGenerator.RollCoin();
                    Pause(1000);
                    if (currentGame.isP1Turn) TellUser("Is it Player 1 ?");
                    else TellUser("Is it Player 2 ?");
                    Pause(1000);
                }
            }
            else
            {
                currentGame.isP1Turn = true;
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
                    tile += "[" + boardTile[i, j].value + "]";
                }

                TellUser(tile);
            }

            if (currentGame.isP1Turn) TellUser("Player x Turn :");
            else TellUser("Player o Turn :");
            Pause(pauseTime / 6);
        }

        private bool AskToPlay()
        {
            //ask user if he want to play again
            bool isAskUser = true;
            while (isAskUser)
            {
                TellUser("Would you like to play again ? (y/n)");
                string answer = "";
                answer = AskUser();
                if (answer == "y")
                {
                    return isAskUser;
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

            return isAskUser;

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
