using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinEchek.Persistance
{
    interface ISaver
    {
        void Save(Game game, string path);
        String Filter();
    }
}
