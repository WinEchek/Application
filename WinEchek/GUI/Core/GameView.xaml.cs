using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace WinEchek.GUI.Core {
    /// <summary>
    /// Logique d'interaction pour GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        private MainWindow _mainWindow;
        public GameView(MainWindow mw) {
            InitializeComponent();
            _mainWindow = mw;
            try
            {
                UcBoardView.Content = _mainWindow.WinEchek.Game.BoardView;
            }
            catch (Exception)
            {

                _mainWindow.ShowMessageAsync("Erreur", "Impossible d'afficher une partie non créée");
            }    
        }
    }
}
