using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Command
{
    /// <summary>
    /// A command that can be done and undone
    /// </summary>
    public interface ICompensableCommand
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        void Execute();

        /// <summary>
        /// Undo the command
        /// </summary>
        void Compensate();

        /// <summary>
        /// PieceType
        /// </summary>
        /// <value>
        /// The type of the piece that is concerned by the move
        /// </value>
        Type PieceType { get; }

        /// <summary>
        /// PieceColor
        /// </summary>
        /// <value>
        /// The color of the piece that is concerned by the move
        /// </value>
        Color PieceColor { get; }

        /// <summary>
        /// Copy constructor to change the acting model
        /// </summary>
        /// <param name="board">The new board to apply the command on</param>
        /// <returns>The new command that apply on the given board</returns>
        ICompensableCommand Copy(Board board);

        string ToString();
    }
}