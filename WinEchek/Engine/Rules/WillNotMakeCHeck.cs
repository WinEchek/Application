using System;
using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Rules {
    public class WillNotMakeCheck : IRule
    {
        public bool IsMoveValid(Move move)
        {
            throw new NotImplementedException();
        }

        public List<Square> PossibleMoves(Piece piece)
        {
            throw new System.NotImplementedException();
        }
    }
}