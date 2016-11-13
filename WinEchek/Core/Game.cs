using WinEchek.Engine;
using WinEchek.GUI;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek
{
    public class Game
    {
        YoloMoteur moteur = new YoloMoteur(new Board());
        public BoardView BoardView { get; set; }
        public Game()
        {
            BoardView = new BoardView(moteur.Board, new RealPlayer(this, Color.White));
        }
        public void DoMove(Piece piece, Square square)
        {
            moteur.DoMove(piece, square);
        }
    }
}