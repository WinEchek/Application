using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using WinEchek.Engine;
using WinEchek.Model.Pieces;
using WinEchek.ModelView;
using WinEchek.Views.FlyoutContent;
using WinEchek.Views.Widgets;

namespace WinEchek.Views
{
    /// <summary>
    ///     Logique d'interaction pour GameView.xaml
    /// </summary>
    public partial class GameView
    {
        private BoardView _boardView;
        private MainWindow _mainWindow;
        
        public GameView(MainWindow mainWindow, Core.Game game, BoardView boardView)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _boardView = boardView;
            Game = game;

            game.PlayerDisconnectedEvent += GameOnPlayerDisconnectedEvent;

            game.StateChanged += _boardView.GameStateChanged;
            
            game.StateChanged += state =>
            {
                switch (state)
                {
                    case BoardState.BlackCheckMate:
                        _mainWindow.ShowMessageAsync("Fin de la partie", "Le joueur noir est echec et mat.",
                            MessageDialogStyle.AffirmativeAndNegative);
                        break;
                    case BoardState.WhiteCheckMate:
                        _mainWindow.ShowMessageAsync("Fin de la partie", "Le joueur blanc est echec et mat.",
                            MessageDialogStyle.AffirmativeAndNegative);
                        break;
                    case BoardState.BlackPat:
                        _mainWindow.ShowMessageAsync("Match nul", "Le joueur noir est pat.",
                            MessageDialogStyle.AffirmativeAndNegative);
                        break;
                    case BoardState.WhitePat:
                        _mainWindow.ShowMessageAsync("Match nul", "Le joueur blanc est pat.",
                            MessageDialogStyle.AffirmativeAndNegative);
                        break;
                }
            };

            //Création et ajout du contenu du PLS pour cette vue
            GameViewFlyout gameViewFlyout = new GameViewFlyout(this);
            _mainWindow.Flyout.Content = gameViewFlyout.Content;
            UcBoardView.Content = boardView;

            game.Container.MoveDone += move =>
            {
                LabelPlayerTurn.Content = move.PieceColor == Color.Black ? "Blanc" : "Noir";
            };

            game.Container.MoveUndone += move =>
            {
                LabelPlayerTurn.Content = move.PieceColor == Color.Black ? "Blanc" : "Noir";
            };

            HistoryView.Content = new HistoryView(this);
        }

        private void GameOnPlayerDisconnectedEvent(string message)
        {
            _mainWindow.ShowMessageAsync("Erreur : Déconnexion d'un joueur", message);
        }

        public Core.Game Game { get; set; }

        private void ButtonUndo_OnClick(object sender, RoutedEventArgs e) => Game.Undo();
        private void ButtonRedo_OnClick(object sender, RoutedEventArgs e) => Game.Redo();

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

            var result =
                await
                    _mainWindow.ShowMessageAsync("Quitter la partie",
                        "Voulez-vous vraiment quitter la partie ? Si votre partie n'est pas sauvegardée, elle sera perdue...",
                        MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Affirmative)
            {
                _mainWindow.Flyout.Content = null;
                _mainWindow.MainControl.Content = new Home(_mainWindow);
            }
        }

        #endregion
    }
}