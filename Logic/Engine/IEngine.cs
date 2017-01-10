using System.Collections.Generic;
using WinEchek.Model;
using WinEchek.Model.Pieces;

namespace WinEchek.Engine
{
    /// <summary>
    ///     Represents all the constraints on the chess model
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        ///     Ask the engine to do a move
        /// </summary>
        /// <param name="move">The move to do</param>
        /// <returns>True if the move was valid and therefore has been done</returns>
        bool DoMove(Move move);


        List<Square> PossibleMoves(Piece piece);

        /// <summary>
        ///     Undo the last move
        /// </summary>
        /// <returns>True if anything was done</returns>
        Move Undo();

        /// <summary>
        ///     Redo the last move that has been undone
        /// </summary>
        /// <returns>True if anything was done</returns>
        Move Redo();

        BoardState CurrentState();
    }

    public enum BoardState
    {
        Normal,
        WhiteCheck,
        BlackCheck,
        BlackCheckMate,
        WhiteCheckMate,
        BlackPat,
        WhitePat
    }
}