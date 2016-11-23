namespace WinEchek.Core.Persistance
{
    interface ILoader
    {
        Game Load(string path);
        string Filter();
    }
}
