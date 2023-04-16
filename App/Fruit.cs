using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    internal class Fruit
    {
        public int posX { get; private set; }
        public int posY { get; private set; }

        public Fruit(int x, int y)
        {
            posX = x;
            posY = y;
        }
    }

}
