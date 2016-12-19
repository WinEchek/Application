using System.IO;
using WinEchek.Model;

namespace WinEchek.Core.Persistance
{
    class FileLogger
    {
        private Container _container;
        private ISaver _saver = new BinarySaver();

        public FileLogger(Game game)
        {
            _container = game.Container;
            if (File.Exists("log.temp"))
            {
                //TODO file handling
                int i = 0;
                while (File.Exists("crashlog" + i)) i++;
                File.Copy("log.temp", "crashlog_" + i + ".we");
            }

            game.StateChanged += state =>
            {
                _saver.Save(_container, "log.temp");
            };
        }

    }
}