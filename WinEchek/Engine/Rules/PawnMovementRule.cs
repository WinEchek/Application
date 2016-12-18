using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Rules
{
    public class PawnMovementRule : IRule
    {
        public bool IsMoveValid(Move move)
        {
            Square targetSquare = move.TargetSquare;
            Piece piece = move.Piece;
            Square square = piece.Square;
            Board board = targetSquare.Board;
            bool isWhite = piece.Color == Color.White;
            bool isStartPosition = piece.Square.Y == 1 && !isWhite || piece.Square.Y == 6 && isWhite;

            if (targetSquare.Piece == null)
            {
                bool normalMove = 
                    //Déplacement d'une case en avant
                    (piece.Square.Y - targetSquare.Y == (isWhite ? 1 : -1) ||
                    //Premier déplacement de deux cases
                    (isStartPosition && piece.Square.Y - targetSquare.Y == (isWhite ? 2 : -2))) &&
                    //Sur la même colonne
                    piece.Square.X == targetSquare.X;

                Pawn leftPiece =
                    (square.X > 0)
                        ? board.Squares[square.X - 1, square.Y]?.Piece as Pawn
                        : null;
                Pawn rightPiece =
                    (square.X < 7)
                        ? board.Squares[square.X + 1, square.Y]?.Piece as Pawn
                        : null;

                if (leftPiece?.EnPassant == true)
                {
                    if(targetSquare.X == square.X-1 && (piece.Square.Y - targetSquare.Y == (isWhite ? 1 : -1)))
                    return true;
                }
                if (rightPiece?.EnPassant == true)
                {
                    if (targetSquare.X == square.X + 1 && (piece.Square.Y - targetSquare.Y == (isWhite ? 1 : -1)))
                    return true;
                }
                
                return normalMove;
            }
            else
            {
                return //Seulement les deux cases diagonal
                (piece.Square.X == targetSquare.X - 1 || piece.Square.X == targetSquare.X + 1) &&
                //D'une case en avant
                piece.Square.Y - targetSquare.Y == (isWhite ? 1 : -1);
            }
            
        }

        public List<Square> PossibleMoves(Piece piece)
        {
            return piece.Square.Board.Squares.OfType<Square>().ToList().FindAll(x => IsMoveValid(new Move(piece, x)));
        }
    }
}