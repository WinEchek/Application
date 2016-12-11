using System.Collections.Generic;
using WinEchek.Engine.Command;
using WinEchek.Engine.RuleManager;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine
{
    public class RealEngine : Engine
    {
        private CompensableConversation _conversation;
        private RuleGroup _ruleGroups;

        /// <summary>
        /// The board the engine works with
        /// </summary>
        public override Board Board { get; }

        /// <summary>
        /// RealEngine constructor
        /// </summary>
        /// <param name="container">The model the engine will work with</param>
        public RealEngine(Container container)
        {
            Board = container.Board;
            _conversation = new CompensableConversation(container.Moves);

            _ruleGroups = new PawnRuleGroup();
            _ruleGroups.AddGroup(new BishopRuleGroup());
            _ruleGroups.AddGroup(new KingRuleGroup());
            _ruleGroups.AddGroup(new KnightRuleGroup());
            _ruleGroups.AddGroup(new QueenRuleGroup());
            _ruleGroups.AddGroup(new RookRuleGroup());
        }

        /// <summary>
        /// Ask the engine to do a move
        /// </summary>
        /// <param name="move">The move to do</param>
        /// <returns>True if the move was valid and therefore has been done</returns>
        public override bool DoMove(Move move)
        {
            //No reason to move if it's the same square
            if (move.TargetSquare == move.Piece.Square) return false;

            Square startSquare = move.Piece.Square;

            //TODO gérer exception
            if (_ruleGroups.Handle(move))
            {
                _conversation.Execute(new MoveCommand(move));
                return true;
            }

            return false;
        }

        public override List<Square> PossibleMoves(Piece piece)
        {
            return _ruleGroups.PossibleMoves(piece);
        }

        /// <summary>
        /// Undo the last command that has been done
        /// </summary>
        /// <returns>True if anything has been done</returns>
        public override bool Undo() => _conversation.Undo() != null;

        /// <summary>
        /// Redo the last command that has been undone
        /// </summary>
        /// <returns>True if anything has been done</returns>
        public override bool Redo() => _conversation.Redo() != null;

    }
}