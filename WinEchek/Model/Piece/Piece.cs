using System.ComponentModel;
using System.Runtime.CompilerServices;
using WinEchek.Annotations;

using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    public abstract class Piece {
        public Color Color { get; }
        public Square Square { get; set; }

        public Piece(Color color, Square square)
        {
            Color = color;
            Square = square;
        }

        public abstract Type Type ();
    }
    [Serializable]
    public enum Type {
        Bishop,
        King,
        Queen,
        Pawn,
        Knight,
        Rook
    }
    [Serializable]
    public enum Color
    {
        White,
        Black
    }

}
