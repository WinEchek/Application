using System;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WinEchek.Model.Piece;
using Color = WinEchek.Model.Piece.Color;
using Type = WinEchek.Model.Piece.Type;


namespace WinEchek.GUI {
    /// <summary>
    /// Logique d'interaction pour PieceView.xaml
    /// </summary>
    public partial class PieceView : UserControl {
        //TODO : ça va Binder un jour ou l'autre, mais pour le moment les rageux bavent
        public Piece Piece { get; set; }
        public PieceView(Piece piece)
        {
            InitializeComponent();
            this.Piece = piece;
            switch (Piece.Type)
            {
                case Type.Bishop:
                    Image.Source = (Piece.Color == Color.Black ? FindResource("BlackBishop") : FindResource("WhiteBishop")) as ImageSource;
                    break;
                case Type.King:
                    Image.Source = (Piece.Color == Color.Black ? FindResource("BlackKing") : FindResource("WhiteKing")) as ImageSource;
                    break;
                case Type.Queen:
                    Image.Source = (Piece.Color == Color.Black ? FindResource("BlackQueen") : FindResource("WhiteQueen")) as ImageSource;
                    break;
                case Type.Pawn:
                    Image.Source = (Piece.Color == Color.Black ? FindResource("BlackPawn") : FindResource("WhitePawn")) as ImageSource;
                    break;
                case Type.Tower:
                    Image.Source = (Piece.Color == Color.Black ? FindResource("BlackTower") : FindResource("WhiteTower")) as ImageSource;
                    break;
                case Type.Rook:
                    Image.Source = (Piece.Color == Color.Black ? FindResource("BlackRook") : FindResource("WhiteRook")) as ImageSource;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
