namespace WinEchek.Model.Piece
{
    //TODO Add an enum or something like that to define the piece color
    public class Piece
    {
        public Type Type { get; }
        public Color Color { get; }

        public Piece(Type type, Color color)
        {
            this.Type = type;
            Color = color;
        }

    }
    public enum Type {
        Bishop,
        King,
        Queen,
        Pawn,
        Tower,
        Rook
    }

    public enum Color
    {
        White,
        Black
    }

}
