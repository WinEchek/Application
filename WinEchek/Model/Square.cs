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

        public Square(int x, int y)
        {
            X = x;
            Y = y;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
