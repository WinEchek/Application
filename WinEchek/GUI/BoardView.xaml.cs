using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro.Controls;
using WinEchek.Model;

namespace WinEchek.GUI
{
    /// <summary>
    ///     Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        private PieceView selectedPiece;

        private SquareView previousSquare;
        public RealPlayer RealPlayer { get; set; }
        public Board Board { get; set; }

        public static readonly DependencyProperty SetTextProperty =
         DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(SquareView));
        public BoardView(Board board, RealPlayer player)
        {
            InitializeComponent();



            RealPlayer = player;
            Board = board;
            for (int i = 0; i < Board.Size; i++) {
                Grid.RowDefinitions.Add(new RowDefinition());
                Grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            foreach (var square in Board.Squares)
            {
                var squareView = new SquareView(square);
                if (square.Piece != null && square.Piece.Color == RealPlayer.Color)
                    squareView.PieceView.Player = RealPlayer;
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

            if (previousSquare == null)
            {
                if (clickedPieceView == null) return;
                if (clickedPieceView.Piece.Color != RealPlayer.Color) return;
                previousSquare = clickedSquare;
                selectedPiece = clickedSquare.PieceView;
                clickedSquare.BorderThickness = new Thickness(4);
                //SetResourceReference(BorderBrushProperty, "BlackColorBrush");
                clickedSquare.BorderBrush = new SolidColorBrush(Colors.Blue);
            }
            else
            {
                if (clickedPieceView == null)
                {
                    previousSquare.BorderThickness = new Thickness(0);
                    RealPlayer.DoMove(selectedPiece.Piece, clickedSquare.Square);
                    previousSquare = null;
                    selectedPiece = null;
                }
                else
                {
                    clickedSquare.PieceView = null;
                    previousSquare.BorderThickness = new Thickness(0);
                    RealPlayer.DoMove(selectedPiece.Piece, clickedSquare.Square);
                    previousSquare = null;
                    selectedPiece = null;
                }
            }
        }
    }
}