using System;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;
using WinEchek.Core;
using WinEchek.Engine;
using WinEchek.GUI;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek
{
    //TODO the game should implement a mode, two players and make them play together
    [Serializable]
    public class Game
    {
        public IPlayer WhitePlayer { get; set; }
        public IPlayer BlackPlayer { get; set; }
        public Engine.Engine Engine { get; set; }
        public BoardView BoardView { get; set; }

        private bool _end = false;

        public Game()
        {
            Engine = new RealEngine(new Board());
            WhitePlayer = new RealPlayer(this, Color.White);
            BlackPlayer = new RealPlayer(this, Color.Black);
            BoardView = new BoardView(Engine.Board, (RealPlayer) WhitePlayer);
            Start();
        }

        public Game(Engine.Engine engine, BoardView boardView)
        {
            Engine = engine;
            BoardView = boardView;
        }

        public bool DoMove(Move move) => Engine.DoMove(move);

        public void Undo()
        {
            Engine.Undo();
        }

        public void Redo()
        {
            Engine.Redo();
        }

        public void Start()
        {
            Task.Run(() => StartTask());
        }

        private void StartTask() {
            IPlayer currentPlayer = WhitePlayer;
            while (!_end)
            {
                Move currentMove = currentPlayer.Play();

                /**
                bool res = DoMove(currentMove);
                while (DoMove(currentMove) != true)
                    currentMove = currentPlayer.Play();
    */
                // Ou récupérer le mouvement d'une autre manière (event par exemple)
                if (currentPlayer == WhitePlayer)
                    currentPlayer = BlackPlayer;
                else
                    currentPlayer = WhitePlayer;
            }
        }
    }


    public enum Mode
    {
        Local,
        Network,
        AI
    }
}