using System;
using WinEchek.Model;

namespace WinEchek.Core.Persistance
{
    interface ISaver
    {
        void Save(Container container, string path);
        String Filter();
    }
}
