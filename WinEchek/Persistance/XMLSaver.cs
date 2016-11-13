using System.IO;
using System.Xml.Serialization;

namespace WinEchek.Persistance
{
    class XMLSaver : ISaver
    {

        public void Save(Game game)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Game));
            StreamWriter writer = new StreamWriter("Game.xml", false);
            serializer.Serialize(writer, game);
            writer.Close();
        }
    }
}
