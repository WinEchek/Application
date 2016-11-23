using System;

namespace WinEchek.Core.Persistance
{
    interface ISaver
    {
        void Save(Game game, string path);
        String Filter();
    }
}
