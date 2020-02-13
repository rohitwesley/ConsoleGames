using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    class GameView : Game
    {
        Blackjack bj;
        Race race;
        ChessNCheckers CnC;

        //Initialise Game Type ID
        public GameView(int gameId) : base(gameId)
        {

        }

        public override void Play()
        {

            //bj.Play();
            //race.Play();
            Setup();
            CnC.Play();
        }

        protected override void Setup()
        {
            bj = new Blackjack(1);
            race = new Race(2);
            race.OnRaceComplete += GameOver_OnRaceComplete;
            CnC = new ChessNCheckers(3);
            CnC.OnCnCComplete += GameOver_OnCnCComplete;
        }

        private void GameOver_OnRaceComplete(int obj)
        {
            if (AskToPlay())
                race.Play();
            else
                TellUser("Thanks !!!");
        }

        private void GameOver_OnCnCComplete(int obj)
        {
            if (AskToPlay())
                CnC.Play();
            else
                TellUser("Thanks !!!");
        }

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
