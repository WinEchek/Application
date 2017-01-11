using System.Windows;
using System.Windows.Controls;
using WinEchek.Game;
using WinEchek.Model;
using WinEchek.Model.Pieces;
using WinEchek.ModelView;

namespace WinEchek.Views
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
            ComboBoxLevel.SelectedIndex = 19;
            ComboBoxValue.SelectedIndex = 10;
            ComboBoxSearchMode.SelectedIndex = 0;
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
                ComboBoxValue.SelectedIndex = 10;
            }
            else
            {
                ComboBoxValue.Items.Clear();
                for (int i = 500; i <= 5000; i+=500)
                {
                    ComboBoxValue.Items.Add(new ComboBoxItem().Content = i);
                }
                ComboBoxValue.SelectedIndex = 3;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO le controle pour l'antoine
            GameFactory gameFactory = new GameFactory();
            BoardView boardView = new BoardView(_container);
            int skillLevel = ComboBoxLevel.SelectedValue as int? ?? 0;
            int searchValue = ComboBoxValue.SelectedValue as int? ?? 0;
            Core.Game game = gameFactory.CreateGame(Mode.AI, _container, boardView, Color.White, new GameCreatorParameters()
            {
                AiSearchType = ComboBoxSearchMode.SelectedIndex == 0 ? "depth" : "movetime",
                AiSearchValue = searchValue,
                AiSkillLevel = skillLevel
            });
            _mainWindow.MainControl.Content = new GameView(_mainWindow, game, boardView);
        }
    }
}
