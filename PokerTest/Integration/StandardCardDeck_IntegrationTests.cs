using NUnit.Framework;
using Poker;

namespace PokerTest.Integration
{
    internal class StandardCardDeck_IntegrationTests
    {
        internal class With_Players
        {
            StandardCardDeck cardDeck;
            Player[] players;

            [SetUp]
            public void Setup()
            {
                cardDeck = new StandardCardDeck();
                cardDeck.ShuffleDeck();

                players = new Player[2]
                {
                new Player("Player1", 400),
                new Player("Player2", 400)
                };
                cardDeck.DealCardsToPlayers(players);
            }

            [Test]
            public void ShouldHaveTwoCards()
            {
                var cardDeckCount = cardDeck.CurrentDeck.Count;
                foreach (Player player in players)
                {
                    var name = player.Name;
                    var playerCardCount = player.HeldCards.Count;

                    Assert.IsTrue(player.HeldCards.Any(), $"{name} should have cards");
                    Assert.IsTrue(player.HeldCards.Count == 2, $"{name} has {playerCardCount} out of 2 cards");
                    Assert.IsTrue(cardDeck.DrawnCards.Count > 0, $"The card deck currently has {cardDeckCount} cards");
                }
            }

            [Test]
            public void ShouldHaveDifferentCardsPerPlayer()
            {
                var player1 = players[0];
                var player2 = players[1];
                Assert.IsFalse(player1.HeldCards.Intersect(player2.HeldCards).ToList().Any());
            }
        }
    }
}
