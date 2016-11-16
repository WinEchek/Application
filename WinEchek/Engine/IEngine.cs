using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine
{
    public interface IEngine
    {
        Board Board { get; }
        bool DoMove(Piece piece, Square square);
        void Undo();
        void Redo();
    }
}
