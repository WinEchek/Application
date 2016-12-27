using System;

namespace WinEchek.Model
{
    /// <summary>
    ///     Square coordinate, independant of the board
    /// </summary>
    [Serializable]
    public struct Coordinate
    {
        public bool Equals(Coordinate other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Coordinate && Equals((Coordinate) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }

        /// <summary>
        ///     X
        /// </summary>
        /// <value>
        ///     The X coordinate
        /// </value>
        public int X { get; set; }

        /// <summary>
        ///     Y
        /// </summary>
        /// <value>
        ///     The Y coordinate
        /// </value>
        public int Y { get; set; }

        /// <summary>
        ///     Coordinate constructor
        /// </summary>
        /// <param name="x">The X coordinate</param>
        /// <param name="y">The Y coordinate</param>
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Coordinate a, Coordinate b) => (a.X == b.X) && (a.Y == b.Y);

        public static bool operator !=(Coordinate a, Coordinate b) => !(a == b);

        public override string ToString() => (char) ('A' + X) + (8 - Y).ToString();
    }
}