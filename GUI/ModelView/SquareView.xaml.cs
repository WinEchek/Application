using System.ComponentModel;
using System.Windows.Controls;
using WinEchek.Model;

namespace WinEchek.ModelView
{
    /// <summary>
    ///     Interaction logic for SquareView.xaml
    /// </summary>
    public partial class SquareView : UserControl
    {
        public SquareView(Square square)
        {
            InitializeComponent();
            Square = square;
            DataContext = this;
            Square.PropertyChanged += SquarePropertyChangeHandler;

            if (square.Piece != null)
                PieceView = new PieceView(square.Piece);


            SetResourceReference(BackgroundProperty,
                (square.X + square.Y)%2 == 0 ? "AccentColorBrush" : "AccentColorBrush4");

            Grid.SetColumn(this, square.X);
            Grid.SetRow(this, square.Y);
            
        }

        public PieceView PieceView
        {
            get { return UcPieceView.Content as PieceView; }
            set { UcPieceView.Content = value; } 
        }

        public Square Square { get; set; }

        private void SquarePropertyChangeHandler(object sender, PropertyChangedEventArgs e)
        {
            PieceView = Square.Piece != null ? new PieceView(Square.Piece) : null;
            UcPieceView.Content = PieceView;
        }

        public override string ToString() => Square.ToString();
    }
}