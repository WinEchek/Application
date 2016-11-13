using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinEchek.Persistance
{
    class XMLLoader : ILoader
    {
        public Game Load(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Game));
            StreamReader reader = new StreamReader(path);
            Game game = serializer.Deserialize(reader) as Game;
            return game;
        }
    }
}
