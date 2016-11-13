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
        private PieceView _pieceView;
        public PieceView PieceView
        {
            get { return _pieceView; }
            set
            {
                if(_pieceView != null)
                    Grid.Children.Remove(_pieceView);
                if (value != null)
                    Grid.Children.Add(value);
                _pieceView = value;
            } 
        }

        public SquareView(Square square)
        {
            InitializeComponent();
            if(square.Piece != null)
                PieceView = new PieceView(square.Piece);
            //TODO add color theme support
            Background = new SolidColorBrush((square.X+square.Y)%2 == 0 ? Colors.Gray : Colors.DodgerBlue); 
            Grid.SetColumn(this, square.X);
            Grid.SetRow(this, square.Y);
        }
    }
}
