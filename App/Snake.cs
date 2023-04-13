
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    internal class Snake
    {
        public int HeadX, HeadY;
        public int HeadingX, HeadingY;
        private readonly Queue<SnakePart> parts = new();
        private int length = 3;

        public SnakePart? MoveTo(int newX, int newY)
        {
            HeadX = newX;
            HeadY = newY;
            parts.Enqueue(new SnakePart { X = newX, Y = newY });
            if (parts.Count > length)
            {
                return parts.Dequeue();
            }
            return null;
        }

        public void Grow()
        {
            length++;
        }
    }

    struct SnakePart
    {
        public int X, Y;
    }
}
