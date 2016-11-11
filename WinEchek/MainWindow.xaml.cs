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
    public partial class MainWindow : Window
    {

        private UserControl uc = new BoardView(new Board());
        public MainWindow()
        {
            InitializeComponent();
            mabite.Children.Add(uc);
	    }
    }
}
