using System.Collections;

namespace Poker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StandardCardDeck cardSystem = new StandardCardDeck();
            foreach (var card in cardSystem.Cards)
            {
                Console.WriteLine(card.ToString());
            }
            Console.ReadLine();
        }
    }
}