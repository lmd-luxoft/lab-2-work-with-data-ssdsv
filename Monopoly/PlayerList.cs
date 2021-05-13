using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    internal class PlayerList
    {
        private IDictionary<int, Player> listPlayers = new Dictionary<int, Player>();

        public PlayerList()
        {
        }

        public void Add(int id, string name, int initialMoney)
        {
            listPlayers.Add(id, new Player(id, name, initialMoney));
        }

        public IReadOnlyCollection<Player> GetAll() => listPlayers.Values.ToList().AsReadOnly();

        public Player GetById(int id)
        {
            if (listPlayers.TryGetValue(id, out Player player))
            {
                return player;
            }

            return Player.None;
        }
    }
}
