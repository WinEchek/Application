using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using WinEchek.GUI.Core.FlyoutContent;
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

        private GameViewFlyout _gameViewFlyout;

        public GameView(MainWindow mw, Game game) {
            InitializeComponent();
            _mainWindow = mw;
            Game = game;

            /**
             * Création et ajout du contenu du PLS pour cette vue
             */
            _gameViewFlyout = new GameViewFlyout(this);
            _mainWindow.Flyout.Content = _gameViewFlyout.Content;

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
            
        }

        private void ButtonMenu_OnClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.Flyout.IsOpen = !_mainWindow.Flyout.IsOpen;
        }
    }
}
