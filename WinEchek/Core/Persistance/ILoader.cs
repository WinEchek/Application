using WinEchek.Model;

namespace WinEchek.Core.Persistance
{
    interface ILoader
    {
        Container Load(string path);

        string Filter();
    }
}
