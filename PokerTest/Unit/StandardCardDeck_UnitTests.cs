using NUnit.Framework;
using Poker;

namespace PokerTest.Unit
{
    public class StandardCardDeck_UnitTests
    {
        StandardCardDeck cardDeck;

        [SetUp]
        public void Setup()
        {
            cardDeck = new StandardCardDeck();
        }

        [Test]
        public void ShouldInitiallyHave52Cards()
        {
            Assert.IsTrue(cardDeck.CurrentDeck.Any(), "The card deck should have any unused cards waiting");
            Assert.IsTrue(cardDeck.CurrentDeck.Count == 52, $"Counted {cardDeck.CurrentDeck.Count} out of 52 cards.");
        }

        [Test]
        public void ShouldShuffle()
        {
            var previousCards = cardDeck.Cards;
            cardDeck.ShuffleDeck();
            int cardCount = previousCards.Intersect(cardDeck.CurrentDeck).Count();

            Assert.IsTrue(cardDeck.Cards.Count == 52, "Still counting 52 cards...");
            Assert.IsTrue(cardDeck.Cards.Count == cardCount, $"Counted {cardCount} out of 52 shuffled cards.");
        }
    }
}