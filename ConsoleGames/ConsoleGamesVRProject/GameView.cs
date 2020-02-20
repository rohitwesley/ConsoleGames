using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    class GameView : Game
    {
        // ERIK: Below isn't super polymorphic. Let's assume GameView is intended to be something
        // that is interoperable will all instances of Game...this should mean that it wouldn't actually need
        // to know the specific concrete class type. I.e., the below would be valid:
        // Game bj;
        // Game race;
        // Game CheckersNCheckers;
        // If the above *couldn't* be done, then we would need a specific view class for each game.
        // This isn't necessarily a bad thing.
        // Wait holdup GameView and the games are child classes of Game :O

        Blackjack bj;
        Race race;
        ChessNCheckers CnC;

        //Initialise Game Type ID
        public GameView(int gameId) : base(gameId)
        {

        }

        public override void Play()
        {

            Setup();
            Clear();
            TellUser(" 1.Black Jack\n 2.Race \n 3.ChessNCheckers \n 4.Exit\nPick a Game ? (1-3) ");
            string answer = "";
            answer = AskUser();
            switch (answer)
            {
                case "1":
                    bj.Play();
                    break;
                case "2":
                    race.Play();
                    break;
                case "3":
                    CnC.Play();
                    break;
                case "4":
                default:
                    TellUser("Thanks !!!");
                    break;
            }
        }

        // setup and index all games
        protected override void Setup()
        {
            bj = new Blackjack(1);
            race = new Race(2);
            race.OnRaceComplete += GameOver_OnRaceComplete;
            CnC = new ChessNCheckers(3);
            CnC.OnCnCComplete += GameOver_OnCnCComplete;
        }

        // ERIK: The below yearns to be DRY: https://en.wikipedia.org/wiki/Don%27t_repeat_yourself
        //called when Race ends
        private void GameOver_OnRaceComplete(int obj)
        {
            if (AskToPlay())
                race.Play();
            else
                Play();
        }

        //called when Chess/Checkers ends
        private void GameOver_OnCnCComplete(int obj)
        {
            if (AskToPlay())
                CnC.Play();
            else
                Play();
        }

        // ERIK: This is very controller-y. What does a view care if someone wants to play or not?
        private bool AskToPlay()
        {
            //aske user if he want to play again
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


    }
}
