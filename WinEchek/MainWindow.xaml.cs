using WinEchek.GUI;
using WinEchek.Model;


namespace WinEchek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            Game game = new Game();
            Grid.Children.Add(game.BoardView);
        }
    }
}
