using System;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Model.Utility
{
    public class FenTranslator
    {
        public static string FenNotation(Container container)
        {
            Board board = container.Board;

            string result = "";
            for (int i = 0; i < board.Size; i++)
            {
                int emptySquareNumber = 0;
                for (int j = 0; j < board.Size; j++)
                {
                    Square square = board.Squares[j, i];
                    if (square.Piece != null)
                    {
                        if (emptySquareNumber != 0)
                        {
                            result += emptySquareNumber;
                            emptySquareNumber = 0;
                        }
                        char c = ' ';

                        switch (square.Piece.Type)
                        {
                            case Type.Bishop:
                                c = 'b';
                                break;
                            case Type.King:
                                c = 'k';
                                break;
                            case Type.Queen:
                                c = 'q';
                                break;
                            case Type.Pawn:
                                c = 'p';
                                break;
                            case Type.Knight:
                                c = 'n';
                                break;
                            case Type.Rook:
                                c = 'r';
                                break;
                        }

                        result += (square.Piece.Color == Color.White) ? c.ToString().ToUpper() : c.ToString();
                    }
                    else
                    {
                        emptySquareNumber++;
                    }
                }
                if (emptySquareNumber != 0)
                    result += emptySquareNumber;
                result += '/';
            }

            result += ' ';

            result += container.Moves[container.Moves.Count - 1].PieceColor == Color.White ? 'b' : 'w';

            result += ' ';

            Piece.Piece blackRookQueen = null;
            Piece.Piece blackRookKing = null;
            Piece.Piece whiteRookQueen = null;
            Piece.Piece whiteRookKing = null;

            Piece.Piece blackKing = null;
            Piece.Piece whiteKing = null;

            Square enPassant = null;
            foreach (Square square in board.Squares)
            {
                if (square?.Piece?.Type == Type.King)
                {
                    if (square.Piece.Color == Color.White)
                        whiteKing = square.Piece;
                    else
                        blackKing = square.Piece;
                }
                else if (square?.Piece?.Type == Type.Rook)
                {
                    //QueenSide
                    if (square.X == 0)
                    {
                        if (square.Piece.Color == Color.White)
                            whiteRookQueen = square.Piece;
                        else
                            blackRookQueen = square.Piece;
                    }
                    else
                    {
                        if (square.Piece.Color == Color.White)
                            whiteRookKing = square.Piece;
                        else
                            blackRookKing = square.Piece;
                    }
                }


                else if (square?.Piece?.Type == Type.Pawn)
                {

                    if ((square.Piece as Pawn)?.EnPassant == true)
                    {
                        if (square?.Piece.Color == container.Moves[container.Moves.Count - 1].PieceColor)
                        {
                            enPassant = board.Squares[square.X, square.Piece.Color == Color.White ? square.Y + 1 : square.Y - 1];
                        }
                    }



                }
            }

            //Castling
            var bRQ = !blackRookQueen?.HasMoved == true;
            var bRK = !blackRookKing?.HasMoved == true;
            var wRQ = !whiteRookQueen?.HasMoved == true;
            var wRK = !whiteRookKing?.HasMoved == true;

            var wK = !whiteKing.HasMoved;
            var bK = !blackKing.HasMoved;

            if (wK)
            {
                if (wRK)
                    result += 'K';
                if (wRQ)
                    result += 'Q';
            }
            if (bK)
            {
                if (bRK)
                    result += 'k';
                if (bRQ)
                    result += 'q';
            }
            
            if (!(bK && (bRK || bRQ))
                && !(wK && (wRK || wRQ)))
                result += '-';

            result += ' ';

            //En passant
            
            if (enPassant != null)
                result += enPassant.ToString().ToLower();
            else
                result += '-';

            result += ' ';

            //Halfmove since last capture
            result += container.HalfMoveSinceLastCapture;

            result += ' ';

            result += (int) Math.Ceiling((double) (container.Moves.Count/2));
       
            return result;
        }
    }
}