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
            //TODO Determine who adds the board an how
            Grid.Children.Add(new BoardView(new Board()));
        }
    }
}
