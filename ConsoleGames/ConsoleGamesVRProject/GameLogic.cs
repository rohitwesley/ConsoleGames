using System;
using System.Threading;

namespace Games
{

    /// <summary>
    /// The abstract class to use for all game logic code.
    /// </summary>
    public abstract class GameLogic
    {
        // ERIK: Hard to say if checkers has a high score (maybe?). If something
        // doesn't truly accomodate a general use case, just leave it specific! Specific
        // code that is contained doesn't hurt other classes, and is easy to move around.
        // Current HighScore 
        public float HighScore { get; private set; }

        static public Random rnd = new Random();

        // ERIK: Purge all unused fields and variables! They just make stuff harder
        // to read and debug.
        // Do make use of readonly for fields that are not modified after construction.
        public readonly int GameID;

        public GameLogic(int id)
        {
            GameID = id;
        }

        // All games are launched using this function to start playign the game.
        // (it is the first function you call and preferbly the only function you call of the game)
        public abstract void Play();

        // Use this to setup the game or reset the game.
        protected abstract void Setup();

    }

}