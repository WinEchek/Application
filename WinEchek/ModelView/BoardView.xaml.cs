using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WinEchek.Core.Windows;
using WinEchek.Engine;
using WinEchek.Game;
using WinEchek.Model;
using Color = WinEchek.Model.Pieces.Color;
using Type = WinEchek.Model.Pieces.Type;

namespace WinEchek.ModelView
{
    /// <summary>
    ///     Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        public static readonly DependencyProperty SetTextProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(SquareView));

        private SquareView _lastChangedSquareView;

        private List<SquareView> _possibleMoves = new List<SquareView>();
        private SquareView _previousSquare;
        private PieceView _selectedPiece;

        public BoardView(Board board)
        {
            InitializeComponent();
            Board = board;

            for (int i = 0; i < Board.Size; i++)
            {
                Grid.RowDefinitions.Add(new RowDefinition());
                Grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            Grid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
            Grid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});

            foreach (var square in Board.Squares)
            {
                var squareView = new SquareView(square)
                {
                    UcPieceView = {LayoutTransform = LayoutTransform},
                    LayoutTransform = LayoutTransform
                };
                SquareViews.Add(squareView);
                Grid.Children.Add(squareView); //Position is set in the squareview constructor
            }

            for (int i = 0; i < Board.Size; i++)
            {
                Label label = new Label
                {
                    Content = (char) ('A' + i),
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetColumn(label, i);
                Grid.SetRow(label, 8);
                Grid.Children.Add(label);
            }

            for (int i = Board.Size; i > 0; i--)
            {
                Label label = new Label
                {
                    Content = Board.Size - i + 1,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(label, 8);
                Grid.SetRow(label, i - 1);
                Grid.Children.Add(label);
            }
        }

        public List<SquareView> SquareViews { get; } = new List<SquareView>();
        public Board Board { get; set; }

        public List<BoardViewPlayerController> BoardViewPlayerControllers { get; set; } =
            new List<BoardViewPlayerController>();

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
            if (BoardViewPlayerControllers.Count == 0) return;

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
                .First(x => (Grid.GetRow(x) == row) && (Grid.GetColumn(x) == col));

            var clickedPieceView = clickedSquare.PieceView;

            List<BoardViewPlayerController> concernedControllers =
                BoardViewPlayerControllers.FindAll(
                    x =>
                        (x.Player.Color ==
                         (_selectedPiece == null ? clickedPieceView?.Piece.Color : _selectedPiece?.Piece.Color)) &&
                        x.IsPlayable);

            if (concernedControllers.Count == 0) return;


            if (_previousSquare == null)
            {
                _previousSquare = clickedSquare;
                _selectedPiece = clickedSquare.PieceView;
                clickedSquare.BorderThickness = new Thickness(4);

                foreach (Square square in concernedControllers.First().PossibleMoves(_selectedPiece.Piece))
                {
                    SquareView squareView =
                        Grid.Children.Cast<SquareView>()
                            .First(x => (Grid.GetRow(x) == square.Y) && (Grid.GetColumn(x) == square.X));
                    squareView.SetResourceReference(BackgroundProperty,
                        (square.X + square.Y)%2 == 0
                            ? "CleanWindowCloseButtonBackgroundBrush"
                            : "CleanWindowCloseButtonPressedBackgroundBrush");
                    _possibleMoves.Add(squareView);
                }
            }
            else
            {
                _previousSquare.BorderThickness = new Thickness(0);
                Move move;
                if ((_selectedPiece.Piece.Type == Type.Pawn) &&
                    (clickedSquare.Square.Y == (_selectedPiece.Piece.Color == Color.White ? 0 : 7)) &&
                    _possibleMoves.Contains(clickedSquare))
                {
                    var promoteDialog = new PieceTypeSelectionWindow(_selectedPiece.Piece.Color);
                    promoteDialog.ShowDialog();

                    move = new Move(_selectedPiece.Piece.Square, clickedSquare.Square, _selectedPiece.Piece.Type,
                        _selectedPiece.Piece.Color, promoteDialog.ChosenType);
                }
                else
                {
                    move = new Move(_selectedPiece.Piece, clickedSquare.Square);
                }

                concernedControllers.ForEach(x => x.Move(move));

                ResetBoardColor();

                _possibleMoves.Clear();

                _previousSquare = null;
                _selectedPiece = null;
            }
        }

        private void ResetBoardColor()
        {
            foreach (SquareView squareView in _possibleMoves)
                ResetSquareViewColor(squareView);
        }

        private static void ResetSquareViewColor(SquareView squareView)
        {
            squareView.SetResourceReference(BackgroundProperty,
                (squareView.Square.X + squareView.Square.Y)%2 == 0 ? "AccentColorBrush" : "AccentColorBrush4");
        }

        public void GameStateChanged(BoardState state)
        {
            SquareView squareView = null;

            ResetBoardColor();

            switch (state)
            {
                case BoardState.Normal:
                    if (_lastChangedSquareView != null)
                        ResetSquareViewColor(_lastChangedSquareView);
                    break;
                case BoardState.WhiteCheck:
                    squareView =
                        SquareViews.First(
                            x => (x.Square?.Piece?.Type == Type.King) && (x.Square?.Piece?.Color == Color.White));
                    squareView.SetResourceReference(BackgroundProperty, "ValidationBrush5");
                    break;
                case BoardState.BlackCheck:
                    squareView =
                        SquareViews.First(
                            x => (x.Square?.Piece?.Type == Type.King) && (x.Square?.Piece?.Color == Color.Black));
                    squareView.SetResourceReference(BackgroundProperty, "ValidationBrush5");
                    break;
                case BoardState.BlackCheckMate:
                    squareView =
                        SquareViews.First(
                            x => (x.Square?.Piece?.Type == Type.King) && (x.Square?.Piece?.Color == Color.Black));
                    squareView.SetResourceReference(BackgroundProperty, "TextBrush");
                    break;
                case BoardState.WhiteCheckMate:
                    squareView =
                        SquareViews.First(
                            x => (x.Square?.Piece?.Type == Type.King) && (x.Square?.Piece?.Color == Color.White));
                    squareView.SetResourceReference(BackgroundProperty, "TextBrush");
                    break;
                case BoardState.BlackPat:
                    squareView =
                        SquareViews.First(
                            x => (x.Square?.Piece?.Type == Type.King) && (x.Square?.Piece?.Color == Color.Black));
                    squareView.SetResourceReference(BackgroundProperty, "WhiteColorBrush");
                    break;
                case BoardState.WhitePat:
                    squareView =
                        SquareViews.First(
                            x => (x.Square?.Piece?.Type == Type.King) && (x.Square?.Piece?.Color == Color.White));
                    squareView.SetResourceReference(BackgroundProperty, "WhiteColorBrush");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
            _lastChangedSquareView = squareView;
        }
    }
}