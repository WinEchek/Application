using System;
using System.Collections.ObjectModel;
using WinEchek.Engine.Command;

namespace WinEchek.Model
{
    /// <summary>
    /// Contains the model for a chess game
    /// </summary>
    [Serializable]
    public class Container
    {
        /// <summary>
        /// The game board
        /// </summary>
        public Board Board { get; }

        /// <summary>
        /// The list of moves that has been done
        /// </summary>
        public ObservableCollection<ICompensableCommand> Moves { get; }

        /// <summary>
        /// Container constructor
        /// </summary>
        /// <remarks>
        /// Use to create a new game
        /// </remarks>
        public Container()
        {
            Board = new Board();
            Moves = new ObservableCollection<ICompensableCommand>();
        }

        /// <summary>
        /// Container constructor
        /// </summary>
        /// <remarks>
        /// Use to load a game
        /// </remarks>
        /// <param name="board">The game board</param>
        /// <param name="moves">The list of moves that has been done</param>
        public Container(Board board, ObservableCollection<ICompensableCommand> moves)
        {
            Board = board;
            Moves = moves;
        }
    }
}