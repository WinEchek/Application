using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WinEchek.Core;
using WinEchek.Engine;
using WinEchek.Model;
using Color = WinEchek.Model.Piece.Color;

namespace WinEchek.GUI
{
    /// <summary>
    ///     Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        private PieceView _selectedPiece;
        private SquareView _previousSquare;
        private bool _isPlayBoard;
        public Color Color { get; set; } = Color.White;
        public Board Board { get; set; }

        public static readonly DependencyProperty SetTextProperty =
         DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(SquareView));
        public BoardView(Board board, bool isPlayBoard = false)
        {
            InitializeComponent();
            Board = board;
            _isPlayBoard = isPlayBoard;

            for (int i = 0; i < Board.Size; i++) {
                Grid.RowDefinitions.Add(new RowDefinition());
                Grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            foreach (var square in Board.Squares)
            {
                var squareView = new SquareView(square)
                {
                    UcPieceView = {LayoutTransform = LayoutTransform},
                    LayoutTransform = LayoutTransform
                };
                Grid.Children.Add(squareView); //Position is set in the squareview constructor
            }
        }
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            var minNewSizeOfParentUserControl = Math.Min(sizeInfo.NewSize.Height, sizeInfo.NewSize.Width);
            Grid.Width = minNewSizeOfParentUserControl;
            Grid.Height = minNewSizeOfParentUserControl;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            //Only treats events if this boardview is intended to be played on
            if (!_isPlayBoard) return;

            var point = Mouse.GetPosition(Grid);

            var row = 0;
            var col = 0;
            var accumulatedHeight = 0.0;
            var accumulatedWidth = 0.0;

            // calc row mouse was over
            foreach (var rowDefinition in Grid.RowDefinitions)
            {
                accumulatedHeight += rowDefinition.ActualHeight;
                if (accumulatedHeight >= point.Y)
                    break;
                row++;
            }

            // calc col mouse was over
            foreach (var columnDefinition in Grid.ColumnDefinitions)
            {
                accumulatedWidth += columnDefinition.ActualWidth;
                if (accumulatedWidth >= point.X)
                    break;
                col++;
            }

            //TODO Move the event logic appart from this class
            var clickedSquare = Grid.Children
                .Cast<SquareView>() //Wonderful cast right here
                .First(x => Grid.GetRow(x) == row && Grid.GetColumn(x) == col);

            var clickedPieceView = clickedSquare.PieceView;

            if (_previousSquare == null)
            {
                if (clickedPieceView?.Piece.Color != Color) return;
                _previousSquare = clickedSquare;
                _selectedPiece = clickedSquare.PieceView;
                clickedSquare.BorderThickness = new Thickness(4);
                /* 
                 * ATTENTION ! CERTAINES PARTIQUES DE PROGRAMMATION UTILISÉES CI DESSOUS PEUVENT CHOQUER LA SENSIBILITÉ DE CERTAINS PROGRAMMEURS.
                 */
                 Engine.Engine lol = new RealEngine(Board);
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Move TestMove = new Move(_selectedPiece.Piece, Board.Squares[i, j]);
                        if (lol.PossibleMove(TestMove))
                        {
                            Grid.Children.Cast<SquareView>()
                                .First(x => Grid.GetRow(x) == j && Grid.GetColumn(x) == i)
                                .SetResourceReference(Control.BackgroundProperty, (i + j) % 2 == 0 ? "CleanWindowCloseButtonBackgroundBrush" : "CleanWindowCloseButtonPressedBackgroundBrush");
                        }
                    }
                }





            } else
            {
                foreach (SquareView lol in Grid.Children.Cast<SquareView>().ToList())
                {
                    lol.SetResourceReference(Control.BackgroundProperty,(lol.Square.X + lol.Square.Y) % 2 == 0 ? "AccentColorBrush" : "AccentColorBrush4");
                }
                
                _previousSquare.BorderThickness = new Thickness(0);
                BoardMove?.Invoke(new Move(_selectedPiece.Piece, clickedSquare.Square));
                _previousSquare = null;
                _selectedPiece = null;
            }
        }

        public delegate void MoveHandler(Move move);
        public event MoveHandler BoardMove;
    }
}