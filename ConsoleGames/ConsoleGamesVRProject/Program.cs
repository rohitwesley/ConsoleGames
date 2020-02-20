using System;
using Games;

namespace ConsoleGamesVRProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // ERIK: Sort of a contradiction that a launcher gets instantiated as a new...view.
            // Don't worry if everything isn't initially cleanly separated into MVC; I find it's
            // easier to start with them all together (if this is a very iterative project) and break
            // them out AFTER functionality is present. Ideally though, leave the naming more general
            // so as not to introduce confusion. This shows *you* have a strong idea where the code is at
            // w.r.t. robustness/extensibility/modularity.
            GameView gameLauncher = new GameView(0);
            gameLauncher.Play();

        }
    }
}
