using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WinEchek.Annotations;
using Color = WinEchek.Model.Piece.Color;

namespace WinEchek.Model
{
    [Serializable]
    public class Square : INotifyPropertyChanged
    {
        public Board Board { get; set; }
        private Piece.Piece _piece;
        public Piece.Piece Piece
        {
            get { return _piece; }
            set
            {
                _piece = value;
                OnPropertyChanged();
            }
        }

        public int X { get; }
        public int Y { get; }

        public Square(Board parent,int x, int y)
        {
            Board = parent;
            X = x;
            Y = y;
        }

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
