using System;
using WinEchek.Engine;
using WinEchek.GUI;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek
{
    [Serializable]
    public class Game
    {
        public IEngine Moteur { get; set; }
        public BoardView BoardView { get; set; }

        public Game()
        {
            Moteur = new RealEngine(new Board());
            BoardView = new BoardView(Moteur.Board, new RealPlayer(this, Color.White));
        }

        public Game(IEngine moteur, BoardView boardView)
        {
            Moteur = moteur;
            BoardView = boardView;
        }

        public void DoMove(Piece piece, Square square)
        {
            Moteur.DoMove(piece, square);
        }

        public void Undo()
        {
            Moteur.Undo();
        }

        public void Redo()
        {
            Moteur.Redo();
        }
    }
}