using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    public class Piece
    {
        public Type Type { get; }
        public Color Color { get; }
        public Square Square { get; set; }

        public Piece(Type type, Color color, Square square)
        {
            this.Type = type;
            Color = color;
            Square = square;
        }

    }
    public enum Type {
        Bishop,
        King,
        Queen,
        Pawn,
        Knight,
        Rook
    }

    public enum Color
    {
        White,
        Black
    }

}
