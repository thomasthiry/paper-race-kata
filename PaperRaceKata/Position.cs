using System;

namespace PaperRaceKata
{
	public struct Position
	{
        public override string ToString()
        {
            return "x: "+X+"  y: "+Y;
        }

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Position other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}
	}
}