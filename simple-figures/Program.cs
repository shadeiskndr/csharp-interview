using System;
using Geometry;

class Program
{
    static void Main(string[] args)
    {
        var p1 = new Point(0, 0);
        var p2 = new Point(3, 4);
        var line = new Line(new Point(0, 0), new Point(3, 4));
        var circle = new Circle(new Point(1, 1), 5);

        var aggregation = new Aggregation();
        aggregation.Add(p1);
        aggregation.Add(line);
        aggregation.Add(circle);

        Console.WriteLine("Original:");
        Console.WriteLine(p1);
        Console.WriteLine(line);
        Console.WriteLine(circle);
        Console.WriteLine(aggregation);

        aggregation.Move(2, 3);
        Console.WriteLine("\nAfter Move(2, 3):");
        Console.WriteLine(p1);
        Console.WriteLine(line);
        Console.WriteLine(circle);
        Console.WriteLine(aggregation);

        aggregation.Rotate(90, new Point(0, 0));
        Console.WriteLine("\nAfter Rotate(90, origin):");
        Console.WriteLine(p1);
        Console.WriteLine(line);
        Console.WriteLine(circle);
        Console.WriteLine(aggregation);
    }
}
