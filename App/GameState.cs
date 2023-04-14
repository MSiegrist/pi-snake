using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    /*
    The GameState class represents the current state of the game and keeps
    track of the playfield, score and gameOver State (bool) 
    */

    internal class GameState
    {
        public GameTile[,] Playfield { get; }

        public int Score { get; private set; }

        public bool GameOver { get; private set; }

        public Snake Snake { get; }

        public Fruit Fruit { get; private set; }

        //GameTile Size score and GameOverState will be initialized here 
        //this method will be called in SnakeGame Class
        public GameState(int width, int length)
        {
            Playfield = new GameTile[width, length];
            Score = 0;
            GameOver = false;
            //DifficultyEasy = difficultyEasy

            //Set initial Snake Position? --> Place it always in the middle
            int initialSnakeX = width / 2;
            int initialSnakeY = length / 2;
            Snake = new Snake(initialSnakeX, initialSnakeY);

            //Set First Fruit
            Fruit = GenerateInitialFruit(width, length, Snake);


        }

        public void IncreaseScore()
        {
            Score++;
        }

        public void SetGameOverState()
        {
            GameOver = true;
        }

        public Fruit GenerateInitialFruit(int width, int length, Snake snake)
        {
            int fruitX;
            int fruitY;
            bool fruitOnSnake;
            //simple error checking just in case 
            int maxAttempts = 10;

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                fruitX = new Random().Next(0, width);
                fruitY = new Random().Next(0, length);

                fruitOnSnake = false;
                for (int i = 0; i < snake.SnakePosition.Length; i++)
                {
                    if (snake.SnakePosition[i, 0] == fruitX && snake.SnakePosition[i, 1] == fruitY)
                    {
                        fruitOnSnake = true;
                        break;
                    }
                }

                if (!fruitOnSnake)
                {
                    return new Fruit(fruitX, fruitY);
                }
            }
            // Return a default fruit if no valid fruit could be generated (which should not happen?) 
            return new Fruit(0, 0);
        }

        public void SetFruit(Fruit fruit)
        {
            Fruit = fruit;
        }

    }
}
