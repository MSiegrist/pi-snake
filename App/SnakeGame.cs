using Explorer700Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    internal class SnakeGame
    {
        public GameState State { get; private set; }

        public SnakeGame(int width, int length)
        {
            State = new GameState(width, length);
        }

        public GameState Tick(Keys input)
        {
            // Move snake direction according to Keys input - implement snake movement here
            switch (input)
            {
                case Keys.Up:
                    State.Snake.Direction = Direction.Up;
                    break;
                case Keys.Down:
                    State.Snake.Direction = Direction.Down;
                    break;
                case Keys.Left:
                    State.Snake.Direction = Direction.Left;
                    break;
                case Keys.Right:
                    State.Snake.Direction = Direction.Right;
                    break;
                default:
                    break;
            }

            //get current position 
            int currentHeadX = State.Snake.SnakePosition[0, 0];
            int currentHeadY = State.Snake.SnakePosition[0, 1];

            //init newHead and change according to position 
            int newHeadX = currentHeadX;
            int newHeadY = currentHeadY;
            switch (State.Snake.Direction)
            {
                case Direction.Up:
                    newHeadX = currentHeadX;
                    newHeadY = currentHeadY - 1;
                    break;
                case Direction.Down:
                    newHeadX = currentHeadX;
                    newHeadY = currentHeadY + 1;
                    break;
                case Direction.Left:
                    newHeadX = currentHeadX - 1;
                    newHeadY = currentHeadY;
                    break;
                case Direction.Right:
                    newHeadX = currentHeadX + 1;
                    newHeadY = currentHeadY;
                    break;
                default:
                    newHeadX = currentHeadX;
                    newHeadY = currentHeadY;
                    break;
            }

            //update position of newHead in the SnakePosition Array
            State.Snake.SnakePosition[0, 0] = newHeadX;
            State.Snake.SnakePosition[0, 1] = newHeadY;

            //check if snake collides with wall and set GameOverState accordingly
            if (newHeadX < 0 || newHeadX >= State.Playfield.GetLength(0) || newHeadY < 0 || newHeadY >= State.Playfield.GetLength(1))
            {
                //sets gameOver = true in the GameState Class
                State.SetGameOverState();
            }

            //check if snake collides / eats a fruit and increase store and generate a new fruit
            if (newHeadX == State.Fruit.posX && newHeadY == State.Fruit.posY)
            {
                State.IncreaseScore();
                Fruit newFruit = GenerateNewFruit(State.Playfield.GetLength(0), State.Playfield.GetLength(1), State.Snake);
                State.SetFruit(newFruit);
            }

            return State;

        }

        //basically same code as GenerateInitialFruit from GameState
        private Fruit GenerateNewFruit(int width, int length, Snake snake)
        {
            int maxAttempts = 10;
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                Fruit fruit = State.GenerateInitialFruit(width, length, snake);
                bool fruitOnSnake = false;
                for (int i = 0; i < snake.SnakePosition.Length; i++)
                {
                    if (snake.SnakePosition[i, 0] == fruit.posX && snake.SnakePosition[i, 1] == fruit.posY)
                    {
                        fruitOnSnake = true;
                        break;
                    }
                }
                if (!fruitOnSnake)
                {
                    return fruit;
                }
            }
            // Return a default fruit if no valid fruit could be generated (which should not happen?) 
            return new Fruit(0, 0);
        }

    }
}

