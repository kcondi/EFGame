using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EFGame.Data;

namespace EFGame.Domain.Repositories
{
    public class MatchRepository
    {
        public MatchRepository()
        {
            _context = new GameBaseContext();
        }
        private readonly GameBaseContext _context;
        public List<Match> GetAllMatches()
        {
            return _context.Matches.ToList();
        }

        public void AddMatch(Match m, List<int> indexes)
        {
            if (!_context.Matches.Any()) m.MatchId = 1;
            else
                m.MatchId = _context.Matches.Max(mat => mat.MatchId) + 1;
            Player playerToAdd;
            foreach (int id in indexes)
            {
                playerToAdd = _context.Players.Find(id);
                m.Players.Add(playerToAdd);
            }
            _context.Matches.Add(m);
            _context.SaveChanges();
        }

        public void DeleteMatch(int id)
        {
            var matchToDelete = _context.Matches.Find(id);
            if (matchToDelete == null) return;
            _context.Matches.Remove(matchToDelete);
            _context.SaveChanges();
        }



    }
}
