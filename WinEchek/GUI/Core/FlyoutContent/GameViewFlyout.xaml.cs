using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using WinEchek.Core.Persistance;

namespace WinEchek.GUI.Core.FlyoutContent
{
    /// <summary>
    /// Logique d'interaction pour GameViewFlyout.xaml
    /// </summary>
    public partial class GameViewFlyout : UserControl
    {
        private GameView _gameView;
        public GameViewFlyout(GameView gameView)
        {
            InitializeComponent();
            _gameView = gameView;
        }
        /// <summary>
        /// Action effectuée lors d'un click sur la tile sauvegarder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //TODO A refaire
        private void TileSave_OnClick(object sender, RoutedEventArgs e)
        {
            ISaver saver = new BinarySaver();
            String directorySaveName = "Save";
            String fullSavePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + directorySaveName;
            Console.WriteLine(fullSavePath);
            if (Directory.Exists(fullSavePath) == false) {
                Directory.CreateDirectory(fullSavePath);
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog {
                Filter = saver.Filter(),
                InitialDirectory = fullSavePath
            };
            if (saveFileDialog.ShowDialog() == true) {
                saver.Save(_gameView.Game, saveFileDialog.FileName);
            }
        }

        private void TileQuit_OnClick(object sender, RoutedEventArgs e)
        {
            _gameView.Quit();
        }
    }
}
