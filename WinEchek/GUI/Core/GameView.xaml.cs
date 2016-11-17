using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using WinEchek.Command;
using WinEchek.Engine;
using WinEchek.GUI.Core.FlyoutContent;

namespace WinEchek.GUI.Core {
    /// <summary>
    /// Logique d'interaction pour GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public Game Game { get; set; }
        private MainWindow _mainWindow;
        private int _lastStateDisplayed = 0;

        public GameView(MainWindow mw, Game game) {
            InitializeComponent();
            _mainWindow = mw;
            Game = game;

            //Event handler when a move is done
            game.Engine.MoveDone += MoveDone;
            HistoryView.ListItemOvered += DrawOldBoard;
            HistoryView.MouseLeave += RefreshBoard;

            //Création et ajout du contenu du PLS pour cette vue
            GameViewFlyout gameViewFlyout = new GameViewFlyout(this);
            _mainWindow.Flyout.Content = gameViewFlyout.Content;
            
            try
            {
                UcBoardView.Content = Game.BoardView;
            }
            catch (Exception)
            {
                //TODO could be another exception
                _mainWindow.ShowMessageAsync("Erreur", "Impossible d'afficher une partie non créée");
            }    
        }

        private void RefreshBoard(object sender, MouseEventArgs e)
        {
            if (_lastStateDisplayed == 0) return;
            for (int i = 0; i < _lastStateDisplayed; i++)
            {
                Game.Redo();
            }
            _lastStateDisplayed = 0;
        }

        private void DrawOldBoard(int index)
        {
            if (_lastStateDisplayed > index)
            {
                for (int i = 0; i < _lastStateDisplayed - index; i++)
                {
                    Game.Redo();
                }
            }
            else
            {
                for (int i = 0; i < index-_lastStateDisplayed; i++)
                {
                    Game.Undo();
                }
            }
            Console.WriteLine(index);
            _lastStateDisplayed = index;
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

        private void MoveDone(object sender, MoveEventArgs eventArgs)
        {
            if(_lastStateDisplayed == 0)
                HistoryView.Add(eventArgs.Piece, eventArgs.StartSquare, eventArgs.TargetSquare);
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

        #endregion

        #region Undo Redo

        private void ButtonUndo_OnClick(object sender, RoutedEventArgs e)
        {
            Game.Undo();
            HistoryView.Remove();
        }

        private void ButtonRedo_OnClick(object sender, RoutedEventArgs e)
        {
            Game.Redo();
        }

        #endregion
        
    }
}
