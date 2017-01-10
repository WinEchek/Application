using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Pieces;

namespace WinEchek.Engine.Rules
{
    public class RookMovementRule : IRule
    {
        public bool IsMoveValid(Move move, Board board)
        {
            Square targetSquare = board.SquareAt(move.TargetCoordinate);
            Piece piece = board.PieceAt(move.StartCoordinate);
            //if the movement is not inline
            if (!((piece.Square.X == targetSquare.X) ^ (piece.Square.Y == targetSquare.Y))) return false;

            return board.Squares.OfType<Square>()
                .Where(x => piece.Square.Y == targetSquare.Y
                    ? Between(piece.Square.X, targetSquare.X, x.X) && (x.Y == targetSquare.Y)
                    : //Horizontal movement
                    Between(piece.Square.Y, targetSquare.Y, x.Y) && (x.X == targetSquare.X)) //Vertical movement
                //All squares in between startsquare and targetsquare are empty
                .All(betweenSquare => betweenSquare.Piece == null);
        }

        public List<Square> PossibleMoves(Piece piece)
        {
            return
                piece.Square.Board.Squares.OfType<Square>()
                    .ToList()
                    .FindAll(x => IsMoveValid(new Move(piece, x), piece.Square.Board));
        }

        private static bool Between(int i, int j, int x) => i > j
            ? (i > x) && (j < x)
            : (j > x) && (i < x);
    }
}