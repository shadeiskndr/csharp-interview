using System;
using System.Collections.Generic;

namespace Geometry
{
    public class Aggregation : IGeometricFigure
    {
        private readonly List<IGeometricFigure> _figures = new List<IGeometricFigure>();

        public Aggregation() { }

        public void Add(IGeometricFigure figure)
        {
            if (figure == null) throw new ArgumentNullException(nameof(figure));
            _figures.Add(figure);
        }

        public bool Remove(IGeometricFigure figure)
        {
            return _figures.Remove(figure);
        }

        public void Move(double dx, double dy)
        {
            foreach (var figure in _figures)
            {
                figure.Move(dx, dy);
            }
        }

        public void Rotate(double angleDegrees, Point origin)
        {
            foreach (var figure in _figures)
            {
                figure.Rotate(angleDegrees, origin);
            }
        }

        public override string ToString()
        {
            var result = "Aggregation containing:\n";
            foreach (var figure in _figures)
            {
                result += "  " + figure.ToString() + "\n";
            }
            return result;
        }
    }
}
