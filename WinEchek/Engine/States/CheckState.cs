using System.Collections.Generic;
using System.Linq;
using WinEchek.Engine.Rules;
using WinEchek.Model;
using WinEchek.Model.Piece;

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
            List<IRule> QueenMovementCheckRules = new List<IRule>();
            QueenMovementCheckRules.Add(new QueenMovementRule());
            QueenMovementCheckRules.Add(new CanOnlyTakeEnnemyRule());

            List<IRule> PawnMovementCheckRules = new List<IRule>();
            PawnMovementCheckRules.Add(new PawnMovementRule());
            PawnMovementCheckRules.Add(new CanOnlyTakeEnnemyRule());

            List<IRule> KingMovementCheckRules = new List<IRule>();
            KingMovementCheckRules.Add(new KingMovementRule());
            KingMovementCheckRules.Add(new CanOnlyTakeEnnemyRule());

            List<IRule> KnightMovementCheckRules = new List<IRule>();
            KnightMovementCheckRules.Add(new KnightMovementRule());
            KnightMovementCheckRules.Add(new CanOnlyTakeEnnemyRule());

            List<IRule> RookMovementCheckRules = new List<IRule>();
            RookMovementCheckRules.Add(new CanOnlyTakeEnnemyRule());
            RookMovementCheckRules.Add(new RookMovementRule());

            List<IRule> BishopMovementCheckRules = new List<IRule>();
            BishopMovementCheckRules.Add(new CanOnlyTakeEnnemyRule());
            BishopMovementCheckRules.Add(new BishopMovementRule());

            Dictionary<Type, List<IRule>> rulesGroup = new Dictionary<Type, List<IRule>>();
            rulesGroup.Add(Type.Queen ,QueenMovementCheckRules);
            rulesGroup.Add(Type.Pawn ,PawnMovementCheckRules);
            //rulesGroup.Add(Type.King, KingMovementCheckRules);
            rulesGroup.Add(Type.Knight ,KnightMovementCheckRules);
            rulesGroup.Add(Type.Rook ,RookMovementCheckRules);
            rulesGroup.Add(Type.Bishop ,BishopMovementCheckRules);

            // On cherche le roi
            Piece concernedKing = tempBoard.Squares.OfType<Square>()
                        .First(x => x?.Piece?.Type == Type.King && x?.Piece?.Color == color).Piece;

            bool res = false;
            foreach (KeyValuePair<Type, List<IRule>> rules in rulesGroup)
            {
                List<Square> possibleMoves = new List<Square>();
                concernedKing.Type = rules.Key;
                    possibleMoves = possibleMoves.Concat(rules.Value.First().PossibleMoves(concernedKing)).ToList();
                    rules.Value.ForEach(x => possibleMoves = possibleMoves.Intersect(x.PossibleMoves(concernedKing)).ToList());

                if (possibleMoves.Any(x => x?.Piece?.Type == rules.Key)) // Vérifier si il ne faut pas être d'une couleur différente
                {
                    res = true;
                }
                
            }
            concernedKing.Type = Type.King;
            return res;
        }

        public string Explain() => "Le roi du joueur est en echec";
    }
}