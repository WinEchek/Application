using System.Collections.Generic;
using System.Collections.ObjectModel;
using WinEchek.Engine.Command;
using WinEchek.Engine.RuleManager;
using WinEchek.Engine.States;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine
{
    public class RealEngine : Engine
    {
        private CompensableConversation _conversation;
        private RuleGroup _ruleGroups;
        private ObservableCollection<ICompensableCommand> _moves;

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
            _moves = container.Moves;

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
        public override BoardState DoMove(Move move)
        {
            //No reason to move if it's the same square
            if (move.TargetSquare == move.Piece.Square) return BoardState.Invalid;

            Square startSquare = move.Piece.Square;

            //TODO gérer exception
            if (_ruleGroups.Handle(move))
            {
                ICompensableCommand command;
                if (move.Piece.Type == Type.King && move.TargetSquare?.Piece?.Type == Type.Rook)
                    command = new CastlingCommand(move);
                else
                    command = new MoveCommand(move);
                _conversation.Execute(command);
                _moves.Add(command);

                
                IState checkState = new CheckState();

                if (checkState.IsInState(move.Piece.Square.Board, (move.Piece.Color == Color.Black ? Color.White : Color.Black)))
                {
                    return move.Piece.Color == Color.Black ? BoardState.WhiteCheck : BoardState.BlackCheck;
                }
                
                return BoardState.Valid;
            }

            return BoardState.Invalid;
        }

        public override List<Square> PossibleMoves(Piece piece)
        {
            return _ruleGroups.PossibleMoves(piece);
        }

        /// <summary>
        /// Undo the last command that has been done
        /// </summary>
        /// <returns>True if anything has been done</returns>
        public override bool Undo()
        {
            ICompensableCommand command = _conversation.Undo();
            if (command == null) return false;

            _moves.Remove(command);
            return true;
        }

        /// <summary>
        /// Redo the last command that has been undone
        /// </summary>
        /// <returns>True if anything has been done</returns>
        public override bool Redo()
        {
            ICompensableCommand command = _conversation.Redo();
            if (command == null) return false;

            _moves.Add(command);
            return true;
        }
    }
}