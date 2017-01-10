using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WinEchek.Model.Pieces;
using WinEchek.ModelView;

namespace WinEchek.Views.Windows
{
    /// <summary>
    ///     Logique d'interaction pour PieceTypeSelectionWindow.xaml
    /// </summary>
    public partial class PieceTypeSelectionWindow
    {
        private UserControl _selectedControl;

        public PieceTypeSelectionWindow(Color color)
        {
            InitializeComponent();
            _selectedControl = null;
            UserControlQueen.Content = new PieceView(new Queen(color));
            UserControlQueen.SetResourceReference(BorderBrushProperty, "AccentColorBrush");

            UserControlRook.Content = new PieceView(new Rook(color));
            UserControlRook.SetResourceReference(BorderBrushProperty, "AccentColorBrush");

            UserControlBishop.Content = new PieceView(new Bishop(color));
            UserControlBishop.SetResourceReference(BorderBrushProperty, "AccentColorBrush");

            UserControlKnight.Content = new PieceView(new Knight(color));
            UserControlKnight.SetResourceReference(BorderBrushProperty, "AccentColorBrush");
        }

        public Type ChosenType { get; set; }

        private void ChangeSelectedControl(UserControl uc)
        {
            if (_selectedControl != null)
                _selectedControl.BorderThickness = new Thickness(0);

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
            if (pv == null) return;

            ChosenType = pv.Piece.Type;
            DialogResult = true;
            Close();
        }

        protected override void OnClosing(CancelEventArgs e) => e.Cancel = DialogResult == null;
    }
}