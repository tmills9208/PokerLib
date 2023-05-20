using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class StandardCardDeck
    {
        private const int CARD_LIMIT = 52;
        // Cards to keep track of
        public readonly List<StandardCard> Cards;
        // Cards in the deck.
        public List<StandardCard> CurrentDeck { get; private set; }
        // Cards being used currently, for whatever reason. Players, The dealer, etc.
        public Hashtable DrawnCards { get; private set; }

        public StandardCardDeck()
        {
            Cards = new List<StandardCard>();
            GenerateDeck();
        }

        public void ReturnCardsFromPlayers(Player[] players)
        {
            foreach (var player in players)
            {
                player.RemoveCards();
            }
            DrawnCards.Clear();
            GenerateDeck();
        }

        public void DealCardsToPlayers(Player[] players)
        {
            if (players.Length < 2) return;

            for (int i = 0; i < players.Length * 2; i++)
            {
                int playerIndex = i % players.Length;
                StandardCard topCard = CurrentDeck.First();
                players.ToArray()[playerIndex].GiveCard(topCard);
                DrawnCards.Add(topCard, players[playerIndex]);
                CurrentDeck.Remove(topCard);
            }
        }

        private void GenerateDeck()
        {
            for (int i = 0; i < CARD_LIMIT; i++)
            {
                PokerCardSuit suit = Enum.GetValues<PokerCardSuit>()[i % 4];
                PokerCardRank rank = Enum.GetValues<PokerCardRank>()[i / 4];
                StandardCard card = new StandardCard(suit, rank);
                Cards.Add(card);
            }
            DrawnCards = new Hashtable();
            CurrentDeck = Cards;
        }

        public void ShuffleDeck()
        {
            var rng = new Random();
            // Quick solution to randomly order objects within an enumerable.
            var shuffledCards = CurrentDeck.OrderBy(card => rng.Next()).ToList();

            CurrentDeck = shuffledCards.ToList();
        }
    }
}
