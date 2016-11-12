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
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        public RealPlayer RealPlayer { get; set; }
        public BoardView(Board board, RealPlayer player)
        {
            InitializeComponent();
            RealPlayer = player;

            for (int i = 0; i < Board.Size; i++)
            {
                Grid.RowDefinitions.Add(new RowDefinition());
                Grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            foreach (Square square in board.Squares)
            {
                SquareView squareView = new SquareView(square);
                if (square?.Piece.Color == player.Color) //TODO Check the boardview add the player to the pieceview
                    squareView.PieceView.Player = player;
                Grid.Children.Add(squareView); //Position is set in the squareview constructor
            }
        }

        //TODO check if this resizing policy isn't problematic
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            double minNewSizeOfParentUserControl = Math.Min(sizeInfo.NewSize.Height, sizeInfo.NewSize.Width);
            Grid.Width = minNewSizeOfParentUserControl;
            Grid.Height = minNewSizeOfParentUserControl;
        }

        
        private SquareView selectedSquare;
        private PieceView selectedPiece;
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            var point = Mouse.GetPosition(Grid);

            int row = 0;
            int col = 0;
            double accumulatedHeight = 0.0;
            double accumulatedWidth = 0.0;

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
            SquareView view = Grid.Children
                .Cast<SquareView>() //Wonderful cast right here
                .First(x => Grid.GetRow(x) == row && Grid.GetColumn(x) == col);

            PieceView clickedPieceView = view.PieceView;

            if (selectedSquare == null)
            {
                if(clickedPieceView?.Player.Color != RealPlayer.Color) return;
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
