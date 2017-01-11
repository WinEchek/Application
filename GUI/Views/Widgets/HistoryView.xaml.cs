using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WinEchek.Command;
using WinEchek.Model;
using WinEchek.ModelView;

namespace WinEchek.Views.Widgets
{
    /// <summary>
    ///     Logique d'interaction pour HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        //TODO the board should adapt to the loaded size
        private Board _board = new Board();
        private BoardView _boardView;
        private HistoryViewConversation _conversation;
        private Core.Game _game;
        private GameView _gameView;
        private int _lastIndex = -1;
        private ObservableCollection<ICompensableCommand> _moves = new ObservableCollection<ICompensableCommand>();
        private BoardView _realBoardView;

        public HistoryView(GameView gameView)
        {
            InitializeComponent();
            _game = gameView.Game;

            _gameView = gameView;
            _realBoardView = gameView.UcBoardView.Content as BoardView;
            _conversation = new HistoryViewConversation();

            foreach (ICompensableCommand command in _game.Container.Moves)
            {
                ICompensableCommand momand = command.Copy(_board);
                _conversation.Execute(momand);
                _moves.Add(momand);
            }

            _game.Container.Moves.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    ICompensableCommand command = args.NewItems[args.NewItems.Count - 1] as ICompensableCommand;
                    command = command.Copy(_board);
                    _conversation.Execute(command);
                    _moves.Add(command);
                }
                if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    _moves.RemoveAt(_moves.Count - 1);
                    _conversation.Undo();
                }
            };

            _boardView = new BoardView(new Container(_board, _moves));
            ListViewHistory.ItemsSource = _moves;
        }

        private void ListViewHistory_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Reinit();

            _gameView.UcBoardView.Content = _realBoardView;
            _lastIndex = -1;
        }


        private void EventSetter_OnHandler(object sender, MouseEventArgs e)
        {
            var item = (sender as FrameworkElement)?.DataContext;
            Console.WriteLine("wow");
            int index = ListViewHistory.Items.IndexOf(item);
            var plop = sender as ListViewItem;
            if (_lastIndex == -1)
                for (int i = 1; i < _moves.Count - index; i++)
                    _conversation.Undo();
            else if (index < _lastIndex)
                for (int i = 0; i < _lastIndex - index; i++)
                    _conversation.Undo();
            else if (index > _lastIndex)
                for (int i = 0; i < index - _lastIndex; i++)
                    _conversation.Redo();
            _lastIndex = index;
        }

        private void ListViewHistory_OnMouseEnter(object sender, MouseEventArgs e)
        {
            _gameView.UcBoardView.Content = _boardView;
        }

        private void ItemListDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as FrameworkElement)?.DataContext;
            int index = ListViewHistory.Items.IndexOf(item);
            var plop = sender as ListViewItem;

            Reinit();

            int count = _moves.Count;

            _game.Undo(count - index - 1);

            _lastIndex = -1;
        }

        private void Reinit()
        {
            if ((_lastIndex == -1) || (_lastIndex == _moves.Count - 1)) return;
            for (int i = 1; i < _moves.Count - _lastIndex; i++)
                _conversation.Redo();
        }

        private void ListViewHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}