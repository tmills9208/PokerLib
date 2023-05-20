using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public enum PokerCardSuit
    {
        Clubs,
        Spades,
        Hearts,
        Diamonds
    }
    public enum PokerCardRank
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public class StandardCard
    {
        public PokerCardSuit suit;
        public PokerCardRank rank;

        public StandardCard(PokerCardSuit _suit, PokerCardRank _rank)
        {
            suit = _suit;
            rank = _rank;
        }

        public int GetRankIndex()
        {
            int result = 0;

            foreach (PokerCardRank _rank in Enum.GetValues<PokerCardRank>())
            {
                if (rank == _rank) break;
                result++;
            }
            return result;
        }

        public int GetSuitIndex()
        {
            int result = 0;

            foreach (PokerCardSuit _suit in Enum.GetValues<PokerCardSuit>())
            {
                if (suit == _suit) break;
                result++;
            }
            return result;
        }

        public static int GetRankIndex(PokerCardRank _matchingRank)
        {
            int result = 0;

            foreach (PokerCardRank _rank in Enum.GetValues<PokerCardRank>())
            {
                if (_matchingRank == _rank) break;
                result++;
            }
            return result;
        }

        public static int GetSuitIndex(PokerCardSuit _matchingSuit)
        {
            int result = 0;

            foreach (PokerCardSuit _suit in Enum.GetValues<PokerCardSuit>())
            {
                if (_matchingSuit == _suit) break;
                result++;
            }
            return result;
        }

        public override string ToString()
        {
            return $"{rank} of {suit}";
        }
    }
}
