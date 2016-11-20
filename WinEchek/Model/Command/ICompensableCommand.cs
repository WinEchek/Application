namespace WinEchek.Command
{
    public interface ICompensableCommand
    {
        void Execute();
        void Compensate();
    }
}