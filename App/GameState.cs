using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    internal class GameState
    {
        public GameTile[,] Playfield { get; }

        public int Score { get; private set; }

        public bool GameOver { get; private set; }

        public GameState() { }
    }
}
