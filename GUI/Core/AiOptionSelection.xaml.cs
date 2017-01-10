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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinEchek.Game;
using WinEchek.Model;
using WinEchek.Model.Pieces;
using WinEchek.ModelView;

namespace WinEchek.Core
{
    /// <summary>
    /// Interaction logic for AiOptionSelection.xaml
    /// </summary>
    public partial class AiOptionSelection : UserControl
    {
        private MainWindow _mainWindow;
        private Container _container;

        public AiOptionSelection(MainWindow mainWindow, Container container)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _container = container;
            for (int i = 1; i <= 20; i++)
            {
                ComboBoxLevel.Items.Add(new ComboBoxItem().Content = i);
            }
            for (int i = 0; i <= 42; i++)
            {
                ComboBoxValue.Items.Add(new ComboBoxItem().Content = i);
            }
            ComboBoxLevel.SelectedValue = 20;
            ComboBoxValue.SelectedValue = 10;
        }
        
        private void ComboBoxSearchMode_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            int selectedIndex = ComboBoxSearchMode.SelectedIndex;

            if (selectedIndex == 0)
            {
                ComboBoxValue.Items.Clear();
                for (int i = 0; i <= 42; i++)
                {
                    ComboBoxValue.Items.Add(new ComboBoxItem().Content = i);
                }  
            }
            else
            {
                ComboBoxValue.Items.Clear();
                ComboBoxValue.IsEditable = true;
                for (int i = 500; i <= 5000; i+=500)
                {
                    ComboBoxValue.Items.Add(new ComboBoxItem().Content = i);
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO le controle pour l'antoine
            GameFactory gameFactory = new GameFactory();
            BoardView boardView = new BoardView(_container);
            Game game = gameFactory.CreateGame(Mode.AI, _container, boardView, Color.White, new GameCreatorParameters()
            {
                AiSearchType = ComboBoxSearchMode.SelectedIndex == 0 ? "depth" : "movetime",
                AiSearchValue = (int) ComboBoxValue.SelectedValue,
                AiSkillLevel = (int) ComboBoxLevel.SelectedValue
            });
            _mainWindow.MainControl.Content = new GameView(_mainWindow, game, boardView);
        }
    }
}
