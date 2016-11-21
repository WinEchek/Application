namespace WinEchek.Model
{
    public class Move
    {
        public Piece.Piece Piece { get; set; }
        public Square Square { get; set; }

        public Move(Piece.Piece piece, Square square)
        {
            Piece = piece;
            Square = square;
        }
    }
}