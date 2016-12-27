using WinEchek.Model;

namespace WinEchek.IO
{
    public interface ISaver
    {
        void Save(Container container, string path);
        string Filter();
    }
}