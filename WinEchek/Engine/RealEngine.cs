using System.CodeDom;
using System.Windows.Controls;
using WinEchek.Command;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine
{
    public class RealEngine : Engine
    {
        public override Board Board { get; }
        private CompensableConversation _conversation = new CompensableConversation();

        public RealEngine(Board board)
        {
            Board = board;
        }

        public override bool DoMove(Piece piece, Square square)
        {
            //TODO check rules here
            Square startSquare = piece.Square;
            _conversation.Execute(new Move(piece, square));
            MoveDone?.Invoke(this, new MoveEventArgs(piece, startSquare, square));
            return true;
        }

        public override void Undo()
        {
            _conversation.Undo();
        }

        public override void Redo()
        {
            Move move = _conversation.Redo() as Move;
            if(move!=null)
                MoveDone?.Invoke(this, new MoveEventArgs(move.Piece, move.Square, move.Piece.Square));
        }

        public override event MoveHandler MoveDone;
    }
}