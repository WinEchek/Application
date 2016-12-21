using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using WinEchek.Annotations;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Model
{
    /// <summary>
    /// Represent a chess move attached to a board for now
    /// </summary>
    [Serializable]
    [DataContract]
    public class Move
    {
        [DataMember]
        public Coordinate StartCoordinate { get; }

        [DataMember]
        public Coordinate TargetCoordinate { get; }

        [DataMember]
        public Color PieceColor { get; }

        [DataMember]
        public Type PieceType { get; }

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
        public Piece.Piece Piece => Board.Squares[StartCoordinate.X, StartCoordinate.Y].Piece;

        /// <summary>
        /// StartSquare
        /// </summary>
        /// <value>
        /// The square the piece is coming from
        /// </value>
        public Square StartSquare => Board.Squares[StartCoordinate.X, StartCoordinate.Y];

        /// <summary>
        /// TargetSquare
        /// </summary>
        /// <value>
        /// The square the piece is going to
        /// </value>
        [NotNull]
        public Square TargetSquare => Board.Squares[TargetCoordinate.X, TargetCoordinate.Y];
        /// <summary>
        /// PromotePieceType
        /// </summary>
        /// <value>
        /// The type of piece the piece promotes to
        /// </value>
        public Type PromotePieceType { get; }

        #region Constructors
        public Move(Piece.Piece piece, Square targetSquare)
        {
            Board = targetSquare.Board;
            PieceColor = piece.Color;
            PieceType = piece.Type;
            StartCoordinate = piece.Square.Coordinate;
            TargetCoordinate = targetSquare.Coordinate;
        }

        /// <summary>
        /// Move constructor
        /// </summary>
        /// <param name="startSquare">The square the piece is on</param>
        /// <param name="targetSquare">The square the piece goes to</param>
        /// <param name="pieceType">The type of the piece</param>
        /// <param name="pieceColor">The pieceColor of the piece</param>
        public Move(Square startSquare, Square targetSquare, Type pieceType, Color pieceColor)
        {
            Board = startSquare.Board;
            PieceColor = pieceColor;
            PieceType = pieceType;
            StartCoordinate = startSquare.Coordinate;
            TargetCoordinate = targetSquare.Coordinate;
        }

        /// <summary>
        /// Promoting move constructor
        /// </summary>
        /// <param name="startSquare">The square the piece is on</param>
        /// <param name="targetSquare">The square the piece goes to</param>
        /// <param name="pieceType">The type of the piece</param>
        /// <param name="pieceColor">The color of the piece</param>
        /// <param name="promotePieceType">The type the piece promotes to</param>
        public Move(Square startSquare, Square targetSquare, Type pieceType, Color pieceColor, Type promotePieceType)
        {
            Board = startSquare.Board;
            PieceColor = pieceColor;
            PieceType = pieceType;
            StartCoordinate = startSquare.Coordinate;
            TargetCoordinate = targetSquare.Coordinate;
            PromotePieceType = promotePieceType;
        }
        #endregion
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