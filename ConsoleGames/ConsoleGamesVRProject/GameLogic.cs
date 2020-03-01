using System;
using System.Threading;

namespace Games
{

    /// <summary>
    /// The abstract class to use for all game logic code.
    /// </summary>
    public abstract class GameLogic
    {
        // All games are launched using this function to start playign the game.
        // (it is the first function you call and preferbly the only function you call of the game)
        public abstract void Play();

        // Use this to setup the game or reset the game.
        protected abstract void Setup();

    }

}