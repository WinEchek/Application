using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinEchek.GUI;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Color = WinEchek.Model.Piece.Color;


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
