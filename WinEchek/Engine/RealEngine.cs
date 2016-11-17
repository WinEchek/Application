using System;
using System.CodeDom;
using System.Windows.Controls;
using WinEchek.Command;
using WinEchek.Engine.Rules;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine
{
    public class RealEngine : Engine
    {
        public override Board Board { get; }
        private CompensableConversation _conversation = new CompensableConversation();
        private PieceRule _rules;

        public RealEngine(Board board)
        {
            Board = board;
            _rules = new PawnRule();
            _rules.Add(new BishopRule());
            _rules.Add(new KingRule());
            _rules.Add(new KnightRule());
            _rules.Add(new QueenRule());
            _rules.Add(new RookRule());
        }

        public override bool DoMove(Piece piece, Square square)
        {
            Square startSquare = piece.Square;
            //TODO gérer exception
            if (_rules.Handle(piece, square))
            {
                _conversation.Execute(new Move(piece, square));
                MoveDone?.Invoke(this, new MoveEventArgs(piece, startSquare, square));
                return true;
            }

            return false;
        }

        public override void Undo()
        {
            _conversation.Undo();
        }

        public override void Redo()
        {
            Move move = _conversation.Redo() as Move;
            if(move!=null)
                MoveDone?.Invoke(this, new MoveEventArgs(move.Piece, move.Square, move.Piece.Square));
        }

        public override event MoveHandler MoveDone;
    }
}