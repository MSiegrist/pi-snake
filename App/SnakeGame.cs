
using Explorer700Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;

namespace App
{
    internal class SnakeGame
    {
        public GameState State { get; private set; }

        public SnakeGame(int width, int length)
        {
            State = new GameState(width, length);
            GenerateFruit();
            
        }

        public GameState Tick(Keys input)
        {
            //Move snake according to Keys input - implement snake movement here
            Snake snake = State.Snake;

            int dirX = 0, dirY = 0;
            switch (input)
            {
                case Keys.Left:
                    dirX = -1;
                    break;
                case Keys.Right:
                    dirX = 1;
                    break;
                case Keys.Up:
                    dirY = -1;
                    break;
                case Keys.Down:
                    dirY = 1;
                    break;
            }
            if ((snake.HeadingX == 0 || snake.HeadingX != -dirX) && (snake.HeadingY == 0 || snake.HeadingY != -dirY))
            {
                snake.HeadingX = dirX;
                snake.HeadingY = dirY;
            } else
            {
                dirX = snake.HeadingX;
                dirY = snake.HeadingY;
            }
            int newX = snake.HeadX + dirX;
            int newY = snake.HeadY + dirY;
            if (newX < 0 || newX >= State.Playfield.GetLength(0) || newY < 0 || newY >= State.Playfield.GetLength(1))
            {
                // Out of bounds, perish
                Console.WriteLine("Out of bounds");
                State.GameOver = true;
                return State;
            }
            var tileAtNewHead = State.Playfield[newX, newY];
            if (tileAtNewHead == GameTile.Snake)
            {
                // Cannibalism is illegal
                Console.WriteLine("Ate itself");
                State.GameOver = true;
                return State;
            } else if (tileAtNewHead == GameTile.Fruit)
            {
                State.AteFruit();
                GenerateFruit();
                State.Playfield[newX, newY] = GameTile.None;
            }

            var assToRemove = snake.MoveTo(newX, newY);
            State.Playfield[newX, newY] = GameTile.Snake;
            if (assToRemove != null)
            {
                State.Playfield[assToRemove.Value.X, assToRemove.Value.Y] = GameTile.None;
            }

            return State;
        }

        private void GenerateFruit()
        {
            for (int i = 0; i < 100; i++)
            {
                int fruitX = new Random().Next(0, State.Playfield.GetLength(0));
                int fruitY = new Random().Next(0, State.Playfield.GetLength(1));

                if (State.Playfield[fruitX, fruitY] == GameTile.None)
                {
                    State.Playfield[fruitX, fruitY] = GameTile.Fruit;
                    return;
                }
            }
            throw new Exception("Could not place fruit, aborting");
        }
    }
}
