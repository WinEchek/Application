using System.CodeDom;
using System.Windows.Controls;
using WinEchek.Command;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine
{
    public class RealEngine : IEngine
    {
        public Board Board { get; }
        private CompensableConversation _conversation = new CompensableConversation();

        public RealEngine(Board board)
        {
            Board = board;
        }

        public bool DoMove(Piece piece, Square square)
        {
            //TODO check rules here
            _conversation.Execute(new Move(piece, square));
            return true;
        }

        public void Undo()
        {
            _conversation.Undo();
        }

        public void Redo()
        {
            _conversation.Redo();
        }
    }
}