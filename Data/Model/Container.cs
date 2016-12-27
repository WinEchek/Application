using System;
using System.Collections.ObjectModel;
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
        }

        /// <summary>
        ///     The game board
        /// </summary>
        public Board Board { get; }

        /// <summary>
        ///     The list of moves that has been done
        /// </summary>
        public ObservableCollection<ICompensableCommand> Moves { get; }

        //Increment at each 
        public int HalfMoveSinceLastCapture { get; set; } = 0;
    }
}