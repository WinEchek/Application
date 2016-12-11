using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WinEchek.Engine.Command;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.GUI.Core.Widgets
{
    //TODO should be just a widget
    /// <summary>
    /// Logique d'interaction pour HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        private ObservableCollection<ICompensableCommand> _moves;
        private Game _game;
        private int _lastIndex = -1;

        public HistoryView(Game game)
        {
            InitializeComponent();

            _game = game;
            _moves = game.Container.Moves;
            ListViewHistory.ItemsSource = _moves;
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
            if (index != -1 && index != _lastIndex)
            {
                _lastIndex = index;
                ListItemOvered?.Invoke(_moves.Count - index);
            }
        }

        private static bool IsMouseOverTarget(Visual target, Point point)
        {
            var bounds = VisualTreeHelper.GetDescendantBounds(target);
            return bounds.Contains(point);
        }
    }
}