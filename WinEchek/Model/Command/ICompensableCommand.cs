namespace WinEchek.Model.Command
{
    /// <summary>
    /// Defines a command that can be undone
    /// </summary>
    public interface ICompensableCommand : ICommand
    {
        void Compensate();
    }
}