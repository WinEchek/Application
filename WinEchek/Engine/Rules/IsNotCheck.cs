using System.Collections.Generic;
using System.Linq;
using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Rules {
    public class IsNotCheck : IRule
    {
        public bool IsMoveValid(Move move)
        {
            /*
             * On construit des groupes de règles spéciales qui ne tienne pas compte
             * de celle de la mise en echec
             */
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

            List<List<IRule>> rulesGroup = new List<List<IRule>>();
            rulesGroup.Add(QueenMovementCheckRules);
            rulesGroup.Add(PawnMovementCheckRules);
            rulesGroup.Add(KingMovementCheckRules);
            rulesGroup.Add(KnightMovementCheckRules);
            rulesGroup.Add(RookMovementCheckRules);
            rulesGroup.Add(BishopMovementCheckRules);

            Piece concernedKing;
            if (move.Piece.Type == Type.King)
            {
                concernedKing = move.Piece;
            }
            else
            {
                /**
                 * On cherche notre roi...
                 */
                concernedKing =
                    move.Piece.Square.Board.Squares.OfType<Square>()
                        .First(x => x?.Piece?.Type == Type.King && x?.Piece?.Color == move.Piece.Color).Piece;
            }










        }

        public List<Square> PossibleMoves(Piece piece)
        {
            throw new System.NotImplementedException();
        }
    }
}