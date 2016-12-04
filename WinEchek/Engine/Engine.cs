using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine
{
    public abstract class Engine
    {
        public abstract Board Board { get; }
        public abstract bool DoMove(Move move);
        /// <summary>
        /// Implémentation à revoir : la méthode devra renvoyer une liste de case.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public abstract bool PossibleMove(Move move);
        public abstract void Undo();
        public abstract void Redo();

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
