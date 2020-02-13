using System;
using Games;

namespace ConsoleGamesVRProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GameView gameLauncher = new GameView(0);
            gameLauncher.Play();

        }
    }
}
