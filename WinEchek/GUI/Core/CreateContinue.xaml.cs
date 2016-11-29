using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using WinEchek.Core.Persistance;

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
            _mainWindow.WinEchek.CreateGame();
            _mainWindow.MainControl.Content = new GameView(_mainWindow, _mainWindow.WinEchek.Game);
        }

        private void TileLoadGame_OnClick(object sender, RoutedEventArgs e)
        {
            ILoader loader = new BinaryLoader();

            string directorySaveName = "Save";
            string fullSavePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + directorySaveName;
            Console.WriteLine(fullSavePath);
            if (Directory.Exists(fullSavePath) == false) {
                Directory.CreateDirectory(fullSavePath);
            }
            OpenFileDialog openFileDialog = new OpenFileDialog() {
                Filter = loader.Filter(),
                InitialDirectory = fullSavePath
            };
            if (openFileDialog.ShowDialog() == true) {
                _mainWindow.WinEchek.Game = loader.Load(openFileDialog.FileName);
                _mainWindow.MainControl.Content = new GameView(_mainWindow, _mainWindow.WinEchek.Game);
            }

        }
    }
}
