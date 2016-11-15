using System;
using System.IO;
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
            //_mainWindow.Flyout.Content = PLS.Content;
            //_mainWindow.Flyout.IsOpen = true;
            Flyout.IsOpen = true;
            Flyout.Visibility = Visibility.Visible;
            RoutedEvent eEvent =  Flyout.ClosingFinishedEvent;
            

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
            String directorySaveName = "Save";
            String fullSavePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + directorySaveName;
            Console.WriteLine(fullSavePath);
            if (Directory.Exists(fullSavePath) == false)
            {
                Directory.CreateDirectory(fullSavePath);
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "WinEchek Save Files (*.we)|*.we",
                InitialDirectory = fullSavePath
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                saver.Save(_mainWindow.WinEchek.Game, saveFileDialog.FileName);
            }
        }
    }
}
