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
        /// <param name="piece"></param>
        /// <returns></returns>
        public abstract List<Square> PossibleMoves(Piece piece);
        public abstract bool Undo();

        /// <summary>
        /// Redo the last move that has been undone
        /// </summary>
        /// <returns>True if anything was done</returns>
        public abstract bool Redo();

        public delegate void MoveHandler(object sender, MoveEventArgs eventArgs);

        public abstract event MoveHandler MoveDone;
    }

    public class MoveEventArgs : EventArgs
    {
        public Piece Piece { get; internal set; }
        public Square StartSquare { get; internal set; }
        public Square TargetSquare { get; internal set; }

        public MoveEventArgs(Piece piece, Square startSquare, Square targetSquare)
        {
            Piece = piece;
            StartSquare = startSquare;
            TargetSquare = targetSquare;
        }
    }
}
