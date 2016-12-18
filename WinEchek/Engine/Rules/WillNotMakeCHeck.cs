using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WinEchek.Engine.Command;
using WinEchek.Engine.States;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Rules {
    public class WillNotMakeCheck : IRule
    {
        public bool IsMoveValid(Move move)
        {
            IState checkState = new CheckState();
            Board tempBoard = new Board(move.Piece.Square.Board);
            bool castling = new Castling().IsMoveValid(move) && (move.Piece.Type == Type.King && move.TargetSquare?.Piece?.Type == Type.Rook && move.PieceColor == move.TargetSquare?.Piece?.Color) ;

            if (!castling)
            {
                if (move.Piece.Color == move.TargetSquare?.Piece?.Color)
                {
                    return true;
                }
            }
            ICompensableCommand command = castling
                ? new CastlingCommand(move).Copy(tempBoard)
                : new MoveCommand(move).Copy(tempBoard);

            command.Execute();

            return !checkState.IsInState(tempBoard, move.Piece.Color);

        }

        public List<Square> PossibleMoves(Piece piece)
        {
            return piece.Square.Board.Squares.OfType<Square>().ToList().FindAll(x => IsMoveValid(new Move(piece, x)));
        }
    }
}