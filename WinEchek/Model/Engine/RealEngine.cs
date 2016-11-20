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
        private RuleGroup _ruleGroups;

        public RealEngine(Board board)
        {
            Board = board;
            _ruleGroups = new PawnRuleGroup();
            _ruleGroups.Add(new BishopRuleGroup());
            _ruleGroups.Add(new KingRuleGroup());
            _ruleGroups.Add(new KnightRuleGroup());
            _ruleGroups.Add(new QueenRuleGroup());
            _ruleGroups.Add(new RookRuleGroup());
        }

        public override bool DoMove(Piece piece, Square square)
        {
            Square startSquare = piece.Square;
            //TODO gérer exception
            if (_ruleGroups.Handle(piece, square))
            {
                _conversation.Execute(new MoveCommand(piece, square));
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
            MoveCommand moveCommand = _conversation.Redo() as MoveCommand;
            if(moveCommand!=null)
                MoveDone?.Invoke(this, new MoveEventArgs(moveCommand.Piece, moveCommand.Square, moveCommand.Piece.Square));
        }

        public override event MoveHandler MoveDone;
    }
}