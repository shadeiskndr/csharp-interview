namespace MinefieldGame
{
    public class Character
    {
        public string Name { get; }
        public (int x, int y) Position { get; private set; }

        public Character(string name, (int x, int y) startPosition)
        {
            Name = name;
            Position = startPosition;
        }

        public void MoveTo((int x, int y) newPosition)
        {
            Position = newPosition;
        }
    }
}
