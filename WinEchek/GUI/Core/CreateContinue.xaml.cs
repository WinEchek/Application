using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using WinEchek.Core;
using WinEchek.Core.Persistance;
using WinEchek.Model;

namespace WinEchek.GUI.Core {
    /// <summary>
    /// Logique d'interaction pour CreateContinue.xaml
    /// </summary>
    public partial class CreateContinue : UserControl
    {
        private readonly MainWindow _mainWindow;

        public CreateContinue(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void CreateNewGameTile_OnClick(object sender, RoutedEventArgs e)
        {
            GameFactory gameFactory = new GameFactory();
            Container container = new Container();
            BoardView boardView = new BoardView(container.Board);
            Game game = gameFactory.CreateGame(Mode.Local, container, boardView);

            _mainWindow.MainControl.Content = new GameView(_mainWindow, game, boardView);
        }

        private void TileLoadGame_OnClick(object sender, RoutedEventArgs e)
        {
            ILoader loader = new BinaryLoader();

            string directorySaveName = "Save";
            string fullSavePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + directorySaveName;

            if (Directory.Exists(fullSavePath) == false)
            {
                Directory.CreateDirectory(fullSavePath);
            }

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = loader.Filter(),
                InitialDirectory = fullSavePath
            };

            if (openFileDialog.ShowDialog() == true)
            {
                GameFactory gameFactory = new GameFactory();
                Container container = loader.Load(openFileDialog.FileName); //TODO verify filename
                BoardView boardView = new BoardView(container.Board);
                Game game = gameFactory.CreateGame(Mode.Local, container, boardView);

                _mainWindow.MainControl.Content = new GameView(_mainWindow, game, boardView);
            }

        }
    }
}
