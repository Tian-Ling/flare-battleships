using System;
namespace battleships.Models
{
    public class Coordinate
    {
        public int X { get; set; }

        public int Y { get; set; }


        public Coordinate(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !obj.GetType().Equals(this.GetType()))
            {
                return false;
            }

            var coord = obj as Coordinate;

            return coord.X == this.X && coord.Y == this.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return "(" + this.X + ", " + this.Y + ")";
        }
    }
}
