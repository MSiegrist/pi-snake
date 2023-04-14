using Explorer700Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    internal class App
    {
        private SnakeGame game = new SnakeGame(25, 12);
        private const int tileSize = 5;
        private Keys lastInput = Keys.Left;
        private Explorer700 explorer;
        static readonly Keys[] CARDINAL_DIRECTIONS = new[] { Keys.Left, Keys.Right, Keys.Up, Keys.Down };


        public void Run()
        {
            explorer = new Explorer700();
            explorer.Joystick.JoystickChanged += JoystickChanged;
            while (!game.State.GameOver)
            {
                GameState gameState = game.Tick(lastInput);
                UpdateDisplay(gameState);
                Thread.Sleep(500);
            }
            Thread.Sleep(5000);
        }

        private void JoystickChanged(object? sender, KeyEventArgs e)
        {
            // Ignore diagonal jostick inputs
            var direction = CARDINAL_DIRECTIONS.FirstOrDefault(d => e.Keys == d);
            if (direction != Keys.NoKey)
            {
                lastInput = direction;
            }
        }

        private void UpdateDisplay(GameState gameState)
        {
            Graphics graphics = explorer.Display.Graphics;
            
            Pen pen = new Pen(Brushes.White);
            Brush brush = new SolidBrush(Color.White);
            int width = gameState.Playfield.GetLength(0);
            int height = gameState.Playfield.GetLength(1);

            // Clear display
            graphics.Clear(Color.Black);

            // Draw playfield borders
            graphics.DrawRectangle(pen, new Rectangle(0, 0, width * tileSize, height * tileSize));

            // Draw playfield contents
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    GameTile tile = gameState.Playfield[x, y];
                    if (tile == GameTile.Snake)
                    {
                        graphics.FillRectangle(brush, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize));
                    }
                    else if (tile == GameTile.Fruit)
                    {
                        graphics.FillPie(brush, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), 0, 360);
                    }
                }
            }

            if (gameState.GameOver)
            {
                string gameOverText = $"Game Over\nScore: {gameState.Score}";
                PointF textPosition = new PointF(2, 5);
                Font font = new Font(new FontFamily("arial"), 15, FontStyle.Bold);
                SizeF fontSpace = graphics.MeasureString(gameOverText, font);
                graphics.FillRectangle(new SolidBrush(Color.Black), textPosition.X, textPosition.Y, fontSpace.Width, fontSpace.Height);
                graphics.DrawString(gameOverText, font, Brushes.White, textPosition);
            }

            explorer.Display.Update();
        }
    }
}
