using System;

namespace Geometry
{
    // Interface for geometric figures that support Move and Rotate
    public interface IGeometricFigure
    {
        void Move(double dx, double dy);
        void Rotate(double angleDegrees, Point origin);
    }

    // Represents a point in 2D space
    public class Point : IGeometricFigure
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Move(double dx, double dy)
        {
            X += dx;
            Y += dy;
        }

        public void Rotate(double angleDegrees, Point origin)
        {
            double angleRadians = angleDegrees * Math.PI / 180.0;
            double cos = Math.Cos(angleRadians);
            double sin = Math.Sin(angleRadians);

            double translatedX = X - origin.X;
            double translatedY = Y - origin.Y;

            double rotatedX = translatedX * cos - translatedY * sin;
            double rotatedY = translatedX * sin + translatedY * cos;

            X = rotatedX + origin.X;
            Y = rotatedY + origin.Y;
        }

        public override string ToString() => $"({X}, {Y})";
    }

    public class Line : IGeometricFigure
    {
        public Point Start { get; }
        public Point End { get; }

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public double Length()
        {
            double dx = End.X - Start.X;
            double dy = End.Y - Start.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public void Move(double dx, double dy)
        {
            Start.Move(dx, dy);
            End.Move(dx, dy);
        }

        public void Rotate(double angleDegrees, Point origin)
        {
            Start.Rotate(angleDegrees, origin);
            End.Rotate(angleDegrees, origin);
        }

        public override string ToString() => $"Line[{Start} -> {End}]";
    }

    public class Circle : IGeometricFigure
    {
        public Point Center { get; }
        public double Radius { get; private set; }

        public Circle(Point center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public double Area() => Math.PI * Radius * Radius;
        public double Circumference() => 2 * Math.PI * Radius;

        public void Move(double dx, double dy)
        {
            Center.Move(dx, dy);
        }

        public void Rotate(double angleDegrees, Point origin)
        {
            Center.Rotate(angleDegrees, origin);
        }

        public override string ToString() => $"Circle[Center: {Center}, Radius: {Radius}]";
    }
}
