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
using WinEchek.GUI;
using WinEchek.Model.Piece;
using Color = WinEchek.Model.Piece.Color;

namespace WinEchek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserControl uc = new PieceView(new King(Color.Black));
        public MainWindow()
        {
            InitializeComponent();
            mabite.Children.Add(uc);
        }
    }
}
