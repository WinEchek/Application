using WinEchek.Model;

namespace WinEchek.Engine
{
    /// <summary>
    /// Represents all the constraints on the chess model
    /// </summary>
    public abstract class Engine
    {
        /// <summary>
        /// The board the engine works with
        /// </summary>
        public abstract Board Board { get; }

        /// <summary>
        /// Ask the engine to do a move
        /// </summary>
        /// <param name="move">The move to do</param>
        /// <returns>True if the move was valid and therefore has been done</returns>
        public abstract bool DoMove(Move move);

        /// <summary>
        /// Implémentation à revoir : la méthode devra renvoyer une liste de case.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public abstract bool PossibleMove(Move move);

        /// <summary>
        /// Undo the last move that has been done
        /// </summary>
        /// <returns>True if anything has been done</returns>
        public abstract bool Undo();

        /// <summary>
        /// Redo the last move that has been undone
        /// </summary>
        /// <returns>True if anything was done</returns>
        public abstract bool Redo();
    }
}
