using System.Collections.Generic;

namespace Monopoly
{
    internal class BuyPricer
    {

        private static IDictionary<FieldType, int> mapBuyPrice = new Dictionary<FieldType, int>
        {
            { FieldType.AUTO, 500 },
            { FieldType.FOOD, 250 },
            { FieldType.TRAVEL, 700 },
            { FieldType.CLOTHER, 100 },
        };

        public BuyPricer()
        {
        }

        public bool TryToBuy(FieldType fieldType, Player buyer)
        {
            if (mapBuyPrice.TryGetValue(fieldType, out int price))
            {
                return buyer.TryChangeMoney(-price);
            }

            return false;
        }

    }
}
