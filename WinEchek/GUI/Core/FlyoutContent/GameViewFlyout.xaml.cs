using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using WinEchek.Persistance;

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
        private void TileSave_OnClick(object sender, RoutedEventArgs e)
        {
            BinarySaver saver = new BinarySaver();
            String directorySaveName = "Save";
            String fullSavePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + directorySaveName;
            Console.WriteLine(fullSavePath);
            if (Directory.Exists(fullSavePath) == false) {
                Directory.CreateDirectory(fullSavePath);
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog {
                Filter = "WinEchek Save Files (*.we)|*.we",
                InitialDirectory = fullSavePath
            };
            if (saveFileDialog.ShowDialog() == true) {
                saver.Save(_gameView.Game, saveFileDialog.FileName);
            }
        }
    }
}
