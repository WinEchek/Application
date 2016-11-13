using System.ComponentModel;
using System.Runtime.CompilerServices;
using WinEchek.Annotations;

namespace WinEchek.Model.Piece
{
    public class Piece : INotifyPropertyChanged
    {
        public Type Type { get; }
        public Color Color { get; }
        public Square Square { get; set; }

        public Piece(Type type, Color color, Square square)
        {
            this.Type = type;
            Color = color;
            Square = square;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public enum Type {
        Bishop,
        King,
        Queen,
        Pawn,
        Knight,
        Rook
    }

    public enum Color
    {
        White,
        Black
    }

}
