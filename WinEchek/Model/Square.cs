using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WinEchek.Annotations;

namespace WinEchek.Model
{
    /// <summary>
    /// Chess square
    /// </summary>
    [Serializable]
    public class Square : INotifyPropertyChanged
    {
        /// <summary>
        /// Board
        /// </summary>
        /// <value>
        /// The board the square is in
        /// </value>
        public Board Board { get; set; }

        private Piece.Piece _piece;
        /// <summary>
        /// Piece
        /// </summary>
        /// <value>
        /// The piece currently on the square
        /// </value>
        public Piece.Piece Piece
        {
            get
            {
                return _piece;
            }
            set
            {
                _piece = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// X
        /// </summary>
        /// <value>
        /// The X coordinate
        /// </value>
        public int X { get; }

        /// <summary>
        /// Y
        /// </summary>
        /// <value>
        /// The Y coordinate
        /// </value>
        public int Y { get; }

        /// <summary>
        /// Coordinate
        /// </summary>
        /// <value>
        /// Convenience property representing the X and Y coordinate of a square in a board
        /// </value>
        public Coordinate Coordinate => new Coordinate(X, Y);

        /// <summary>
        /// Square constructor
        /// </summary>
        /// <param name="parent">The board the square belongs to</param>
        /// <param name="x">The square X coordinate</param>
        /// <param name="y">The square Y coordinate</param>
        public Square(Board parent, int x, int y)
        {
            Board = parent;
            X = x;
            Y = y;
        }

        public override string ToString() => (char)('A' + X) + (8 - Y).ToString();

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
