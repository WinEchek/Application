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
        public bool IsMoveValid(Move move)
        {
            if (move.TargetSquare.Piece?.Color != move.Piece.Color) return true;
            Board board = move.TargetSquare.Board;

            List<Square> list = move.TargetSquare.X > move.StartSquare.X ?
                board.Squares.OfType<Square>().ToList().FindAll(x => (x.Y == move.StartSquare.Y && x.X < 7 && x.X > move.StartSquare.X)) :
                board.Squares.OfType<Square>().ToList().FindAll(x => (x.Y == move.StartSquare.Y && x.X > 0 && x.X < move.StartSquare.X));

            return list.All(x => x.Piece == null) && move.TargetSquare.Piece.Type == Type.Rook && !move.Piece.HasMoved && !move.TargetSquare.Piece.HasMoved;
        }

        public List<Square> PossibleMoves(Piece piece)
        {
            return piece.Square.Board.Squares.OfType<Square>()
                .ToList()
                .FindAll(x => IsMoveValid(new Move(piece, x)));
        }
    }
}