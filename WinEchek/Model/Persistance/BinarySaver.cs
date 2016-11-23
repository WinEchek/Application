using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using WinEchek.GUI;
using WinEchek.Model;

namespace WinEchek.Persistance
{
    class BinarySaver : ISaver
    {
        public void Save(Game game, string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            Board board = game.BoardView.Board;
            formatter.Serialize(stream, board);
            //TODO should serialize command for the motor and reconstruct it with the command too
            stream.Close();
        }

        public string Filter()
        {
            return "WinEchek Binary Save Files (*.we)|*.we";
        }
    }
}
