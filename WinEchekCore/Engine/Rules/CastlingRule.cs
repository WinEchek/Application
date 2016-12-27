using System;
using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Pieces;
using Type = WinEchek.Model.Pieces.Type;

namespace WinEchek.Engine.Rules
{
    public class CastlingRule : IRule
    {
        public bool IsMoveValid(Move move, Board board)
        {
            Square targetSquare = board.SquareAt(move.TargetCoordinate);
            Piece piece = board.PieceAt(move.StartCoordinate);

            if ((Math.Abs(move.TargetCoordinate.X - move.StartCoordinate.X) == 2) &&
                (move.TargetCoordinate.Y == move.StartCoordinate.Y))
            {
                if (!NoPiecesBetween(move, board))
                    return false;

                //Vers la droite ou la gauche ?
                Piece possibleRook =
                    board.PieceAt(new Coordinate(move.TargetCoordinate.X > move.StartCoordinate.X ? 7 : 0,
                        move.StartCoordinate.Y));
                return !piece.HasMoved && (!possibleRook?.HasMoved == true) && (possibleRook?.Type == Type.Rook);
            }
            if ((targetSquare?.Piece?.Type == Type.Rook) && (targetSquare?.Piece?.Color == piece.Color))
            {
                if (!NoPiecesBetween(move, board))
                    return false;

                return !piece.HasMoved && !targetSquare.Piece.HasMoved;
            }

            return true;
        }

        public List<Square> PossibleMoves(Piece piece)
        {
            return piece.Square.Board.Squares.OfType<Square>()
                .ToList()
                .FindAll(x => IsMoveValid(new Move(piece, x), piece.Square.Board));
        }

        private static bool NoPiecesBetween(Move move, Board board) => (move.TargetCoordinate.X > move.StartCoordinate.X
                ? board.Squares.OfType<Square>()
                    .ToList()
                    .FindAll(x => (x.Y == move.StartCoordinate.Y) && (x.X < 7) && (x.X > move.StartCoordinate.X))
                : board.Squares.OfType<Square>()
                    .ToList()
                    .FindAll(x => (x.Y == move.StartCoordinate.Y) && (x.X > 0) && (x.X < move.StartCoordinate.X))
        ).All(x => x.Piece == null);
    }
}