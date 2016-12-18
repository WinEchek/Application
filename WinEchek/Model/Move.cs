using System;
using System.Runtime.InteropServices;
using WinEchek.Annotations;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Model
{
    /// <summary>
    /// Represent a chess move attached to a board for now
    /// </summary>
    [Serializable]
    public class Move
    {
        //TODO make it independant of the board. A move shouldn't have any control logic. It's from a targetSquare to another one

        private Coordinate _startCoordinate;
        private Coordinate _targetCoordinate;
        private Board _board;

        /// <summary>
        /// Board
        /// </summary>
        /// <value> 
        /// The board the move uses to retrieve data
        /// </value>
        public Board Board
        {
            get { return _board; }
            set
            {
                if(value == null) throw new ArgumentNullException(nameof(value));
                _board = value;
            }
        }

        /// <summary>
        /// Piece
        /// </summary>
        /// <value>
        /// The piece concerned by the move
        /// </value>
        public Piece.Piece Piece => Board.Squares[_startCoordinate.X, _startCoordinate.Y].Piece;

        /// <summary>
        /// StartSquare
        /// </summary>
        /// <value>
        /// The square the piece is coming from
        /// </value>
        public Square StartSquare => Board.Squares[_startCoordinate.X, _startCoordinate.Y];

        /// <summary>
        /// TargetSquare
        /// </summary>
        /// <value>
        /// The square the piece is going to
        /// </value>
        [NotNull]
        public Square TargetSquare => Board.Squares[_targetCoordinate.X, _targetCoordinate.Y];

        /// <summary>
        /// PromotePieceType
        /// </summary>
        /// <value>
        /// The type of piece the piece promotes to
        /// </value>
        public Type PromotePieceType { get; }

        public Move(Piece.Piece piece, Square targetSquare)
        {
            Board = targetSquare.Board;
            _startCoordinate = piece.Square.Coordinate;
            _targetCoordinate = targetSquare.Coordinate;
        }

        //TODO remove the piece from this constructor
        public Move(Square startSquare, Square targetSquare)
        {
            Board = startSquare.Board;
            _startCoordinate = startSquare.Coordinate;
            _targetCoordinate = targetSquare.Coordinate;
        }

        /// <summary>
        /// Promoting move constructor
        /// </summary>
        /// <param name="startSquare">The square the piece is on</param>
        /// <param name="targetSquare">The square the piece goes to</param>
        /// <param name="promotePieceType">The type the piece promotes to</param>
        public Move(Square startSquare, Square targetSquare, Type promotePieceType)
        {
            Board = startSquare.Board;
            _startCoordinate = startSquare.Coordinate;
            _targetCoordinate = targetSquare.Coordinate;
            PromotePieceType = promotePieceType;
        }
    }

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
    }
}