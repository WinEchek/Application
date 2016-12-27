using WinEchek.Model;

namespace WinEchek.IO
{
    public interface ILoader
    {
        Container Load(string path);

        string Filter();
    }
}