using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine
{
    class YoloMoteur : IEngine
    {
        public YoloMoteur(Board _board)
        {
            Board board = _board;
        }

        public bool DoMove(Piece piece, Square square)
        {
            if (0 != 0)//Si case vide
            {
                //déplacement pièce
            }
            else//Sinon
            {
                //On supprime la pièce dans la case d'arrivée
                //déplacement de la pièce
            }

            return true;
        }
    }
}
