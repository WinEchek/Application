using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine
{
    public class YoloMoteur : IEngine
    {
        public Board Board { get; set; }

        public YoloMoteur(Board board)
        {
            Board = board;
        }

        public bool DoMove(Piece piece, Square square)
        {
            if (square.Piece==null)//Si case vide
            {
                piece.Square.Piece = null;
                piece.Square = square;
            }
            else
            {
                square.Piece = null;
                piece.Square.Piece = null;
                piece.Square = square;
            }
            return true;
        }
    }
}
