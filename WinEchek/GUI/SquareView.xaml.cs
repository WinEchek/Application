using System;
using System.ComponentModel;
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
        public Square Square { get; set; }

        public SquareView(Square square)
        {
            InitializeComponent();
            Square = square;
            DataContext = this;
            Square.PropertyChanged += new PropertyChangedEventHandler(SquarePropertyChangeHandler);

            if (square.Piece != null)
                PieceView = new PieceView(square.Piece);

            //TODO add color theme support
            Background = new SolidColorBrush((square.X+square.Y)%2 == 0 ? Colors.Gray : Colors.DodgerBlue); 
            Grid.SetColumn(this, square.X);
            Grid.SetRow(this, square.Y);
            UcPieceView.Content = PieceView;
        }

        private void SquarePropertyChangeHandler(object sender, PropertyChangedEventArgs e)
        {
            if (Square.Piece != null)
                PieceView = new PieceView(Square.Piece);
            else
                PieceView = null;
            UcPieceView.Content = PieceView;
        }
    }
}
