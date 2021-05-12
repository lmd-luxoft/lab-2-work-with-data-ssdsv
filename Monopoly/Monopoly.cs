using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    partial class Monopoly
    {
        private const int InitialMoney = 6000;

        public List<Player> players = new List<Player>();
        public List<Field> fields = new List<Field>();

        public Monopoly(string[] p, int v)
        {
            for (int i = 1; i <= v; i++)
            {
                players.Add(new Player(i, p[i], InitialMoney));
            }
            fields.Add(new Field("Ford", FieldType.AUTO));
            fields.Add(new Field("MCDonald", FieldType.FOOD));
            fields.Add(new Field("Lamoda", FieldType.CLOTHER));
            fields.Add(new Field("Air Baltic", FieldType.TRAVEL));
            fields.Add(new Field("Nordavia", FieldType.TRAVEL));
            fields.Add(new Field("Prison", FieldType.PRISON));
            fields.Add(new Field("MCDonald", FieldType.FOOD));
            fields.Add(new Field("TESLA", FieldType.AUTO));
        }

        internal IReadOnlyCollection<Player> GetPlayersList()
        {
            return players;
        }

        internal IReadOnlyCollection<Field> GetFieldsList()
        {
            return fields;
        }

        internal Field GetFieldByName(string v)
        {
            return (from p in fields where p.Name == v select p).FirstOrDefault();
        }

        internal bool Buy(int buyerId, Field field)
        {
            var buyer = GetPlayerInfo(buyerId);
            switch (field.FieldType)
            {
                case FieldType.AUTO:
                    if (field.OwnerId != 0)
                        return false;
                    buyer.ChangeMoney(-500);
                    break;
                case FieldType.FOOD:
                    if (field.OwnerId != 0)
                        return false;
                    buyer.ChangeMoney(-250);
                    break;
                case FieldType.TRAVEL:
                    if (field.OwnerId != 0)
                        return false;
                    buyer.ChangeMoney(-700);
                    break;
                case FieldType.CLOTHER:
                    if (field.OwnerId != 0)
                        return false;
                    buyer.ChangeMoney(-100);
                    break;
                default:
                    return false;
            }
            int i = players.Select((item, index) => new { name = item.Name, index = index })
                .Where(n => n.name == buyer.Name)
                .Select(p => p.index).FirstOrDefault();
            fields[i] = new Field(field.Name, field.Item2, buyerId, field.Item4);
            field.SetOwner(buyerId);
            return true;
        }

        internal Player GetPlayerInfo(int v)
        {
            return players[v - 1];
        }

        internal bool Renta(int v, Field k)
        {
            var guest = GetPlayerInfo(v);
            Tuple<string, int> owner = null;
            switch (k.FieldType)
            {
                case FieldType.AUTO:
                    if (k.OwnerId == 0)
                        return false;
                    owner = GetPlayerInfo(k.OwnerId);
                    guest = new Player(guest.Name, guest.Item2 - 250);
                    owner = new Player(owner.Name, owner.Item2 + 250);
                    break;
                case FieldType.FOOD:
                    if (k.OwnerId == 0)
                        return false;
                    owner = GetPlayerInfo(k.OwnerId);
                    guest = new Player(guest.Name, guest.Item2 - 250);
                    owner = new Player(owner.Name, owner.Item2 + 250);

                    break;
                case FieldType.TRAVEL:
                    if (k.OwnerId == 0)
                        return false;
                    owner = GetPlayerInfo(k.OwnerId);
                    guest = new Player(guest.Name, guest.Item2 - 300);
                    owner = new Player(owner.Name, owner.Item2 + 300);
                    break;
                case FieldType.CLOTHER:
                    if (k.OwnerId == 0)
                        return false;
                    owner = GetPlayerInfo(k.OwnerId);
                    guest = new Player(guest.Name, guest.Item2 - 100);
                    owner = new Player(owner.Name, owner.Item2 + 1000);

                    break;
                case FieldType.PRISON:
                    guest = new Player(guest.Name, guest.Item2 - 1000);
                    break;
                case FieldType.BANK:
                    guest = new Player(guest.Name, guest.Item2 - 700);
                    break;
                default:
                    return false;
            }
            players[v - 1] = guest;
            if (owner != null)
                players[k.OwnerId - 1] = owner;
            return true;
        }
    }
}
