using System;
using System.Collections.Generic;

namespace MinefieldGame
{
    public class Pathfinder
    {
        private static readonly int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        private static readonly int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

        public static List<(int x, int y)> FindSafePath(Minefield minefield, (int x, int y) start, (int x, int y) end)
        {
            int n = minefield.Rows;
            int m = minefield.Cols;

            if (minefield[start.x, start.y] != CellType.StartOrEnd)
            {
                Console.Error.WriteLine($"Error: Start position ({start.x},{start.y}) is not the designated start value (3). Found {(int)minefield[start.x, start.y]}.");
                return null;
            }

            var visited = new bool[n, m];
            var prev = new (int x, int y)?[n, m];
            var queue = new Queue<(int x, int y)>();

            queue.Enqueue(start);
            visited[start.x, start.y] = true;

            while (queue.Count > 0)
            {
                var curr = queue.Dequeue();

                if (curr == end)
                    break;

                for (int dir = 0; dir < 8; dir++)
                {
                    int nx = curr.x + dx[dir];
                    int ny = curr.y + dy[dir];

                    if (!minefield.IsInBounds(nx, ny) || visited[nx, ny])
                        continue;

                    bool isTargetEnd = (nx == end.x && ny == end.y);
                    bool isPathCell = (minefield[nx, ny] == CellType.Path);

                    if (!isTargetEnd && !isPathCell)
                        continue;

                    visited[nx, ny] = true;
                    prev[nx, ny] = curr;
                    queue.Enqueue((nx, ny));
                }
            }

            if (!visited[end.x, end.y])
                return null;

            var path = new List<(int x, int y)>();
            (int x, int y)? posOpt = end;
            while (posOpt != null)
            {
                var pos = posOpt.Value;
                path.Add(pos);
                posOpt = prev[pos.x, pos.y];
            }
            path.Reverse();
            return path;
        }
    }
}
