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
using System.Windows.Shapes;
using WinEchek.Model.Piece;
using Color = WinEchek.Model.Piece.Color;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.GUI.Core.Windows {
    /// <summary>
    /// Logique d'interaction pour PieceTypeSelectionWindow.xaml
    /// </summary>
    public partial class PieceTypeSelectionWindow
    {

        public Type ChosenType { get; set; }
        private UserControl _selectedControl;

        public PieceTypeSelectionWindow(Color color) {
            InitializeComponent();
            _selectedControl = null;
            UserControlQueen.Content = new PieceView(new Queen(color));
            UserControlQueen.SetResourceReference(Control.BorderBrushProperty, "AccentColorBrush");

            UserControlRook.Content = new PieceView(new Rook(color));
            UserControlRook.SetResourceReference(Control.BorderBrushProperty, "AccentColorBrush");

            UserControlBishop.Content = new PieceView(new Bishop(color));
            UserControlBishop.SetResourceReference(Control.BorderBrushProperty, "AccentColorBrush");

            UserControlKnight.Content = new PieceView(new Knight(color));
            UserControlKnight.SetResourceReference(Control.BorderBrushProperty, "AccentColorBrush");

        }

        private void ChangeSelectedControl(UserControl uc)
        {
            if (_selectedControl != null)
            {
                _selectedControl.BorderThickness = new Thickness(0);
            }

            _selectedControl = uc;
            _selectedControl.BorderThickness = new Thickness(2);

        }

        private void UserControlQueen_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeSelectedControl(UserControlQueen);
        }

        private void UserControlBishop_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeSelectedControl(UserControlBishop);
        }

        private void UserControlRook_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeSelectedControl(UserControlRook);
        }

        private void UserControlKnight_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeSelectedControl(UserControlKnight);
        }

        private void ButtonValidation_OnClick(object sender, RoutedEventArgs e)
        {
            PieceView pv = _selectedControl.Content as PieceView;
            ChosenType = pv.Piece.Type;
            DialogResult = true;
        }
    }
}
