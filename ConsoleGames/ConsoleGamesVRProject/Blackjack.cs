// Comments have a space after the slashes, and end in a period.
// Long comments should use multiple lines, either with a double
// slash before each, or using /* */ comment blocks.

// Remove any unused using statements.
using System;
using System.Collections.Generic;

// Do use namespaces to avoid collisions with libraries.
// Do use sub namespaces for groups of classes with a shared
// functionality.
namespace Games
{

    /// <summary>
    /// BlackJack Game Logic
    /// Use play to start.
    /// </summary>
    public class Blackjack : Game
    {

        // the money you have with you through a game.
        public float Money { get; private set; }

        // Do make use of readonly for fields that are not modified after construction.
        public readonly bool IsGoodGuide;

        List<Card> Deck = new List<Card>();

        enum Bets
        {
            Low = 10,
            Mid = 20,
            High = 40
        }

        //Initialise Game Type ID
        public Blackjack(int gameId) : base(gameId)
        {

        }

        /// <summary>
        /// Calls <see cref="AnotherMethod(int, float)"/> or something. I dunno.
        /// </summary>
        public override void Play()
        {
            // Do put spaces after commas separating arguments.
            Setup();
        }

        // Do use descriptive names for parameters, variables, methods, and functions.
        // Short names don't really serve much purpose anymore for most cases.
        protected override void Setup()
        {
            // Create all the 52 cards and put them in the deck list
            //TODO find a more optimised way to run this loop
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Card temp = new Card();
                    Suit temsuit = Suit.Ace;
                    if (i == 1) temsuit = Suit.Clubs;
                    if (i == 2) temsuit = Suit.Hearts;
                    if (i == 3) temsuit = Suit.Spades;

                    temp = new Card();
                    temp.no = Number.One;
                    temp.suit = temsuit;
                    Deck.Add(temp);

                    temp = new Card();
                    temp.no = Number.Two;
                    temp.suit = temsuit;
                    Deck.Add(temp);

                    temp = new Card();
                    temp.no = Number.Three;
                    temp.suit = temsuit;
                    Deck.Add(temp);

                    temp = new Card();
                    temp.no = Number.Four;
                    temp.suit = temsuit;
                    Deck.Add(temp);

                    temp = new Card();
                    temp.no = Number.Five;
                    temp.suit = temsuit;
                    Deck.Add(temp);

                    temp = new Card();
                    temp.no = Number.Six;
                    temp.suit = temsuit;
                    Deck.Add(temp);

                    temp = new Card();
                    temp.no = Number.Seven;
                    temp.suit = temsuit;
                    Deck.Add(temp);

                    temp = new Card();
                    temp.no = Number.Eight;
                    temp.suit = temsuit;
                    Deck.Add(temp);

                    temp = new Card();
                    temp.no = Number.Nine;
                    temp.suit = temsuit;
                    Deck.Add(temp);

                    temp = new Card();
                    temp.no = Number.Ten;
                    temp.suit = temsuit;
                    Deck.Add(temp);

                    temp = new Card();
                    temp.no = Number.Jack;
                    temp.suit = temsuit;
                    Deck.Add(temp);

                    temp = new Card();
                    temp.no = Number.Queen;
                    temp.suit = temsuit;
                    Deck.Add(temp);

                    temp = new Card();
                    temp.no = Number.King;
                    temp.suit = temsuit;
                    Deck.Add(temp);
                }
            }
            Money = 100;

            DrawCards();
        }

        private List<Card> ShuffleDeck<Card>(List<Card> inputList)
        {
            List<Card> randomList = new List<Card>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }

        private void DrawCards()
        {



            bool isPassed = false;
            List<Card> DealerDeck = new List<Card>();
            List<Card> UserDeck = new List<Card>();
            string answer = "";
            float bet = 0;
            while (!isPassed)
            {
                bool isBetting = true;
                while (isBetting)
                {
                    TellUser("Welcome To Black Jack :");
                    Pause(80);
                    TellUser("Choose your base bet :");
                    TellUser("1. Low");
                    TellUser("2. Mid");
                    TellUser("3. High");
                    answer = AskUser();
                    int betChoice = Convert.ToInt32(answer);
                    TellUser("betChoice: " + betChoice);
                    //if (betChoice > 0 && betChoice < 4)
                    //{
                    //    if (betChoice == 1)
                    //    {
                    //        bet = Convert.ToSingle(Bets.Low);
                    //    }
                    //    else if (betChoice == 2)
                    //    {
                    //        bet = Convert.ToSingle(Bets.Mid);
                    //    }
                    //    else if (betChoice == 3)
                    //    {
                    //        bet = Convert.ToSingle(Bets.High);
                    //    }

                    isBetting = false;
                    //}
                    //else
                    //{
                    //    TellUser("Enter a value between 1 - 3.");
                    //}
                }

                //TellUser("You bet $" + bet);
                //Money -= bet;
                //TellUser("You have $" + Money);
                //Pause(80);

                ////shuffle deck
                //TellUser("Shuffling Cards....");
                //Deck = ShuffleDeck(Deck);
                //Pause(160);

                //PrintDeck(Deck);
                //TellUser("-------------- ");
                ////deal cards to dealer
                //DealerDeck.Add(Deck[Deck.Count - 1]);
                //Deck.RemoveAt(Deck.Count - 1);
                //DealerDeck.Add(Deck[Deck.Count - 1]);
                //Deck.RemoveAt(Deck.Count - 1);
                ////deal cards to user
                //UserDeck.Add(Deck[Deck.Count - 1]);
                //Deck.RemoveAt(Deck.Count - 1);
                //UserDeck.Add(Deck[Deck.Count - 1]);
                //Deck.RemoveAt(Deck.Count - 1);
                //TellUser("User Cards:" + CheckDeck(UserDeck));
                //PrintDeck(UserDeck);
                //TellUser("Dealing Cards:" + CheckDeck(DealerDeck));
                //PrintDeck(DealerDeck);

                //bool isDraw = true;
                //if (CheckDeck(DealerDeck) < 21)
                //{

                //    TellUser("Choose to hit/stay/fold:");
                //    answer = AskUser();
                //    if (answer == "hit")
                //    {
                //        isDraw = true;
                //    }
                //    else
                //    {
                //        isDraw = false;
                //    }
                //}
                //else
                //{
                //    isDraw = false;
                //}
                //while (isDraw)
                //{
                //    //shuffle deck
                //    TellUser("Shuffling Cards....");
                //    Deck = ShuffleDeck(Deck);
                //    Pause(160);
                //    //deal cards to user
                //    UserDeck.Add(Deck[Deck.Count - 1]);
                //    Deck.RemoveAt(Deck.Count - 1);

                //    TellUser("User Cards:" + CheckDeck(UserDeck));
                //    PrintDeck(UserDeck);

                //    if (CheckDeck(UserDeck) < 21)
                //    {

                //        TellUser("Choose to hit/stay/fold:");
                //        answer = AskUser();
                //        if (answer == "hit")
                //        {
                //            isDraw = true;
                //        }
                //        else
                //        {
                //            isDraw = false;
                //        }
                //    }
                //    else
                //    {
                //        isDraw = false;
                //    }

                //}

                //// user says stay let dealer play
                //if (answer == "stay")
                //{
                //    isDraw = false;
                //    while (isDraw)
                //    {
                //        //shuffle deck
                //        TellUser("Shuffling Cards....");
                //        Deck = ShuffleDeck(Deck);
                //        Pause(160);
                //        //deal cards to user
                //        DealerDeck.Add(Deck[Deck.Count - 1]);
                //        Deck.RemoveAt(Deck.Count - 1);

                //        TellUser("Dealing Cards:" + CheckDeck(DealerDeck));
                //        PrintDeck(DealerDeck);
                //        if(CheckDeck(DealerDeck)<21)
                //        {

                //            TellUser("Choose to hit/stay/fold:");
                //            Random randDealer = new Random();
                //            int choice = randDealer.Next(0, 3);
                //            if (choice == 0) answer = "hit";
                //            if (choice == 1) answer = "stay";
                //            if (choice == 2) answer = "fold";
                //            TellUser("Dealer says: " + answer);

                //            if (answer == "hit")
                //            {
                //                isDraw = true;
                //            }
                //            else
                //            {
                //                isDraw = false;
                //            }
                //        }
                //        else
                //        {
                //            isDraw = false;
                //        }
                //    }
                //}

                ////if player has money ask if player wants to play again
                //if (Money > 10)
                //{
                //    TellUser("Do you want to continue to play (y/n): ");
                //    answer = AskUser();
                //}

                //if (Money < 10 || answer == "n")
                isPassed = true;
            }

            TellUser("Your got:" + Money);

        }

        private void PrintDeck(List<Card> deck)
        {
            for (int i = 0; i < deck.Count; i++)
            {
                TellUser("/" + deck[i].suit + deck[i].no + "/");
            }
        }

        private int CheckDeck(List<Card> deck)
        {
            int count = 0;
            for (int i = 0; i < deck.Count; i++)
            {
                count += Convert.ToInt32(deck[i].no);
            }
            return count;
        }

        public event Action OnGameStart;

        public void StartGame()
        {
            // setting up the game

            OnGameStart();
        }


    }


    // base assets

    public enum Suit
    {
        Ace,
        Spades,
        Hearts,
        Clubs,
    }

    public enum Number
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,

    }

    public class Card
    {
        // Add fields for the card's suit, name and score.
        public Number no;
        public Suit suit;

    }

}