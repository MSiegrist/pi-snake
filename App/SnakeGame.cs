
using Explorer700Library;

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

            // Correlate input to change on playfield grid
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

            // Prevent 180 degree turn into self
            if ((snake.HeadingX == 0 || snake.HeadingX != -dirX) && (snake.HeadingY == 0 || snake.HeadingY != -dirY))
            {
                // Move normally
                snake.HeadingX = dirX;
                snake.HeadingY = dirY;
            }
            else
            {
                // Use previous snake direction if input is opposite of moving direction (to not instantly eat yourself)
                dirX = snake.HeadingX;
                dirY = snake.HeadingY;
            }

            // Calculate new position
            int newX = snake.HeadX + dirX;
            int newY = snake.HeadY + dirY;

            // Detect border colission
            if (newX < 0 || newX >= State.Playfield.GetLength(0) || newY < 0 || newY >= State.Playfield.GetLength(1))
            {
                // Out of bounds, perish
                Console.WriteLine("Game Over: Hit the border");
                State.GameOver = true;

                return State;
            }

            // Detect snake bites
            var tileAtNewHead = State.Playfield[newX, newY];
            if (tileAtNewHead == GameTile.Snake)
            {
                // Cannibalism is illegal
                Console.WriteLine("Game Over: Ate itself");
                State.GameOver = true;

                return State;
            }

            // Detect fruit being eaten
            if (tileAtNewHead == GameTile.Fruit)
            {
                // Ate a fruit
                State.AteFruit();
                if (State.IsFull())
                {
                    // No more space to place a new fruit
                    Console.WriteLine("Game WIN: Playfield is full");
                    State.GameOver = true;
                    // Do not return early so snake moves onto the final field
                }
                else
                {
                    GenerateFruit();
                }
                State.Playfield[newX, newY] = GameTile.None;
            }

            // Add snake head to playfield
            State.Playfield[newX, newY] = GameTile.Snake;

            // Remove tail of snake if required
            var assToRemove = snake.MoveTo(newX, newY);
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
