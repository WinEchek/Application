using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using WinEchek.Engine;
using WinEchek.GUI;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Core.Persistance
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
            BoardView boardView = new BoardView(moteur.Board);
            RealPlayer player = new RealPlayer(Color.White, boardView);
            //TODO should move the player logic appart (can't while the mode logic isn"t implemented
            stream.Close();
            return null;
        }

        public string Filter()
        {
            return "WinEchek Binary Save Files (*.we)|*.we";
        }
    }
}
