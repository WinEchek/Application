using System;
using System.Collections.Generic;
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
    public partial class BoardView
    {
        private PieceView _selectedPiece;
        private SquareView _previousSquare;
        public Board Board { get; set; }
        public List<BoardViewPlayerController> BoardViewPlayerControllers { get; set; } = new List<BoardViewPlayerController>();
        public static readonly DependencyProperty SetTextProperty =
         DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(SquareView));
        public BoardView(Board board)
        {
            InitializeComponent();
            Board = board;
            

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

            // Si le plateau ne contrôle aucun joueur
            if(BoardViewPlayerControllers.Count == 0) return;

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

            var clickedSquare = Grid.Children
                .Cast<SquareView>() //Wonderful cast right here
                .First(x => Grid.GetRow(x) == row && Grid.GetColumn(x) == col);

            var clickedPieceView = clickedSquare.PieceView;

            List<BoardViewPlayerController> concernedControllers =
                BoardViewPlayerControllers.FindAll(x => (x.Player.Color == ((_selectedPiece == null) ? clickedPieceView?.Piece.Color : _selectedPiece?.Piece.Color) && x.IsPlayable));
            if (concernedControllers.Count == 0) return;
            

            if (_previousSquare == null)
            {
                _previousSquare = clickedSquare;
                _selectedPiece = clickedSquare.PieceView;
                clickedSquare.BorderThickness = new Thickness(4);

                foreach (Square square in concernedControllers.First().PossibleMoves(_selectedPiece.Piece))
                {
                    Grid.Children.Cast<SquareView>()
                                .First(x => Grid.GetRow(x) == square.Y && Grid.GetColumn(x) == square.X)
                                .SetResourceReference(Control.BackgroundProperty, (square.X + square.Y) % 2 == 0 ? "CleanWindowCloseButtonBackgroundBrush" : "CleanWindowCloseButtonPressedBackgroundBrush");
                }
            }
            else
            {
                foreach (SquareView squareView in Grid.Children.Cast<SquareView>().ToList())
                {
                    squareView.SetResourceReference(Control.BackgroundProperty,(squareView.Square.X + squareView.Square.Y) % 2 == 0 ? "AccentColorBrush" : "AccentColorBrush4");
                }
                
                _previousSquare.BorderThickness = new Thickness(0);
                
                Move move = new Move(_selectedPiece.Piece, clickedSquare.Square);
                concernedControllers.ForEach(x => x.Move(move));
                _previousSquare = null;
                _selectedPiece = null;
            }
        }

    }
}