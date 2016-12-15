using System.Collections.Generic;
using WinEchek.Core;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.GUI
{
    public class BoardViewPlayerController : PlayerControler
    {
        private BoardView _boardView;
        public bool IsPlayable { get; set; }

        public BoardViewPlayerController(BoardView boardView)
        {
            _boardView = boardView;
        }

        public override void Play()
        {
            IsPlayable = true;
        }

        public override void Move(Move move)
        {
            IsPlayable = false;
            Player.Move(move);
        }

        public override void InvalidMove(List<string> reasonsList)
        {
            IsPlayable = true;
            //TODO Indiquer à l'IHM que le mouvement est invalide et pour quelles raisons
        }

        public override List<Square> PossibleMoves(Piece piece)
        {
            return Player.PossibleMoves(piece);
        }

        public override void Stop()
        {
            IsPlayable = false;
        }
    }
}