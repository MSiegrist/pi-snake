using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    internal enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    internal class Snake
    {
        public int[,] SnakePosition { get; }
        private Direction direction;

        public Snake(int startX, int startY)
        {
            //initialize SnakePosition
            SnakePosition = new int[1, 2];

            //Set actual position of snake 
            SnakePosition[0, 0] = startX;
            SnakePosition[0, 1] = startY;

            //Set initial direction to left
            direction = Direction.Left;
        }

        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }
    }
}
