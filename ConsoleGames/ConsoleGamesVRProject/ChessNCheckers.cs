using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    class ChessNCheckers : Game
    {
        public event Action<int> OnCnCComplete;

        private List<Tile> boardTile;

        //Initialise Game Type ID
        public ChessNCheckers(int gameId) : base(gameId)
        {

        }

        public override void Play()
        {
            //OnCnCComplete(runners[0]);
        }

        protected override void Setup()
        {
            throw new NotImplementedException();
        }

        class Piece
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
