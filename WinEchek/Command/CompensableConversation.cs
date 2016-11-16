using System.Collections.Generic;

namespace WinEchek.Command
{
    public class CompensableConversation
    {
        private Stack<ICompensableCommand> _undoCommands = new Stack<ICompensableCommand>();
        private Stack<ICompensableCommand> _redoCommands = new Stack<ICompensableCommand>();

        public void Execute(ICompensableCommand command)
        {
            command.Execute();
            _undoCommands.Push(command);
            _redoCommands.Clear();
        }

        public void Undo()
        {
            if (_undoCommands.Count == 0) return;
            ICompensableCommand command = _undoCommands.Pop();
            command.Compensate();
            _redoCommands.Push(command);
        }

        public void Redo()
        {
            if (_redoCommands.Count == 0) return;
            ICompensableCommand command = _redoCommands.Pop();
            command.Execute();
            _undoCommands.Push(command);
        }
    }
}