using System.Collections.Generic;
using System.Linq;
using CardGames.Enums;

namespace CardGames.Detectors
{
    public class ThreeOfAKindDectector : IDetect
    {
        public FinalHand Detect(IEnumerable<Card> availableCards)
        {
            var finalHand = new FinalHand();
            var rankableCards = new List<Card>(availableCards);

            var mostOccurringCardValue =
                rankableCards.GroupBy(i => i.NumericalValue)
                    .OrderByDescending(grp => grp.Count())
                    .Select(grp => grp.Key)
                    .First();

            var numberOfOccurances = rankableCards.Count(num => num.NumericalValue == mostOccurringCardValue);

            if (numberOfOccurances == 3)
            {
                var threeOfAKindCards = rankableCards.Where(num => num.NumericalValue == mostOccurringCardValue).ToList();
                finalHand.card1 = threeOfAKindCards[0];
                finalHand.card2 = threeOfAKindCards[1];
                finalHand.card3 = threeOfAKindCards[2];

                rankableCards.RemoveAll(num => num.NumericalValue == mostOccurringCardValue);

                finalHand.card4 = rankableCards[3];
                finalHand.card5 = rankableCards[2];
                finalHand.rank = HandRanking.ThreeOfAKind;
            }
          
            return finalHand;
        }
    }
}