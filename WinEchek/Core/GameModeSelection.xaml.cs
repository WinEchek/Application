using System.Windows;
using WinEchek.Game;
using WinEchek.Model;
using WinEchek.ModelView;

namespace WinEchek.Core
{
    /// <summary>
    /// Interaction logic for GameModeSelection.xaml
    /// </summary>
    public partial class GameModeSelection
    {
        private MainWindow _mainWindow;
        private Container _container;

        public GameModeSelection(Container container, MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _container = container;
        }

        private void TileAiPlay_OnClick(object sender, RoutedEventArgs e)
        {
            GameFactory gameFactory = new GameFactory();
            BoardView boardView = new BoardView(_container.Board);
            WinEchek.Core.Game game = gameFactory.CreateGame(Mode.AI, _container, boardView);

            _mainWindow.MainControl.Content = new GameView(_mainWindow, game, boardView);
        }

        private void TileNetworkPlay_OnClick(object sender, RoutedEventArgs e)
        {
            //if(_container.Moves.Count == 0)
            _mainWindow.MainControl.Content = new HostJoin(_mainWindow, _container);
            // _mainWindow.MainControl.Content = new HostGameOptions(_mainWindow, _container);
        }

        private void LocalGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            GameFactory gameFactory = new GameFactory();
            BoardView boardView = new BoardView(_container.Board);
            WinEchek.Core.Game game = gameFactory.CreateGame(Mode.Local, _container, boardView);

            _mainWindow.MainControl.Content = new GameView(_mainWindow, game, boardView);
        }
    }
}
