using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using WinEchek.Engine;
using WinEchek.GUI.Core.FlyoutContent;
using WinEchek.GUI.Core.Widgets;
using WinEchek.Model.Piece;

namespace WinEchek.GUI.Core {

    /// <summary>
    /// Logique d'interaction pour GameView.xaml
    /// </summary>
    public partial class GameView
    {
        public Game Game { get; set; }
        private MainWindow _mainWindow;
        private BoardView _boardView;

        public GameView(MainWindow mainWindow, Game game, BoardView boardView) {
            InitializeComponent();
            _mainWindow = mainWindow;
            _boardView = boardView;
            Game = game;

            game.StateChanged += _boardView.GameStateChanged;
            game.StateChanged += state =>
            {
                if (state == BoardState.BlackCheckMate)
                {
                    _mainWindow.ShowMessageAsync("Fin de la partie", "Le joueur noir est echec et mat.", MessageDialogStyle.AffirmativeAndNegative);
                }
                else if (state == BoardState.WhiteCheckMate)
                {
                    _mainWindow.ShowMessageAsync("Fin de la partie", "Le joueur blanc est echec et mat.", MessageDialogStyle.AffirmativeAndNegative);
                }
                else if (state == BoardState.BlackPat || state == BoardState.WhitePat)
                {
                    _mainWindow.ShowMessageAsync("Match nul", "Le joueur blanc est echec et mat.", MessageDialogStyle.AffirmativeAndNegative);
                }

            };
            
            //Création et ajout du contenu du PLS pour cette vue
            GameViewFlyout gameViewFlyout = new GameViewFlyout(this);
            _mainWindow.Flyout.Content = gameViewFlyout.Content;
            UcBoardView.Content = boardView;

            HistoryView.Content = new HistoryView(this);
        }


        #region Flyout

        private void Grid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_mainWindow.Flyout.IsOpen) return;
            _mainWindow.Flyout.IsOpen = false;
        }

        private void ButtonMenu_OnClick(object sender, RoutedEventArgs e)
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

        #endregion

        private void ButtonUndo_OnClick(object sender, RoutedEventArgs e) => Game.Undo();
        private void ButtonRedo_OnClick(object sender, RoutedEventArgs e) => Game.Redo();
    }
}
