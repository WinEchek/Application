namespace WinEchek.Engine.Command
{
    public interface ICompensableConversation
    {
        void Execute(ICompensableCommand command);
        ICompensableCommand Redo();
        ICompensableCommand Undo();
    }
}