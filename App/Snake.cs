namespace App
{
    internal class Snake
    {
        public int HeadX, HeadY;
        public int HeadingX, HeadingY;
        private readonly Queue<SnakePart> parts = new();
        public int Length { get; private set; } = 3;

        public SnakePart? MoveTo(int newX, int newY)
        {
            HeadX = newX;
            HeadY = newY;
            parts.Enqueue(new SnakePart { X = newX, Y = newY });
            if (parts.Count > Length)
            {
                return parts.Dequeue();
            }
            return null;
        }

        public void Grow()
        {
            Length++;
        }
    }

    struct SnakePart
    {
        public int X, Y;
    }
}
