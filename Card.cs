using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepBot2
{
    class Card
    {

        static string[] suits = { "Clubs", "Hearts", "Diamonds", "Spades", };
        static string[] levels = { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace" };

        public Card(int suit, int lev)
        {
            suitValue = suit;
            levValue = lev;
        }

        int suitValue;
        int levValue;
        public string GetName()
        {
            return levels[levValue] + " of " + suits[suitValue];
        }

    }
}
