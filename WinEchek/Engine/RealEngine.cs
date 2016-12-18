using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WinEchek.Engine.Command;
using WinEchek.Engine.RuleManager;
using WinEchek.Engine.States;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine
{
    public class RealEngine : Engine
    {
        private CompensableConversation _conversation;
        private RuleGroup _ruleGroups;
        private ObservableCollection<ICompensableCommand> _moves;
        private Pawn _enPassantPawnBlack = null;
        private Pawn _enPassantPawnWhite = null;

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
        public override bool DoMove(Move move)
        {
            //No reason to move if it's the same square
            if (move.TargetSquare == move.Piece.Square) return false;

            //TODO gérer exception
            if (_ruleGroups.Handle(move))
            {
                ICompensableCommand command;
                if (move.Piece.Type == Type.King && move.TargetSquare?.Piece?.Type == Type.Rook)
                    command = new CastlingCommand(move);
                else if (move.Piece.Type == Type.Pawn && move.TargetSquare.Piece == null && move.StartSquare.X != move.TargetSquare.X)
                    command = new EnPassantCommand(move);
                else
                    command = new MoveCommand(move);

                //En passant
                if (move.Piece.Color == Color.White)
                {
                    if (_enPassantPawnWhite != null)
                    {
                        _enPassantPawnWhite.EnPassant = false;
                        _enPassantPawnWhite = null;
                    }
                }
                else
                {
                    if (_enPassantPawnBlack != null)
                    {
                        _enPassantPawnBlack.EnPassant = false;
                        _enPassantPawnBlack = null;
                    }
                }
                if (move.Piece.Type == Type.Pawn && Math.Abs(move.StartSquare.Y - move.TargetSquare.Y) == 2)
                {
                    if (move.Piece.Color == Color.White)
                    {
                        _enPassantPawnWhite = (Pawn) move.Piece;
                        _enPassantPawnWhite.EnPassant = true;
                    }
                    else
                    {
                        _enPassantPawnBlack = (Pawn) move.Piece;
                        _enPassantPawnBlack.EnPassant = true;
                    }
                }
                //En passant

                _conversation.Execute(command);
                _moves.Add(command);

                return true;
            }

            return false;
        }

        public override BoardState CurrentState()
        {
            IState checkState = new CheckState();
            IState patState = new PatState();


            Color color = _moves.Count == 0 ? Color.White : _moves[_moves.Count - 1].PieceColor;

            bool check = checkState.IsInState(Board, color == Color.White ? Color.Black : Color.White);
            
            bool pat = patState.IsInState(Board, color == Color.White ? Color.Black : Color.White);

            if (pat && check)
                return color == Color.Black ? BoardState.WhiteCheckMate : BoardState.BlackCheckMate;
            if(pat)
                return color == Color.Black ? BoardState.WhitePat : BoardState.BlackPat;
            if(check)
                return color == Color.Black ? BoardState.WhiteCheck : BoardState.BlackCheck;

            return BoardState.Normal;
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