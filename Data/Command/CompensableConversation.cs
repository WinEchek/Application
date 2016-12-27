using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WinEchek.Command
{
    /// <summary>
    ///     Conversation keeping track of commands,
    ///     making it able to undo and redo commands
    /// </summary>
    public class CompensableConversation : ICompensableConversation
    {
        private ObservableCollection<ICompensableCommand> _moveList;
        private Stack<ICompensableCommand> _redoCommands = new Stack<ICompensableCommand>();
        private Stack<ICompensableCommand> _undoCommands = new Stack<ICompensableCommand>();

        public CompensableConversation(ObservableCollection<ICompensableCommand> moveList)
        {
            _moveList = moveList;
            foreach (ICompensableCommand command in _moveList)
                _undoCommands.Push(command);
        }

        /// <summary>
        ///     Executes a command
        /// </summary>
        /// <param name="command">The command to execute</param>
        public void Execute(ICompensableCommand command)
        {
            command.Execute();
            _undoCommands.Push(command);
            _redoCommands.Clear();
        }

        /// <summary>
        ///     Undo the last command that has been done
        /// </summary>
        /// <returns>The last command, null if there is none</returns>
        public ICompensableCommand Undo()
        {
            if (_undoCommands.Count == 0) return null;

            ICompensableCommand command = _undoCommands.Pop();
            command.Compensate();
            _redoCommands.Push(command);

            return command;
        }

        /// <summary>
        ///     Redo the last command that has been undone
        /// </summary>
        /// <returns>The last command, null if there is none</returns>
        public ICompensableCommand Redo()
        {
            if (_redoCommands.Count == 0) return null;

            ICompensableCommand command = _redoCommands.Pop();
            command.Execute();
            _undoCommands.Push(command);

            return command;
        }
    }
}