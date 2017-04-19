using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFGame.Data;

namespace EFGame.Domain.Repositories
{
    public class PlayerRepository
    {
        public PlayerRepository()
        {
            _context = new GameBaseContext();
        }
        private readonly GameBaseContext _context;

        public List<Player> GetAllPlayers()
        {
            return _context.Players.ToList();
        }

        public void AddPlayer(Player p)
        {
            if (!_context.Players.Any()) p.PlayerId = 1;
            else
                p.PlayerId = _context.Players.Max(play => play.PlayerId) + 1;
            _context.Players.Add(p);
            _context.SaveChanges();
        }

        public void DeletePlayer(int id)
        {
            var playerToDelete = _context.Players.Find(id);
            if (playerToDelete == null) return;
            _context.Players.Remove(playerToDelete);
            _context.SaveChanges();
        }

        public void UpdateExisting(Player updatedPlayer)
        {
            var playerToUpdate = _context.Players.Find(updatedPlayer.PlayerId);
            if (playerToUpdate == null) return;
            playerToUpdate.Username = updatedPlayer.Username;
            playerToUpdate.Password = updatedPlayer.Password;
            _context.SaveChanges();
        }

    }
}
