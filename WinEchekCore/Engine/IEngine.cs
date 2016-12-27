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

        /// <summary>
        ///     Implémentation à revoir : la méthode devra renvoyer une liste de case.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        List<Square> PossibleMoves(Piece piece);

        /// <summary>
        ///     Undo the last move
        /// </summary>
        /// <returns>True if anything was done</returns>
        bool Undo();

        /// <summary>
        ///     Redo the last move that has been undone
        /// </summary>
        /// <returns>True if anything was done</returns>
        bool Redo();

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