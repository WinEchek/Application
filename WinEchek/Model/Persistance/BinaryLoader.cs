using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using WinEchek.Engine;
using WinEchek.GUI;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Persistance
{
    class BinaryLoader : ILoader
    {
        //TODO should reload the history as well
        public Game Load(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            Board board = formatter.Deserialize(stream) as Board;
            if(board == null)
                throw new SerializationException("Cast exception");
            //TODO the loader should either be independant of the engine or know which one it is using
            RealEngine moteur = new RealEngine(board);
            RealPlayer player = new RealPlayer(null, Color.White);
            BoardView boardView = new BoardView(moteur.Board, player);
            Game game = new Game(moteur, boardView);
            player.Game = game;
            //TODO should move the player logic appart (can't while the mode logic isn"t implemented
            stream.Close();
            return game;
        }

        public string Filter()
        {
            return "WinEchek Binary Save Files (*.we)|*.we";
        }
    }
}
