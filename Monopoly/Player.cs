namespace Monopoly
{
    internal class Player
    {
        public int Id { get; }
        public string Name { get; }

        public int Money { get; private set; }

        public Player(int id, string name, int money)
        {
            Id = id;
            Name = name;
            Money = money;
        }

        public void ChangeMoney(int delta)
        {
            Money += delta;
        }

    }
}
