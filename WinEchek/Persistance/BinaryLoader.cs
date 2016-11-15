using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using WinEchek.Engine;
using WinEchek.GUI;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Persistance
{
    class BinaryLoader : ILoader
    {
        public Game Load(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            Board board = formatter.Deserialize(stream) as Board;
            if(board == null)
                throw new SerializationException("Cast exception");
            YoloMoteur moteur = new YoloMoteur(board);
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
