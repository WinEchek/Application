using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using WinEchek.Model;

namespace WinEchek.IO
{
    public class BinarySaver : ISaver
    {
        public void Save(Container container, string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, container);
            stream.Close();
        }

        public string Filter()
        {
            return "WinEchek Binary Save Files (*.we)|*.we";
        }
    }
}