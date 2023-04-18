using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    /// <summary>
    /// The GameState class represents the current state of the game and keeps 
    /// track of the playfield, score and gameOver State(bool)
    /// </summary>
    internal class GameState
    {
        public GameTile[,] Playfield { get; }

        public int Score { get; private set; }

        public bool GameOver { get; set; }

        public Snake Snake;

        public GameState(int width, int length)
        {
            Playfield = new GameTile[width, length];
            Score = 0;
            GameOver = false;

            // Set initial Snake Position to the middle of the playfield
            int initialSnakeX = width / 2;
            int initialSnakeY = length / 2;
            Snake = new Snake { HeadX = initialSnakeX, HeadY = initialSnakeY };
        }

        public void AteFruit()
        {
            Score++;
            Snake.Grow();
        }

        public bool IsFull()
        {
            return Playfield.Length <= Snake.Length;
        }
    }
}
