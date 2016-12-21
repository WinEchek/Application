using System;
using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Rules
{
    public class Castling : IRule
    {
        public bool IsMoveValid(Move move, Board board)
        {
            if (board.PieceAt(move.TargetCoordinate)?.Color != move.PieceColor) return true;

            List<Square> list = move.TargetCoordinate.X > move.StartCoordinate.X ?
                board.Squares.OfType<Square>().ToList().FindAll(x => (x.Y == move.StartCoordinate.Y && x.X < 7 && x.X > move.StartCoordinate.X)) :
                board.Squares.OfType<Square>().ToList().FindAll(x => (x.Y == move.StartCoordinate.Y && x.X > 0 && x.X < move.StartCoordinate.X));

            return list.All(x => x.Piece == null) && board.PieceAt(move.TargetCoordinate).Type == Type.Rook && !board.PieceAt(move.StartCoordinate).HasMoved && !board.PieceAt(move.TargetCoordinate).HasMoved;
        }

        public List<Square> PossibleMoves(Piece piece)
        {
            return piece.Square.Board.Squares.OfType<Square>()
                .ToList()
                .FindAll(x => IsMoveValid(new Move(piece, x), piece.Square.Board));
        }
    }
}