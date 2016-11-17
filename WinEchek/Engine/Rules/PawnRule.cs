using System;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Rules
{
    public class PawnRule : PieceRule
    {
        //TODO gérer les couleurs de pièces.
        public override bool Handle(Piece piece, Square square)
        {
            if (piece.Type != Type.Pawn)
            {
                if (Next != null)
                {
                    return Next.Handle(piece, square);
                }
                throw new Exception("NOBODY TREATS THIS PIECE !!! " + piece);
            }
            bool res = false;
            if (piece.Square.X == square.X)
            {
                if (piece.Square.Y - square.Y == 1)
                    res = true;
            }
            return res;
        }
    }
}