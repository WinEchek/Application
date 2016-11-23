namespace WinEchek.Engine.Command
{
    public interface ICompensableCommand
    {
        void Execute();
        void Compensate();
    }
}