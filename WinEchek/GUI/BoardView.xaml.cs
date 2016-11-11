using System;
using System.Windows;
using System.Windows.Controls;
using WinEchek.Model;

namespace WinEchek.GUI
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        public BoardView(Board board)
        {
            InitializeComponent();
            for (int i = 0; i < Board.Size; i++)
            {
                Grid.RowDefinitions.Add(new RowDefinition());
                Grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            foreach (Square square in board.Squares)
            {
                SquareView squareView = new SquareView(square);
                Grid.Children.Add(squareView); //Position is set in the squareview constructor
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            double minNewSizeOfParentUserControl = Math.Min(sizeInfo.NewSize.Height, sizeInfo.NewSize.Width);
            Grid.Width = minNewSizeOfParentUserControl;
            Grid.Height = minNewSizeOfParentUserControl;
        }
    }
}
