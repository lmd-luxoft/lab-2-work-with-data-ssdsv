using System.Collections.Generic;

namespace Monopoly
{
    partial class Monopoly
    {
        private const int InitialMoney = 6000;

        public readonly FieldList fieldList = new FieldList();
        private readonly PlayerList playerList = new PlayerList();

        private readonly BuyPricer buyPricer;
        private readonly RentPricer rentPricer;

        public Monopoly(string[] p)
        {
            var playerCount = p.Length;
            
            for (int i = 0; i < playerCount; i++)
            {
                playerList.Add(i + 1, p[i], InitialMoney);
            }

            buyPricer = new BuyPricer();
            rentPricer = new RentPricer(playerList);

            fieldList.Add("Ford", FieldType.AUTO);
            fieldList.Add("MCDonald", FieldType.FOOD);
            fieldList.Add("Lamoda", FieldType.CLOTHER);
            fieldList.Add("Air Baltic", FieldType.TRAVEL);
            fieldList.Add("Nordavia", FieldType.TRAVEL);
            fieldList.Add("Prison", FieldType.PRISON);
            fieldList.Add("MCDonald", FieldType.FOOD);
            fieldList.Add("DeutscheBank", FieldType.BANK);
            fieldList.Add("TESLA", FieldType.AUTO);
        }

        internal IReadOnlyCollection<Player> GetPlayersList()
        {
            return playerList.GetAll();
        }

        internal IReadOnlyCollection<Field> GetFieldsList()
        {
            return fieldList.GetAll();
        }

        internal Field GetFieldByName(string v)
        {
            return fieldList.GetByName(v);
        }

        internal bool Buy(int buyerId, Field field)
        {
            if (field.IsOwned())
                return false;

            var buyer = GetPlayerInfo(buyerId);

            if (!buyPricer.TryToBuy(field.FieldType, buyer))
                return false;

            field.SetOwner(buyer.Id);

            return true;
        }

        internal Player GetPlayerInfo(int playerId)
        {
            return playerList.GetById(playerId);
        }

        internal bool Renta(int guestId, Field field)
        {
            var guest = GetPlayerInfo(guestId);

            return rentPricer.TryToRent(field, guest);
        }
    }
}
