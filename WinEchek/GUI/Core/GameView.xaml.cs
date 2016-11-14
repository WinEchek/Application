using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;
using WinEchek.Persistance;

namespace WinEchek.GUI.Core {
    /// <summary>
    /// Logique d'interaction pour GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public Game Game { get; set; }
        /// <summary>
        /// Liens vers la fenêtre principale pour effectuer les interactions avec celle-ci (Dialog, ...)
        /// </summary>
        private MainWindow _mainWindow;

        public GameView(MainWindow mw, Game game) {
            InitializeComponent();
            _mainWindow = mw;
            Game = game;
            try
            {
                UcBoardView.Content = Game.BoardView;
            }
            catch (Exception)
            {

                _mainWindow.ShowMessageAsync("Erreur", "Impossible d'afficher une partie non créée");
            }    
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            BinarySaver saver = new BinarySaver();
            saver.Save(_mainWindow.WinEchek.Game, "Game.bin");
        }

        private void Load_OnClick(object sender, RoutedEventArgs e)
        {
            BinaryLoader loader = new BinaryLoader();
            Game game = loader.Load("Game.bin");
            _mainWindow.WinEchek.Game = game;
            UcBoardView.Content = game.BoardView;
        }
    }
}
