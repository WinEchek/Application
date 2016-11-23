using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Rules
{
    public class PawnMovementRule : IRule

    {
        //TODO le pion peut se déplacer de deux au premier déplacements
        public bool IsMoveValid(Move move)
        {
            bool res = false;
            Square square = move.Square;
            Piece piece = move.Piece;
            bool white = piece.Color == Color.White;

            if (square.Piece == null) // Si la case d'arriver est vide
            {
                if (piece.Square.X == square.X) // on ne peut pas aller sur les côtés
                {
                    if (piece.Square.Y - square.Y == (white ? 1 : -1)) // on ne se déplace que vers le haut
                        res = true;
                }
            }
            else // si la case d'arriver n'est pas vide
            {
                if (piece.Square.X == square.X - 1 || piece.Square.X == square.X + 1) // on se déplace sur les côté.
                {
                    if (piece.Square.Y - square.Y == (white ? 1 : -1)) // on se déplace que vers le haut
                        res = true;
                }
            }

            return res;
        }
    }
}