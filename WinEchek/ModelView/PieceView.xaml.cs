using System;
using System.Windows.Media;
using WinEchek.Model.Pieces;
using Color = WinEchek.Model.Pieces.Color;
using Type = WinEchek.Model.Pieces.Type;

namespace WinEchek.ModelView
{
    /// <summary>
    ///     Logique d'interaction pour PieceView.xaml
    /// </summary>
    public partial class PieceView
    {
        private Piece _piece;

        public PieceView(Piece piece)
        {
            InitializeComponent();
            Piece = piece;
        }

        public Piece Piece
        {
            get { return _piece; }
            set
            {
                _piece = value;
                switch (Piece.Type)
                {
                    case Type.Bishop:
                        Image.Source =
                            (Piece.Color == Color.Black ? FindResource("BlackBishop") : FindResource("WhiteBishop")) as
                                ImageSource;
                        break;
                    case Type.King:
                        Image.Source =
                            (Piece.Color == Color.Black ? FindResource("BlackKing") : FindResource("WhiteKing")) as
                                ImageSource;
                        break;
                    case Type.Queen:
                        Image.Source =
                            (Piece.Color == Color.Black ? FindResource("BlackQueen") : FindResource("WhiteQueen")) as
                                ImageSource;
                        break;
                    case Type.Pawn:
                        Image.Source =
                            (Piece.Color == Color.Black ? FindResource("BlackPawn") : FindResource("WhitePawn")) as
                                ImageSource;
                        break;
                    case Type.Knight:
                        Image.Source =
                            (Piece.Color == Color.Black ? FindResource("BlackKnight") : FindResource("WhiteKnight")) as
                                ImageSource;
                        break;
                    case Type.Rook:
                        Image.Source =
                            (Piece.Color == Color.Black ? FindResource("BlackRook") : FindResource("WhiteRook")) as
                                ImageSource;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}