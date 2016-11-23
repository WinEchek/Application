using System.IO;
using System.Xml.Serialization;

namespace WinEchek.Core.Persistance
{
    class XMLSaver : ISaver
    {

        public void Save(Game game, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Game));
            StreamWriter writer = new StreamWriter(path, false);
            serializer.Serialize(writer, game);
            writer.Close();
        }

        public string Filter()
        {
            throw new System.NotImplementedException();
        }
    }
}
