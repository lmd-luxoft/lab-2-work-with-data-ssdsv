namespace Monopoly
{
    internal class Player
    {
        public static readonly Player None = new Player(-1, string.Empty, 0);

        public int Id { get; }
        public string Name { get; }

        public int Money { get; private set; }

        public Player(int id, string name, int money)
        {
            Id = id;
            Name = name;
            Money = money;
        }

        public bool TryChangeMoney(int delta)
        {
            var resultMoney = Money + delta;
            if (resultMoney < 0)
            {
                return false;
            }

            Money = resultMoney;
            return true;
        }

        protected bool Equals(Player other)
        {
            return Id == other.Id && Name == other.Name && Money == other.Money;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Player) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Money;
                return hashCode;
            }
        }
    }
}
