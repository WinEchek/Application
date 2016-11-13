using System.Windows.Controls;
using System.Windows.Media;
using WinEchek.Model;
using Color = WinEchek.Model.Piece.Color;

namespace WinEchek.GUI
{
    /// <summary>
    /// Interaction logic for SquareView.xaml
    /// </summary>
    public partial class SquareView : UserControl
    {
        public PieceView PieceView { get; set; }

        public SquareView(Square square)
        {
            InitializeComponent();
            if(square.Piece != null)
                PieceView = new PieceView(square.Piece);
            DataContext = this;
            //TODO add color theme support
            Background = new SolidColorBrush((square.X+square.Y)%2 == 0 ? Colors.Gray : Colors.DodgerBlue); 
            Grid.SetColumn(this, square.X);
            Grid.SetRow(this, square.Y);
        }
    }
}
