using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepBot2
{
    class Deck
    {
        Card[] cards = Enumerable.Range(0, 52).Select(i => new Card(i / 13, i % 13)).ToArray();
        int[] cardOrder;// = Enumerable.Range(0, 52).Select(i => i).ToArray();
        public Deck()
        {
            Reset();
        }

        public void Reset()
        {
            cardOrder = Enumerable.Range(0, 52).Select(i => i).ToArray();
        }

        public string DrawCard()
        {
            Random rand = new Random();
            int r = rand.Next() % 52;
            return cards[r].GetName();
        }
    }
}
