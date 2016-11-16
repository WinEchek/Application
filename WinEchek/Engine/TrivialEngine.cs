using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine
{
    [Serializable]
    public class TrivialEngine : IEngine
    {
        public Board Board { get; set; }

        public TrivialEngine(Board board)
        {
            Board = board;
        }

        public bool DoMove(Piece piece, Square square)
        {
            if (square.Piece==null)//Si case vide
            {
                piece.Square.Piece = null;
                piece.Square = square;
                square.Piece = piece;
            }
            else
            {
                square.Piece = null;
                piece.Square.Piece = null;
                piece.Square = square;
                square.Piece = piece;
            }
            return true;
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }
    }
}
