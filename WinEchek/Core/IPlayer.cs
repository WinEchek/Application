using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek
{
    public interface IPlayer
    {
        void DoMove(Piece piece, Square square);
    }
}
