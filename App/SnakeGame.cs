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

            Explorer700 exp = new Explorer700();
            Graphics g = exp.Display.Graphics;

            // Draw the borders of the game screen
            Graphics g = exp.Display.Graphics;
            Pen pen = new Pen(Brushes.White);

            // Draw top border
            g.DrawLine(pen, 0, 0, width * GameTile.Size, 0);

            // Draw right border
            g.DrawLine(pen, width * GameTile.Size, 0, width * GameTile.Size, length * GameTile.Size);

            // Draw bottom border
            g.DrawLine(pen, 0, length * GameTile.Size, width * GameTile.Size, length * GameTile.Size);

            // Draw left border
            g.DrawLine(pen, 0, 0, 0, length * GameTile.Size);

            // Draw the snake in the middle of the screen
            int centerX = width * GameTile.Size / 2;
            int centerY = length * GameTile.Size / 2;

            for (int i = 0; i < GameTile.Snake.Length; i++)
            {
                int x = centerX - GameTile.Snake.Length * GameTile.Size / 2 + i * GameTile.Size;
                int y = centerY;
                State.Playfield[x / GameTile.Size, y / GameTile.Size] = GameTile.Snake;
                g.FillRectangle(Brushes.White, x, y, GameTile.Size, GameTile.Size);
            }

            // Update the display
            exp.Display.Update();
        }
    }

        public GameState Tick(Keys input)
        {
            //Move snake according to Keys input - implement snake movement here
        }
    }
}

