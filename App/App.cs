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
        private SnakeGame game = new SnakeGame(20, 9);
        private const int tileSize = 6;
        private Keys lastInput = Keys.Left;
        private readonly Explorer700 explorer;
        static readonly Keys[] CARDINAL_DIRECTIONS = new[] { Keys.Left, Keys.Right, Keys.Up, Keys.Down };

        public App()
        {
            explorer = new Explorer700();
        }

        public void Run()
        {
            explorer.Joystick.JoystickChanged += JoystickChanged;
            while (!game.State.GameOver)
            {
                GameState gameState = game.Tick(lastInput);
                UpdateDisplay(gameState);
                Thread.Sleep(500);
            }
            Console.WriteLine($"FINAL SCORE: {game.State.Score}");
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
            graphics.DrawRectangle(pen, new Rectangle(0, 0, width * tileSize + 4, height * tileSize + 4));

            // Draw playfield contents
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    GameTile tile = gameState.Playfield[x, y];
                    Rectangle tileRectangle = new Rectangle(x * tileSize + 2, y * tileSize + 2, tileSize, tileSize);
                    if (tile == GameTile.Snake)
                    {
                        graphics.FillRectangle(brush, tileRectangle);
                    }
                    else if (tile == GameTile.Fruit)
                    {
                        graphics.FillPie(brush, tileRectangle, 0, 360);
                    }
                }
            }

            if (gameState.GameOver)
            {
                DrawEndScreen(gameState);
            }

            explorer.Display.Update();
        }

        private void DrawEndScreen(GameState gameState)
        {
            Graphics graphics = explorer.Display.Graphics;

            string? endScreenText;
            if (gameState.IsFull())
            {
                // Show Win screen
                endScreenText = $"You Win!\nScore: {gameState.Score}";
            }
            else
            {
                // Show Game Over screen
                endScreenText = $"Game Over!\nScore: {gameState.Score}";
            }
            PointF textPosition = new PointF(2, 5);
            Font font = new Font(new FontFamily("arial"), 15, FontStyle.Bold);
            SizeF fontSpace = graphics.MeasureString(endScreenText, font);
            graphics.FillRectangle(new SolidBrush(Color.Black), textPosition.X, textPosition.Y, fontSpace.Width, fontSpace.Height);
            graphics.DrawString(endScreenText, font, Brushes.White, textPosition);
        }
    }
}
