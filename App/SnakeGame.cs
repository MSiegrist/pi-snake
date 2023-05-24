
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
                    //Logger.Log("Joystick: Left: ");
                    break;
                case Keys.Right:
                    dirX = 1;
                    //Logger.Log("Joystick: Right: ");
                    break;
                case Keys.Up:
                    dirY = -1;
                    //Logger.Log("Joystick: Up: ");
                    break;
                case Keys.Down:
                    dirY = 1;
                    //Logger.Log("Joystick: Down: ");
                    break;
            }

            bool directionChanged = false;

            // Prevent 180 degree turn into self
            if ((snake.HeadingX == 0 || snake.HeadingX != -dirX) && (snake.HeadingY == 0 || snake.HeadingY != -dirY))
            {
                //only log input if it changes the snake's direction
                if (dirX != 0 || dirY != 0)
                {
                    string joystickDirection = GetDirectionText(dirX, dirY);
                    Logger.Log($"Joystick: {joystickDirection}: ");
                    directionChanged = true;
                }

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
                Logger.Log("Game Over: Hit the border");
                State.GameOver = true;

                return State;
            }

            // Detect snake bites
            var tileAtNewHead = State.Playfield[newX, newY];
            if (tileAtNewHead == GameTile.Snake)
            {
                // Cannibalism is illegal
                Console.WriteLine("Game Over: Ate itself");
                Logger.Log("Game Over: Ate ifself");
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
                    Logger.Log("Game WIN: Playfield is full");
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

            if (!directionChanged)
            {
                Logger.ClearLastLog();
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

        private string GetDirectionText(int dirX, int dirY)
        {
            if (dirX == -1)
                return "Left";
            if (dirX == 1)
                return "Right";
            if (dirY == -1)
                return "Up";
            if (dirY == 1)
                return "Down";

            return "No direction";
        }
    }
}
