namespace WinEchek.Model.Piece
{
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
        Knight,
        Rook
    }

    public enum Color
    {
        White,
        Black
    }

}
