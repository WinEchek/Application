using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WinEchek.Command;
using WinEchek.Engine;
using WinEchek.Game;
using WinEchek.Model;
using WinEchek.Model.Pieces;
using WinEchek.Views.Windows;
using Color = WinEchek.Model.Pieces.Color;
using Type = WinEchek.Model.Pieces.Type;

namespace WinEchek.ModelView
{
    /// <summary>
    ///     Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        #region Fields

        private SquareView _lastChangedSquareView;
        private List<SquareView> _possibleMoves = new List<SquareView>();
        private List<SquareView> _lastMove = new List<SquareView>();
        private SquareView _clickedSquare;
        private PieceView _selectedPiece;
        private bool _mouseDown;
        private bool _selected;
        private Point _mouseDownPoint;
        private bool _initDragAndDropOnMouseMove;
        private bool _hasBeginDragAndDrop;
        private Container _container;

        #endregion

        #region Constructor

        public BoardView(Container container)
        {
            InitializeComponent();
            Board = container.Board;
            _container = container;

            for (int i = 0; i < Board.Size; i++)
            {
                Grid.RowDefinitions.Add(new RowDefinition());
                Grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            Grid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
            Grid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});
            /* Ajout des cases */
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
            /* Ajout des cases */
            /*  Numéro de case */
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
            /* Numéro de case */
            /* Affichage du dernier coup */
            _container.Moves.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add ||
                    args.Action == NotifyCollectionChangedAction.Remove)
                {
                    if (_container.Moves.Count != 0)
                    {
                        Console.WriteLine("Changed color");
                        _lastMove.ForEach(ResetSquareViewColor);
                        _lastMove.Clear();

                        ICompensableCommand command = _container.Moves.Last();
                        SquareView startSquare = SquareAt(command.Move.StartCoordinate);
                        SquareView targetSquare = SquareAt(command.Move.TargetCoordinate);

                        _lastMove.Add(targetSquare);

                        _lastMove.Add(startSquare);
                    }
                }
            };
        }

        #endregion

        #region Properties

        public List<SquareView> SquareViews { get; } = new List<SquareView>();
        public Board Board { get; set; }

        public List<BoardViewPlayerController> BoardViewPlayerControllers { get; set; } =
            new List<BoardViewPlayerController>();

        #endregion

        #region EventHandling

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.ChangedButton != MouseButton.Left) return;
            _mouseDown = true;
            _mouseDownPoint = e.GetPosition(Grid);
            _clickedSquare = SquareAt(e.GetPosition(Grid));

            if (_clickedSquare == null) return;

            if (_selected && _possibleMoves.Contains(_clickedSquare)) return; 

            ResetBoardColor();
            _selectedPiece = _clickedSquare.PieceView;


            //--Verified--//
            //Concerned controllers to get the possible moves
            List<BoardViewPlayerController> concernedControllers =
                BoardViewPlayerControllers.FindAll(
                    x => (x.Player.Color == (_selectedPiece?.Piece.Color) && x.IsPlayable));

            if (concernedControllers.Count == 0) return;
            _initDragAndDropOnMouseMove = true;

            //Possible move drawing
            foreach (Square square in concernedControllers.First().PossibleMoves(_selectedPiece.Piece))
            {
                SquareView squareView =
                    Grid.Children.Cast<SquareView>()
                        .First(x => Grid.GetRow(x) == square.Y && Grid.GetColumn(x) == square.X);

                squareView.SetResourceReference(BackgroundProperty,
                    (square.X + square.Y)%2 == 0
                        ? "CleanWindowCloseButtonBackgroundBrush"
                        : "CleanWindowCloseButtonPressedBackgroundBrush");

                _possibleMoves.Add(squareView);
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.ChangedButton != MouseButton.Left) return;
            _mouseDown = false;
            _initDragAndDropOnMouseMove = false;

            var concernedControllers = ConcernedControllers();

            if (concernedControllers.Count == 0) return;

            Move move = null;
            SquareView squareView = SquareAt(e.GetPosition(Grid));
            //TODO Put all the fields in the corresponding state
            if (squareView == null)
            {
                if (_selectedPiece == null) return;
                Canvas.Children.Remove(_selectedPiece);
                _clickedSquare.Square.Piece = _selectedPiece.Piece;
                _selectedPiece = null;
                _selected = false;
                return;
            }
            SquareView clickedSquareView = SquareAt(_mouseDownPoint);

            bool select = Equals(squareView.Square.Coordinate, clickedSquareView?.Square?.Coordinate); //Same square

            if (select)
            {
                if (_hasBeginDragAndDrop)
                {
                    Canvas.Children.Remove(_selectedPiece);
                    _clickedSquare.PieceView = _selectedPiece;
                    _selectedPiece = null;
                    _hasBeginDragAndDrop = false;
                    ResetBoardColor();
                    return;
                }
                //First click
                if (!_selected)
                {
                    Console.WriteLine("Selection");
                    _selected = true;
                }
                //Second click
                else
                {
                    if (!_possibleMoves.Contains(_clickedSquare)) return;

                    _selected = false;
                    ResetBoardColor();
                    if ((_selectedPiece.Piece.Type == Type.Pawn) &&
                    (squareView.Square.Y == (_selectedPiece.Piece.Color == Color.White ? 0 : 7)))
                    {
                        var promoteDialog = new PieceTypeSelectionWindow(_selectedPiece.Piece.Color);
                        promoteDialog.ShowDialog();
                        move = new Move(_selectedPiece.Piece.Square, squareView.Square, _selectedPiece.Piece.Type,
                            _selectedPiece.Piece.Color, promoteDialog.ChosenType);
                    }
                    else
                    {
                        move = new Move(_selectedPiece.Piece, squareView.Square);
                    }
                    concernedControllers.ForEach(x => x.Move(move));
                }
            }
            else //Drag case
            {
                if (!_possibleMoves.Contains(squareView))
                {
                    ResetDragAndDrop();
                    ResetBoardColor();
                    return;
                }
                Canvas.Children.Remove(_selectedPiece);
                ResetBoardColor();

                if ((_selectedPiece.Piece.Type == Type.Pawn) &&
                    (squareView.Square.Y == (_selectedPiece.Piece.Color == Color.White ? 0 : 7)))
                {
                    var promoteDialog = new PieceTypeSelectionWindow(_selectedPiece.Piece.Color);
                    promoteDialog.ShowDialog();
                    move = new Move(_selectedPiece.Piece.Square, squareView.Square, _selectedPiece.Piece.Type,
                        _selectedPiece.Piece.Color, promoteDialog.ChosenType);
                }
                else
                {
                    move = new Move(_selectedPiece.Piece, squareView.Square);
                }
                concernedControllers.ForEach(x => x.Move(move));
                _selected = false;
                _hasBeginDragAndDrop = false;
            }
        }



        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Point p = e.GetPosition(Grid);
            if (p.X < 0 || p.Y < 0 || p.X > Grid.ActualWidth || p.Y > Grid.ActualHeight)
            {
                if (_selectedPiece == null) return;
                Canvas.Children.Remove(_selectedPiece);
                _clickedSquare.Square.Piece = _selectedPiece.Piece;
                _selectedPiece = null;
                _selected = false;
                return;
            }
            //Use the threshold too
            if (!_mouseDown) return;
            if (_selectedPiece == null) return;
            if (_initDragAndDropOnMouseMove && (_mouseDownPoint - e.GetPosition(Grid)).Length > 5)
            {
                Console.WriteLine("Init drag and drop");
                _clickedSquare.PieceView = null;

                var width = _selectedPiece.ActualWidth;
                var height = _selectedPiece.ActualHeight;

                //Put the pieceView in the canvas to be able to drag&drop
                Canvas.Children.Add(_selectedPiece);

                _selectedPiece.Height = height;
                _selectedPiece.Width = width;

                _initDragAndDropOnMouseMove = false;
                _hasBeginDragAndDrop = true;
            }
            Canvas.SetTop(_selectedPiece, e.GetPosition(this).Y - _selectedPiece.ActualHeight/2);
            Canvas.SetLeft(_selectedPiece, e.GetPosition(this).X - _selectedPiece.ActualWidth/2);
        }

        //TODO add on mouse leave
        #endregion

        #region Coloration

        private void ResetBoardColor()
        {
            _possibleMoves.ForEach(ResetSquareViewColor);
            _lastMove.ForEach(x => x.SetResourceReference(BackgroundProperty,
                (x.Square.X + x.Square.Y)%2 == 0
                    ? "CheckBoxBrush"
                    : "CheckBoxMouseOverBrush"));
            _possibleMoves.Clear();
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

        #endregion

        #region Utils

        private void ResetDragAndDrop()
        {
            if (_selectedPiece == null) return;
            Canvas.Children.Remove(_selectedPiece);
            _clickedSquare.PieceView = _selectedPiece;
            _selectedPiece = null;
            _hasBeginDragAndDrop = false;
        }

        private SquareView SquareAt(Point point)
        {
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

            // calc column mouse was over
            foreach (var columnDefinition in Grid.ColumnDefinitions)
            {
                accumulatedWidth += columnDefinition.ActualWidth;
                if (accumulatedWidth >= point.X)
                    break;
                col++;
            }

            var clickedControl = Grid.Children
                .OfType<UIElement>()
                .First(x => (Grid.GetRow(x) == row) && (Grid.GetColumn(x) == col));

            return clickedControl as SquareView;
        }

        private SquareView SquareAt(Coordinate coordinate) =>
            Grid.Children.Cast<UIElement>()
                .FirstOrDefault(e => Grid.GetColumn(e) == coordinate.X && Grid.GetRow(e) == coordinate.Y) as SquareView;

        private List<BoardViewPlayerController> ConcernedControllers() =>
                BoardViewPlayerControllers.FindAll(x => (x.Player.Color == (_selectedPiece?.Piece.Color) && x.IsPlayable));

        #endregion

        #region Misc
        public static readonly DependencyProperty SetTextProperty = DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(SquareView));
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            //Pour rendre le plateau toujours carré
            var minNewSizeOfParentUserControl = Math.Min(sizeInfo.NewSize.Height, sizeInfo.NewSize.Width);
            Grid.Width = minNewSizeOfParentUserControl;
            Grid.Height = minNewSizeOfParentUserControl;
        }
        #endregion
    }
}
 