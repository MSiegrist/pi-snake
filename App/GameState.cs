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

        //Maybe add difficulty to GameState(int width, int length, bool difficultyEasy)? 
        //public bool DifficultEasy { get; private set; }


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
            Snake = new Snake[initialSnakeX, initialSnakeY];

            //Set First Fruit
            Fruit = GenerateInitialFruit(width, length);


        }

        public void IncreaseScore()
        {
            Score++;
        }

        public void SetGameOverState()
        {
            GameOver = true;
        }

        private Fruit GenerateInitialFruit(int width, int length)
        {
            int fruitX;
            int fruitY;
            bool fruitOnSnake;

            do
            {
                fruitX = new Random().Next(0, width);
                fruitY = new Random().Next(0, length);

                fruitOnSnake = false;
                foreach (var segment in Snake.Segments)
                {
                    if (segment.X == fruitX && segment.Y == fruitY)
                    {
                        fruitOnSnake = true;
                        break;
                    }
                }

            } while (fruitOnSnake);

            return new Fruit(fruitX, fruitY);
        }

    }

    internal class Snake
    {

    }
}
