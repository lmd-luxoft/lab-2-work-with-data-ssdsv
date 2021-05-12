using System.Collections.Generic;

namespace Monopoly
{
    internal class RentPricer
    {
        private readonly PlayerList _playerList;

        private static IDictionary<FieldType, int> _mapRentPrice = new Dictionary<FieldType, int>
        {
            { FieldType.AUTO, 250 },
            { FieldType.FOOD, 250 },
            { FieldType.TRAVEL, 300 },
            { FieldType.CLOTHER, 100 },
            { FieldType.PRISON, 1000 },
            { FieldType.BANK, 700 },
        };
        private static IDictionary<FieldType, int> _mapRentIncome = new Dictionary<FieldType, int>
        {
            { FieldType.AUTO, 250 },
            { FieldType.FOOD, 250 },
            { FieldType.TRAVEL, 300 },
            { FieldType.CLOTHER, 1000 },
        };

        private static ISet<FieldType> _fieldsWithOwner = new HashSet<FieldType>
        {
            FieldType.AUTO,
            FieldType.FOOD,
            FieldType.TRAVEL,
            FieldType.CLOTHER
        };

        public RentPricer(PlayerList playerList)
        {
            _playerList = playerList;
        }

        public bool TryToRent(Field field, Player guest)
        {
            var fieldMustHaveOwner = _fieldsWithOwner.Contains(field.FieldType);
            var fieldDoesNotHaveOwner = !field.IsOwned();
            if (fieldMustHaveOwner && fieldDoesNotHaveOwner)
                return false;

            int rent;

            if (!_mapRentPrice.TryGetValue(field.FieldType, out rent))
                return false;

            if (!guest.TryChangeMoney(-rent))
                return false;

            int income;
            var owner = _playerList.GetById(field.OwnerId);

            if (_mapRentIncome.TryGetValue(field.FieldType, out income))
                return owner.TryChangeMoney(income);


            return false;
        }
    }
}