using System;
using System.IO;
using System.Xml.Serialization;

namespace WinEchek.Core.Persistance
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

        public string Filter()
        {
            throw new NotImplementedException();
        }
    }
}
