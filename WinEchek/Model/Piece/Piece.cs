﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using WinEchek.Annotations;

using System;

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

        public abstract Type Type { get; }
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
