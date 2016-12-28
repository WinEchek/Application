using System;
using System.Runtime.Serialization;

namespace WinEchek.Model.Piece
{
    [Serializable]
    public abstract class Piece {
        public Color Color { get; }
        public Square Square { get; set; }

        /// <summary>
        /// HasMoved
        /// </summary>
        /// <value>
        /// Return true if the piece has already moved
        /// </value>
        public bool HasMoved { get; set; } = false;

        protected Piece(Color color, Square square)
        {
            Color = color;
            Square = square;
        }

        protected Piece(Color color)
        {
            Color = color;
            Square = null;
        }


        public abstract Piece Clone(Square square);
        public Type Type { get; set; }
    }
    [Serializable]
    [DataContract]
    public enum Type {
        [EnumMember]
        Bishop,
        [EnumMember]
        King,
        [EnumMember]
        Queen,
        [EnumMember]
        Pawn,
        [EnumMember]
        Knight,
        [EnumMember]
        Rook
    }
    [Serializable]
    [DataContract]
    public enum Color
    {
        [EnumMember]
        White,
        [EnumMember]
        Black
    }

}
