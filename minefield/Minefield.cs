using System;

namespace MinefieldGame
{
    public enum CellType
    {
        Bomb = 0,
        Path = 1,
        StartOrEnd = 3
    }

    public class Minefield
    {
        public int Rows { get; }
        public int Cols { get; }
        private readonly CellType[,] field;

        public Minefield(CellType[,] field)
        {
            Rows = field.GetLength(0);
            Cols = field.GetLength(1);
            this.field = field;
        }

        public CellType this[int x, int y] => field[x, y];

        public bool IsInBounds(int x, int y) => x >= 0 && y >= 0 && x < Rows && y < Cols;

        public bool IsTraversable(int x, int y, (int x, int y) end)
        {
            if (!IsInBounds(x, y)) return false;
            if ((x, y) == end) return field[x, y] == CellType.StartOrEnd;
            return field[x, y] == CellType.Path;
        }
    }
}
