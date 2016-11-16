using WinEchek.Model.Command;

namespace WinEchek.Engine
{
    public interface IConversation <in TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);
        void Undo();
        void Redo();
    }
}