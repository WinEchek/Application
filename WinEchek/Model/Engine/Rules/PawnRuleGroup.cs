using System;
using WinEchek.GUI.Core.FlyoutContent;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Rules
{
    public class PawnRuleGroup : RuleGroup
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

            if (square.Piece == null) // Si la case d'arriver est vide
            {
                if (piece.Square.X == square.X) // on ne peut pas aller sur les côtés
                {
                    if (piece.Square.Y - square.Y == 1) // on ne se déplace que vers le haut
                        res = true;
                }
            }
            else // si la case d'arriver n'est pas vide
            {
                if (square.Piece.Color == piece.Color)
                {
                    return false;
                }
                if (piece.Square.X == square.X - 1 || piece.Square.X == square.X + 1) // on se déplace sur les côté.
                {
                    if (piece.Square.Y - square.Y == 1) // on se déplace que vers le haut
                        res = true;
                }
            }

            return res;
        }
    }
}