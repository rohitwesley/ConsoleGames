// ERIK: Make sure to remove all the fluff from my style guide.

// Comments have a space after the slashes, and end in a period.
// Long comments should use multiple lines, either with a double
// slash before each, or using /* */ comment blocks.

// Remove any unused using statements.
using System;
using System.Threading;

// Do use namespaces to avoid collisions with libraries.
// Do use sub namespaces for groups of classes with a shared
// functionality.
namespace Games
{

    // ERIK: This class has some useful functionality in it that is general to 
    // different console games (pausing, printing data, accepting user input),
    // but it really doesn't have anything to do with the games themselves (chess
    // doesn't care about pausing). This is more of a generalized game controller.
    /// <summary>
    /// The abstract class to use for all game logic code.
    /// </summary>
    // Shown here as the parent class of All Game Logic.
    public abstract class Game
    {
        // ERIK: Hard to say if checkers has a high score (maybe?). If something
        // doesn't truly accomodate a general use case, just leave it specific! Specific
        // code that is contained doesn't hurt other classes, and is easy to move around.
        // Current HighScore 
        public float HighScore { get; private set; }

        static protected Random rnd = new Random();

        // ERIK: Purge all unused fields and variables! They just make stuff harder
        // to read and debug.
        // Do make use of readonly for fields that are not modified after construction.
        public readonly int GameID;

        public Game(int id)
        {
            GameID = id;
        }

        // All games are launched using this function to start playign the game.
        // (it is the first function you call and preferbly the only function you call of the game)
        public abstract void Play();

        // Use this to setup the game or reset the game.
        protected abstract void Setup();

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



        // Abstract Random Generator for Coin Toss
        public bool RollCoin()
        {
            if (rnd.Next() % 2 == 0)
                return true;
            else
                return false;
        }

}

}