using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using WinEchek.Command;

namespace WinEchek.Model
{
    /// <summary>
    ///     Contains the model for a chess game
    /// </summary>
    [Serializable]
    public class Container
    {
        /// <summary>
        ///     Container constructor
        /// </summary>
        /// <remarks>
        ///     Use to create a new game
        /// </remarks>
        public Container()
        {
            Board = new Board();
            Moves = new ObservableCollection<ICompensableCommand>();
            Moves.CollectionChanged += (sender, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        OnMoveDone(Moves.Last().Move);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        if(Moves.Count != 0)
                        OnMoveUndone(Moves.Last().Move);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };
        }

        public Container(Board board, ObservableCollection<ICompensableCommand> moves)
        {
            Board = board;
            Moves = moves;
            Moves.CollectionChanged += (sender, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        OnMoveDone(Moves.Last().Move);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        if (Moves.Count != 0)
                            OnMoveUndone(Moves.Last().Move);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };
        }
        /// <summary>
        ///     The game board
        /// </summary>
        public Board Board { get; }

        /// <summary>
        ///     The list of moves that has been done
        /// </summary>
        public ObservableCollection<ICompensableCommand> Moves { get; }

        public int HalfMoveSinceLastCapture { get; set; } = 0;

        
        public delegate void MoveHandler(Move move);

        protected void OnMoveDone(Move move) => MoveDone?.Invoke(move);
        public event MoveHandler MoveDone;

        protected void OnMoveUndone(Move move) => MoveUndone?.Invoke(move);
        public event MoveHandler MoveUndone;
    }
}