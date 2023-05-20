using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public enum CardCombo
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush
    }

    public class HandCombination
    {
        public readonly CardCombo Combo;
        public readonly PokerCardRank HighestRank;

        public HandCombination(CardCombo combo, PokerCardRank highestRank)
        {
            Combo = combo;
            HighestRank = highestRank;
        }
    }

    public static class CardCombinationHandler
    {
        public static HandCombination CalculateHighestCombination(List<StandardCard> cards)
        {
            if (cards.Count < 5) throw new ArgumentException("There must be at least 5 cards to calculate from");

            PokerCardSuit similarSuit = PokerCardSuit.Clubs;
            PokerCardRank similarRank = PokerCardRank.Two;
            int similarSuits = GetSimilarSuit(cards, out similarSuit);
            int similarRanks = GetSimilarRank(cards, out similarRank);

            // Automatically assuming high card, if no higher combinations are found.
            HandCombination combination = new HandCombination(CardCombo.HighCard, similarRank);

            // Pair
            if (similarSuits == 2) 
                combination = new HandCombination(CardCombo.Pair, similarRank);

            // Two Pair
            if (similarSuits == 2)
            {
                PokerCardSuit secondarySuit = PokerCardSuit.Clubs;
                int secondarySimilarSuits = GetSimilarSuit(cards, out secondarySuit, similarSuit);
                if (secondarySimilarSuits == 2) 
                    combination = new HandCombination(CardCombo.TwoPair, similarRank);
            };

            // Three of a Kind
            if (similarSuits == 3) 
                combination = new HandCombination(CardCombo.ThreeOfAKind, similarRank);

            // Straight


            // Flush


            // Full House
            if (similarSuits == 3)
            {
                PokerCardSuit secondarySuit = PokerCardSuit.Clubs;
                int secondarySimilarSuits = GetSimilarSuit(cards, out secondarySuit, similarSuit);
                if (secondarySimilarSuits == 2) 
                    combination = new HandCombination(CardCombo.TwoPair, similarRank);
            };
            // Four of a Kind
            if (similarSuits == 4) 
                combination = new HandCombination(CardCombo.FourOfAKind, similarRank);

            // Straight Flush


            return combination;
        }

        public static PokerCardRank GetHighestRank(List<StandardCard> cards)
        {
            PokerCardRank currentRank = PokerCardRank.Two;
            foreach (StandardCard card in cards)
            {
                if (currentRank < card.rank) currentRank = card.rank;
            }
            return currentRank;
        }
        public static int GetSimilarRank(List<StandardCard> cards, out PokerCardRank rank, PokerCardRank discludeRank)
        {
            int result = 0;
            int countingRanks = 0;
            PokerCardRank currentRank = PokerCardRank.Two;

            foreach (PokerCardRank _rank in Enum.GetValues(typeof(PokerCardRank)))
            {
                if (_rank == discludeRank) continue;

                currentRank = _rank;
                foreach (StandardCard card in cards)
                {
                    if (_rank == card.rank) countingRanks++;
                    if (countingRanks > result) result = countingRanks;
                }
            }

            rank = currentRank;
            return result;
        }
        public static int GetSimilarSuit(List<StandardCard> cards, out PokerCardSuit suit, PokerCardSuit discludeSuit)
        {
            int result = 0;
            int countingSuits = 0;
            PokerCardSuit currentSuit = PokerCardSuit.Clubs;

            foreach (PokerCardSuit _suit in Enum.GetValues(typeof(PokerCardSuit)))
            {
                if (_suit == discludeSuit) continue;

                currentSuit = _suit;
                foreach (StandardCard card in cards)
                {
                    if (_suit == card.suit) countingSuits++;
                    if (countingSuits > result) result = countingSuits;
                }
            }

            suit = currentSuit;
            return result;
        }
        public static int GetSimilarRank(List<StandardCard> cards, out PokerCardRank rank)
        {
            int result = 0;
            int countingRanks = 0;
            PokerCardRank currentRank = PokerCardRank.Two;

            foreach (PokerCardRank _rank in Enum.GetValues(typeof(PokerCardRank)))
            {
                currentRank = _rank;
                foreach (StandardCard card in cards)
                {
                    if (_rank == card.rank) countingRanks++;
                    if (countingRanks > result) result = countingRanks;
                }
            }

            rank = currentRank;
            return result;
        }
        public static int GetSimilarSuit(List<StandardCard> cards, out PokerCardSuit suit)
        {
            int result = 0;
            int countingSuits = 0;
            PokerCardSuit currentSuit = PokerCardSuit.Clubs;

            foreach (PokerCardSuit _rank in Enum.GetValues(typeof(PokerCardSuit)))
            {
                currentSuit = _rank;
                foreach (StandardCard card in cards)
                {
                    if (_rank == card.suit) countingSuits++;
                    if (countingSuits > result) result = countingSuits;
                }
            }

            suit = currentSuit;
            return result;
        }
    }
}
