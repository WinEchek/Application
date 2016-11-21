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
            _ruleGroups.AddGroup(new BishopRuleGroup());
            _ruleGroups.AddGroup(new KingRuleGroup());
            _ruleGroups.AddGroup(new KnightRuleGroup());
            _ruleGroups.AddGroup(new QueenRuleGroup());
            _ruleGroups.AddGroup(new RookRuleGroup());
        }

        public override bool DoMove(Move move)
        {
            //No reason to move if it's the same square
            if (move.Square == move.Piece.Square) return false;

            Square startSquare = move.Piece.Square;
            //TODO gérer exception
            if (_ruleGroups.Handle(move))
            {
                _conversation.Execute(new MoveCommand(move));
                MoveDone?.Invoke(this, new MoveEventArgs(move.Piece, startSquare, move.Square));
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