using WinEchek.Model;
using WinEchek.Model.Pieces;

namespace WinEchek.Command
{
    /// <summary>
    ///     A command that can be done and undone
    /// </summary>
    public interface ICompensableCommand
    {
        /// <summary>
        ///     TakePiece
        /// </summary>
        /// <value>
        ///     True if this command take an ennemy piece
        /// </value>
        bool TakePiece { get; }

        /// <summary>
        ///     Move
        /// </summary>
        /// <value>
        ///     Move corresponding to the command
        /// </value>
        Move Move { get; }

        /// <summary>
        ///     PieceType
        /// </summary>
        /// <value>
        ///     The type of the piece that is concerned by the move
        /// </value>
        Type PieceType { get; }

        /// <summary>
        ///     PieceColor
        /// </summary>
        /// <value>
        ///     The color of the piece that is concerned by the move
        /// </value>
        Color PieceColor { get; }

        /// <summary>
        ///     Execute the command
        /// </summary>
        void Execute();

        /// <summary>
        ///     Undo the command
        /// </summary>
        void Compensate();

        /// <summary>
        ///     Copy constructor to change the acting model
        /// </summary>
        /// <param name="board">The new board to apply the command on</param>
        /// <returns>The new command that apply on the given board</returns>
        ICompensableCommand Copy(Board board);

        string ToString();
    }
}