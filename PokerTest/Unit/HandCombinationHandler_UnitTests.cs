using NUnit.Framework;
using Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTest.Unit
{
    internal class HandCombinationHandler_UnitTests
    {
        internal class GetHighestRank
        {
            [Test]
            public void TestSimpleCombinations()
            {
                
            }
        }
        internal class GetSimilarSuit
        {
            [Test]
            public void TestSimpleCombination()
            {
                StandardCard[] cards_with_three_spades =
                {
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Eight),
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Six),
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Four),
                    new StandardCard(PokerCardSuit.Diamonds, PokerCardRank.Jack),
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Queen),
                    new StandardCard(PokerCardSuit.Clubs, PokerCardRank.Two),
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Ace),
                };
                StandardCard[] cards_with_four_diamonds =
                {
                    new StandardCard(PokerCardSuit.Diamonds, PokerCardRank.Eight),
                    new StandardCard(PokerCardSuit.Diamonds, PokerCardRank.Six),
                    new StandardCard(PokerCardSuit.Diamonds, PokerCardRank.Four),
                    new StandardCard(PokerCardSuit.Diamonds, PokerCardRank.Jack),
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Queen),
                    new StandardCard(PokerCardSuit.Clubs, PokerCardRank.Two),
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Ace),
                };
                StandardCard[] cards_with_five_hearts =
                {
                    // Royal-flush
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Ten),
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.King),
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Jack),
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Queen),
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Ace),
                    new StandardCard(PokerCardSuit.Clubs, PokerCardRank.Two),
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Four),
                };

                PokerCardSuit spade;
                PokerCardSuit diamond;
                PokerCardSuit heart;

                int spadeCount = HandCombinationHandler.GetSimilarSuit(cards_with_three_spades.ToList(), out spade);
                int diamondCount = HandCombinationHandler.GetSimilarSuit(cards_with_four_diamonds.ToList(), out diamond);
                int heartCount = HandCombinationHandler.GetSimilarSuit(cards_with_five_hearts.ToList(), out heart);

                // counts from index 0 at the moment.
                Assert.AreEqual(2, spadeCount, $"Expected index count of {2} spades. Got {spadeCount} with resulting suit {spade}");
                Assert.AreEqual(3, diamondCount, $"Expected index count of {3} diamonds. Got {diamondCount} with resulting suit {diamond}");
                // This is what the intellisense is suggesting below, if you can see the green squiggles directly above.
                Assert.That(4, Is.EqualTo(heartCount), $"Expected index count of {4} hearts. Got {heartCount} with resulting suit {heart}");
            }
        }
        internal class GetSimilarRank
        {
            [Test]
            public void TestSimpleCombination()
            {

            }
        }

        internal class CheckForFlush
        {
            [Test]
            public void TestSimpleCombinations()
            {
                StandardCard[] cards_with_a_flush =
                {
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Eight),
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Six),
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Four),
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Jack),
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Queen),
                    new StandardCard(PokerCardSuit.Clubs, PokerCardRank.Two),
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Ace),
                };
                StandardCard[] cards_without_a_flush =
                {
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Eight),
                    new StandardCard(PokerCardSuit.Diamonds, PokerCardRank.Six),
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Four),
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Jack),
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Queen),
                    new StandardCard(PokerCardSuit.Clubs, PokerCardRank.Two),
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Ace),
                };

                PokerCardSuit flushSuit;
                PokerCardSuit nonFlushSuit;

                bool flush_hand = HandCombinationHandler.CheckForFlush(cards_with_a_flush.ToList(), out flushSuit);
                bool non_flush_hand = HandCombinationHandler.CheckForFlush(cards_without_a_flush.ToList(), out nonFlushSuit);

                Assert.IsTrue(flush_hand, $"Checking for a flush, returned {flush_hand} with suit: {flushSuit}");
                Assert.IsFalse(non_flush_hand, $"Checking for a non-flush hand, returned {non_flush_hand} with suit: {nonFlushSuit}");
            }
        }
        internal class CheckForStraight
        {
            [Test]
            public void TestSimpleCombinations()
            {
                StandardCard[] cards_with_a_straight =
                {
                    // The straight cards
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Eight),
                    new StandardCard(PokerCardSuit.Clubs, PokerCardRank.Nine),
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Ten),
                    new StandardCard(PokerCardSuit.Diamonds, PokerCardRank.Jack),
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Queen),
                    // the non-straight cards
                    new StandardCard(PokerCardSuit.Clubs, PokerCardRank.Two),
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Ace),
                };
                StandardCard[] cards_without_a_straight =
                {
                    // The non-straight cards
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Eight),
                    new StandardCard(PokerCardSuit.Clubs, PokerCardRank.Six),
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Four),
                    new StandardCard(PokerCardSuit.Diamonds, PokerCardRank.Jack),
                    new StandardCard(PokerCardSuit.Spades, PokerCardRank.Queen),
                    new StandardCard(PokerCardSuit.Clubs, PokerCardRank.Two),
                    new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Ace),
                };

                bool straight_hand = HandCombinationHandler.CheckForStraight(cards_with_a_straight.ToList());
                bool non_straight_hand = HandCombinationHandler.CheckForStraight(cards_without_a_straight.ToList());

                Assert.IsTrue(straight_hand, $"Checking for a straight, returned {straight_hand}");
                Assert.IsFalse(non_straight_hand, $"Checking for a not-straight hand, returned {non_straight_hand}");
            }

            [Test]
            public void TestManyCombinations()
            {
                Dictionary<int, (StandardCard[], bool)> cardsWithResultsDictionary = new Dictionary<int, (StandardCard[], bool)>
                {
                    { 1, (new StandardCard[] 
                        {
                            new StandardCard(PokerCardSuit.Spades, PokerCardRank.Eight),
                            new StandardCard(PokerCardSuit.Clubs, PokerCardRank.Six),
                            new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Four),
                            new StandardCard(PokerCardSuit.Diamonds, PokerCardRank.Jack),
                            new StandardCard(PokerCardSuit.Spades, PokerCardRank.Queen),
                            new StandardCard(PokerCardSuit.Clubs, PokerCardRank.Two),
                            new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Ace),
                        }, false 
                    )},
                    { 2, (new StandardCard[]
                        {
                            // The straight cards
                            new StandardCard(PokerCardSuit.Spades, PokerCardRank.Eight),
                            new StandardCard(PokerCardSuit.Clubs, PokerCardRank.Nine),
                            new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Ten),
                            new StandardCard(PokerCardSuit.Diamonds, PokerCardRank.Jack),
                            new StandardCard(PokerCardSuit.Spades, PokerCardRank.Queen),
                            // the non-straight cards
                            new StandardCard(PokerCardSuit.Clubs, PokerCardRank.Two),
                            new StandardCard(PokerCardSuit.Hearts, PokerCardRank.Ace),
                        }, true 
                     )},
                };

                foreach (var cards in cardsWithResultsDictionary)
                {
                    bool result = HandCombinationHandler.CheckForStraight(cards.Value.Item1.ToList());
                    Assert.IsTrue(result == cards.Value.Item2, $"Got: {result}, Expected: {cards.Value.Item2}");
                }
            }
        }
    }
}
