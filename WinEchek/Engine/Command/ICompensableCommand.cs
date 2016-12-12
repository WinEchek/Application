using System;

namespace WinEchek.Engine.Command
{
    /// <summary>
    /// A command that can be done and undone
    /// </summary>
    public interface ICompensableCommand
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        void Execute();

        /// <summary>
        /// Undo the command
        /// </summary>
        void Compensate();

        string ToString();
    }
}