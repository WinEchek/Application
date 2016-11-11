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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinEchek.Model;

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
            PieceView = new PieceView(square.Piece);
            Grid.Children.Add(PieceView);
            Background = new SolidColorBrush(square.Color);
            Grid.SetColumn(this, square.X);
            Grid.SetRow(this, square.Y);
        }
    }
}
