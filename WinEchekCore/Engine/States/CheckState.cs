using System.Collections.Generic;
using System.Linq;
using WinEchek.Engine.Rules;
using WinEchek.Model;
using WinEchek.Model.Pieces;

namespace WinEchek.Engine.States
{
    public class CheckState : IState
    {
        public bool IsInState(Board board, Color color)
        {
            /*
             * On construit des groupes de règles spéciales qui ne tienne pas compte
             * de celle de la mise en echec
             */
            Board tempBoard = new Board(board);
            List<IRule> queenMovementCheckRules = new List<IRule> {new QueenMovementRule(), new CanOnlyTakeEnnemyRule()};

            List<IRule> pawnMovementCheckRules = new List<IRule> {new PawnMovementRule(), new CanOnlyTakeEnnemyRule()};

            List<IRule> kingMovementCheckRules = new List<IRule> {new KingMovementRule(), new CanOnlyTakeEnnemyRule(), new CastlingRule()};

            List<IRule> knightMovementCheckRules = new List<IRule>
            {
                new KnightMovementRule(),
                new CanOnlyTakeEnnemyRule()
            };

            List<IRule> rookMovementCheckRules = new List<IRule> {new CanOnlyTakeEnnemyRule(), new RookMovementRule()};

            List<IRule> bishopMovementCheckRules = new List<IRule>
            {
                new CanOnlyTakeEnnemyRule(),
                new BishopMovementRule()
            };

            Dictionary<Type, List<IRule>> rulesGroup = new Dictionary<Type, List<IRule>>
            {
                {Type.Queen, queenMovementCheckRules},
                {Type.Pawn, pawnMovementCheckRules},
                {Type.Knight, knightMovementCheckRules},
                {Type.Rook, rookMovementCheckRules},
                {Type.Bishop, bishopMovementCheckRules},
                {Type.King, kingMovementCheckRules}
            };


            // On cherche le roi
            Piece concernedKing = tempBoard.Squares.OfType<Square>()
                .First(x => (x?.Piece?.Type == Type.King) && (x?.Piece?.Color == color)).Piece;

            bool res = false;
            foreach (KeyValuePair<Type, List<IRule>> rules in rulesGroup)
            {
                List<Square> possibleMoves = new List<Square>();
                concernedKing.Type = rules.Key;
                possibleMoves = possibleMoves.Concat(rules.Value.First().PossibleMoves(concernedKing)).ToList();
                rules.Value.ForEach(
                    x => possibleMoves = possibleMoves.Intersect(x.PossibleMoves(concernedKing)).ToList());

                if (possibleMoves.Any(x => x?.Piece?.Type == rules.Key))
                    // Vérifier si il ne faut pas être d'une couleur différente
                    res = true;
            }
            concernedKing.Type = Type.King;
            return res;
        }

        public string Explain() => "Le roi du joueur est en echec";
    }
}