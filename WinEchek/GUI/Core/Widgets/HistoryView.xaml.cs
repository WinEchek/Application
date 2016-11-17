using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.GUI.Core.Widgets
{
    /// <summary>
    /// Logique d'interaction pour HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        private ObservableCollection<MoveInfo> _moves = new ObservableCollection<MoveInfo>();
        private int lastIndex = -1;
        public HistoryView()
        {
            InitializeComponent();
            ListViewHistory.ItemsSource = _moves;
        }

        public void Add(Piece piece, Square startSquare, Square targetSquare)
        {
            _moves.Add(new MoveInfo(_moves.Count+1, piece, startSquare, targetSquare));
        }

        public void Remove()
        {
            if(_moves.Count == 0) return;
            _moves.RemoveAt(_moves.Count-1);
        }

        public delegate void ListItemNumber(int i);

        public event ListItemNumber ListItemOvered;

        private void ListViewHistory_OnMouseMove(object sender, MouseEventArgs e)
        {
            //TODO change to on item over
            int index = -1;
            for (int i = 0; i < _moves.Count; i++)
            {
                var lbi = ListViewHistory.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                if (lbi == null) continue;
                if (IsMouseOverTarget(lbi, e.GetPosition((IInputElement) lbi)))
                {
                    index = i;
                    break;
                }
            }
            if (index != -1 && index != lastIndex)
            {
                lastIndex = index;
                ListItemOvered?.Invoke(_moves.Count - index);
            }
        }

        private static bool IsMouseOverTarget(Visual target, Point point)
        {
            var bounds = VisualTreeHelper.GetDescendantBounds(target);
            return bounds.Contains(point);
        }

        class MoveInfo
        {
            public int Number { get; set; }
            public string Move { get; set; }

            public MoveInfo(int number, Piece piece, Square startSquare, Square targetSquare)
            {
                Number = number;
                Move = piece.Type + " " + startSquare.X + "," + startSquare.Y + " vers " + targetSquare.X + "," +
                       targetSquare.Y;
            }
        }
    }
}