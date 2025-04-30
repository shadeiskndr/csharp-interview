using System;
using System.Collections.Generic;

namespace MinefieldGame
{
    class Program
    {
        static void Main()
        {
            var field = new CellType[,]
            {
                {CellType.Bomb, CellType.StartOrEnd, CellType.Bomb, CellType.Bomb, CellType.Bomb},
                {CellType.Safe, CellType.Safe, CellType.Bomb, CellType.Bomb, CellType.Safe},
                {CellType.Bomb, CellType.Bomb, CellType.Safe, CellType.Bomb, CellType.Safe},
                {CellType.Safe, CellType.Bomb, CellType.Bomb, CellType.Safe, CellType.Bomb},
                {CellType.Bomb, CellType.Safe, CellType.Bomb, CellType.Safe, CellType.Bomb},
                {CellType.Safe, CellType.Bomb, CellType.Safe, CellType.Bomb, CellType.Bomb},
                {CellType.Bomb, CellType.Bomb, CellType.StartOrEnd, CellType.Bomb, CellType.Bomb}
            };

            var minefield = new Minefield(field);
            var start = (x: 0, y: 1);
            var end = (x: 6, y: 2);

            var path = Pathfinder.FindSafePath(minefield, start, end);

            if (path == null)
            {
                Console.WriteLine("No safe path found!");
                return;
            }

            Console.WriteLine("Safe path for SnifferPup and Ally:");
            for (int i = 0; i < path.Count; i++)
            {
                var pos = path[i];
                Console.WriteLine($"Step {i + 1}: ({pos.x}, {pos.y})");
            }

            var snifferPup = new Character("SnifferPup", start);
            var ally = new Character("Ally", start);

            Console.WriteLine("\nAlly's trail:");
            for (int i = 1; i < path.Count; i++)
            {
                ally.MoveTo(path[i - 1]);
                snifferPup.MoveTo(path[i]);
                Console.WriteLine($"Step {i}: Ally at ({ally.Position.x}, {ally.Position.y}), SnifferPup at ({snifferPup.Position.x}, {snifferPup.Position.y})");
            }
            if (path.Count > 0)
            {
                ally.MoveTo(path[path.Count - 1]);
                Console.WriteLine($"Step {path.Count}: Ally at ({ally.Position.x}, {ally.Position.y})");
            }
        }
    }
}
