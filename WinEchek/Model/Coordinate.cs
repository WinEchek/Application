using System;

namespace WinEchek.Model
{
    /// <summary>
    /// Square coordinate, independant of the board
    /// </summary>
    [Serializable]
    public struct Coordinate
    {
        /// <summary>
        /// X
        /// </summary>
        /// <value>
        /// The X coordinate
        /// </value>
        public int X { get; set; }

        /// <summary>
        /// Y
        /// </summary>
        /// <value>
        /// The Y coordinate
        /// </value>
        public int Y { get; set; }

        /// <summary>
        /// Coordinate constructor
        /// </summary>
        /// <param name="x">The X coordinate</param>
        /// <param name="y">The Y coordinate</param>
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Coordinate a, Coordinate b) => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Coordinate a, Coordinate b) => !(a == b);

        public override string ToString() => (char)('A' + X) + (8 - Y).ToString();
    }
}