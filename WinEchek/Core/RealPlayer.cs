using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinEchek.Core;
using WinEchek.Engine;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek
{
    public class RealPlayer : Player
    {
        public RealPlayer(Color color) : base(color) {}

        public override void Play()
        {
            //Dire au médiateur joueur / IHM que c'est la vie
        }

        public override void Move(Move move) => MoveDone?.Invoke(this, move);

        public override event MoveHandler MoveDone;
    }
}
