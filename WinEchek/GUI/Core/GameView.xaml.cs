using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using WinEchek.GUI.Core.FlyoutContent;

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
            _mainWindow.Flyout.MouseLeave += Flyout_MouseLeave;

            try
            {
                UcBoardView.Content = Game.BoardView;
            }
            catch (Exception)
            {

                _mainWindow.ShowMessageAsync("Erreur", "Impossible d'afficher une partie non créée");
            }    
        }

        private void Flyout_MouseLeave(object sender, MouseEventArgs e)
        {
            //TODO check if it is fully open
            if (!_mainWindow.Flyout.IsOpen) return;
            _mainWindow.Flyout.IsOpen = false;
        }

        private void ButtonMenu_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (_mainWindow.Flyout.IsOpen) return;
            _mainWindow.Flyout.IsOpen = true;
        }

        public async Task Quit() 
        { 

            _mainWindow.Flyout.IsOpen = false;

            var result = await _mainWindow.ShowMessageAsync("Quitter la partie", "Voulez-vous vraiment quitter la partie ? Si votre partie n'est pas sauvegardée, elle sera perdue...", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                _mainWindow.Flyout.Content = null;
                _mainWindow.WinEchek.Game = null;
                _mainWindow.MainControl.Content = new Home(_mainWindow);
            }
            
        }
    }
}
