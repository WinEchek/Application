using System.Collections.Generic;
using WinEchek.Engine.Command;
using WinEchek.Engine.RuleManager;
using WinEchek.Model;
using WinEchek.Model.Piece;

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

        public override List<Square> PossibleMoves(Piece piece)
        {
            return _ruleGroups.PossibleMoves(piece);
        }

        //TODO gérer les exceptions
        public override bool Undo() => (_conversation.Undo() != null);
        

        public override bool Redo()
        {
            //TODO remove the cast
            MoveCommand moveCommand = _conversation.Redo() as MoveCommand;
            if (moveCommand == null) return false;

            MoveDone?.Invoke(this, new MoveEventArgs(moveCommand.Piece, moveCommand.Square, moveCommand.Piece.Square));

            return true;
        }

        public override event MoveHandler MoveDone;
    }
}