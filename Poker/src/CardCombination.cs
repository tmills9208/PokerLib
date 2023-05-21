using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

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
        public readonly PokerCardRank[] Ranks;
        public readonly PokerCardSuit[] Suits;

        #region Constructors
        public HandCombination(CardCombo combo, PokerCardRank rank, PokerCardSuit suit)
        {
            Combo = combo;
            Ranks = new PokerCardRank[] { rank };
            Suits = new PokerCardSuit[] { suit };
        }
        public HandCombination(CardCombo combo, PokerCardRank[] ranks, PokerCardSuit suit)
        {
            Combo = combo;
            Ranks = ranks;
            Suits = new PokerCardSuit[] { suit };
        }
        public HandCombination(CardCombo combo, PokerCardRank rank, PokerCardSuit[] suits)
        {
            Combo = combo;
            Ranks = new PokerCardRank[] { rank };
            Suits = suits;
        }
        public HandCombination(CardCombo combo, PokerCardRank[] ranks, PokerCardSuit[] suits)
        {
            Combo = combo;
            Ranks = ranks;
            Suits = suits;
        }
        #endregion
    }

    public static class HandCombinationHandler
    {
        public static HandCombination CalculateHighestCombination(List<StandardCard> cards)
        {
            if (cards.Count < 5) throw new ArgumentException("There must be at least 5 cards to calculate from");

            PokerCardSuit similarSuit;
            PokerCardRank similarRank;
            int similarSuits = GetSimilarSuit(cards, out similarSuit);
            int similarRanks = GetSimilarRank(cards, out similarRank);

            PokerCardSuit suitWithFlush;
            bool straight = CheckForStraight(cards);
            bool flush = CheckForFlush(cards, out suitWithFlush);

            // Automatically assuming high card, if no higher combinations are found.
            HandCombination combination = new HandCombination(CardCombo.HighCard, similarRank, similarSuit);

            // Pair
            if (similarRanks == 1)
                combination = new HandCombination(CardCombo.Pair, similarRank, similarSuit);

            // Two Pair
            if (similarRanks == 1)
            {
                PokerCardRank secondaryRank = PokerCardRank.Two;
                int secondarySimilarRanks = GetSimilarRank(cards, out secondaryRank, similarRank);
                if (secondarySimilarRanks == 1)
                    combination = new HandCombination(CardCombo.TwoPair, similarRank, similarSuit);
            };

            // Three of a Kind
            if (similarRanks == 2)
                combination = new HandCombination(CardCombo.ThreeOfAKind, similarRank, similarSuit);

            // Straight
            if (straight)
                combination = new HandCombination(CardCombo.Straight, similarRank, similarSuit);

            // Flush
            if (flush)
                combination = new HandCombination(CardCombo.Flush, similarRank, suitWithFlush);

            // Full House
            if (similarRanks == 3)
            {
                PokerCardRank secondaryRank;
                int secondarySimilarRanks = GetSimilarRank(cards, out secondaryRank, similarRank);
                if (secondarySimilarRanks == 2)
                    combination = new HandCombination(CardCombo.FullHouse, new PokerCardRank[] { similarRank, secondaryRank }, similarSuit);
            };

            // Four of a Kind
            if (similarRanks == 4)
                combination = new HandCombination(CardCombo.FourOfAKind, similarRank, similarSuit);

            // Straight Flush



            return combination;
        }

        public static bool CheckForFlush(List<StandardCard> cards, out PokerCardSuit suit)
        {
            int suitCount = GetSimilarSuit(cards, out suit);
            return suitCount >= 4;
        }

        public static bool CheckForStraight(List<StandardCard> cards)
        {
            bool result = false;
            var cardsSortedByRank = cards;
            cardsSortedByRank.Sort((a, b) => a.rank - b.rank);

            int rankCount = 0;
            PokerCardRank lastRank = PokerCardRank.Two;
            for (int i = 0; i < cards.Count; i++)
            {
                if (cardsSortedByRank[i].rank < lastRank) rankCount = 0;
                if (cardsSortedByRank[i].rank == ++lastRank) rankCount++;
                if (rankCount >= 4) break;
                lastRank = cards[i].rank;
            }
            result = rankCount >= 4;

            return result;
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
            List<StandardCard> cardsSortedBySuit = cards;
            cardsSortedBySuit.Sort((a, b) => a.suit - b.suit);

            foreach (PokerCardSuit _suit in Enum.GetValues(typeof(PokerCardSuit)))
            {
                currentSuit = _suit;
                foreach (StandardCard card in cardsSortedBySuit)
                {
                    if (_suit == card.suit) countingSuits++;
                    if (_suit != card.suit) { 
                        countingSuits = 0; 
                        currentSuit = card.suit; 
                    }
                    if (countingSuits > result) result = countingSuits;
                }
            }

            suit = currentSuit;
            return result;
        }
    }
}
