using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinEchek.Command;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Color = WinEchek.Model.Piece.Color;

namespace WinEchek.GUI.Core.Widgets
{
    /// <summary>
    /// Logique d'interaction pour HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        ObservableCollection<MoveInfo> moves = new ObservableCollection<MoveInfo>();
        public HistoryView()
        {
            InitializeComponent();
            ListViewHistory.ItemsSource = moves;
        }

        public void Add(Piece piece, Square startSquare, Square targetSquare)
        {
            moves.Add(new MoveInfo(moves.Count+1, piece, startSquare, targetSquare));
        }

        public void Remove()
        {
            if(moves.Count == 0) return;
            moves.RemoveAt(moves.Count-1);
        }
    }

    class MoveInfo
    {
        public int Number { get; set; }
        public string Move { get; set; }

        public MoveInfo(int number, Piece piece, Square startSquare, Square targetSquare)
        {
            Number = number;
            Move = piece.Type() + " " + startSquare.X + "," + startSquare.Y + " vers " + targetSquare.X + "," + targetSquare.Y;
        }
    }
}