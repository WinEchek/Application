using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WinEchek.Model;

namespace WinEchek.GUI
{
    /// <summary>
    ///     Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        private PieceView selectedPiece;

        private SquareView selectedSquare;

        public BoardView(Board board, RealPlayer player)
        {
            InitializeComponent();
            Board = board;
            RealPlayer = player;

            Refresh();
        }

        public RealPlayer RealPlayer { get; set; }
        public Board Board { get; set; }

        public void Refresh()
        {
            for (var i = 0; i < Board.Size; i++)
            {
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

        //TODO check if this resizing policy isn't problematic
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
            var view = Grid.Children
                .Cast<SquareView>() //Wonderful cast right here
                .First(x => Grid.GetRow(x) == row && Grid.GetColumn(x) == col);

            var clickedPieceView = view.PieceView;

            if (selectedSquare == null)
            {
                if (clickedPieceView == null) return;
                if (clickedPieceView.Piece.Color != RealPlayer.Color) return;
                selectedSquare = view;
                selectedPiece = view.PieceView;
                view.BorderThickness = new Thickness(4);
                view.BorderBrush = new SolidColorBrush(Colors.Blue);
            }
            else
            {
                if (clickedPieceView == null)
                {
                    selectedSquare.BorderThickness = new Thickness(0);
                    selectedSquare.PieceView = null;
                    view.PieceView = selectedPiece;
                    selectedSquare = null;
                    selectedPiece = null;
                }
                else
                {
                    view.PieceView = null;
                    selectedSquare.BorderThickness = new Thickness(0);
                    selectedSquare.PieceView = null;
                    view.PieceView = selectedPiece;
                    selectedSquare = null;
                    selectedPiece = null;
                }
            }
        }
    }
}