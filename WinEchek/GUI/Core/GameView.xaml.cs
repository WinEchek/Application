using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Controls;
using WinEchek.Model;

namespace WinEchek.GUI.Core {
    /// <summary>
    /// Logique d'interaction pour GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        private MainWindow _mainWindow;
        public GameView(MainWindow mw) {
            InitializeComponent();
            _mainWindow = mw;
            Game game = new Game();
            Grid.Children.Add(game.BoardView);
        }
    }
}
