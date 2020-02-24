using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{

    /// <summary>
    /// Race Game Logic
    /// Use play to start.
    /// </summary>
    class Race : GameLogic
    {

        public event Action<int> OnRaceComplete;

        private List<int> runners;

        private Tile road;
        private Tile dirtRoad;
        private Tile upHill;
        private Tile downHill;
        private Tile downValley;
        private Tile upValley;
        private List<Tile> raceTrack;

        //Initialise Game Type ID
        public Race(int gameId) : base(gameId)
        {

        }

        public override void Play()
        {
            OnRaceComplete(runners[0]);
        }

        protected override void Setup()
        {
            road = new Tile();
            road.shape = "----";
            road.difficulty = 25f;

            dirtRoad = new Tile();
            dirtRoad.shape = "....";
            dirtRoad.difficulty = 12.5f;

            upHill = new Tile();
            upHill.shape = "/ \n / \n /";
            upHill.difficulty = 12.5f;

            downHill = new Tile();
            downHill.shape = "\\ \n \\ \n \\";
            downHill.difficulty = 25f;

            upHill = new Tile();
            upHill.shape = "/ \n / \n /";
            upHill.difficulty = 12.5f;

            downHill = new Tile();
            downHill.shape = "\\ \n \\ \n \\";
            downHill.difficulty = 25f;

        }

        class Runner
        {
            public char symbol;
            public int currentTile;
        }

        class Tile
        {
            public string shape;
            public float difficulty;
        }

    }
}
