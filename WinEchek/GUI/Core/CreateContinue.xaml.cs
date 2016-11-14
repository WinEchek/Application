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
using WinEchek.Persistance;

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
            _mainWindow.MainControl.Content = new GameView(_mainWindow);
        }

        private void TileLoadGame_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO: déplacer la logique des sauvegarde dans WinEchek et retourner les éventuels affichages à la graphique (msgb)
            ILoader loader = new BinaryLoader();
            _mainWindow.WinEchek.Game = loader.Load("Game.bin");
            _mainWindow.MainControl.Content = new GameView(_mainWindow);
        }
    }
}
