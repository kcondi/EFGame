using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EFGame.Data;

namespace EFGame.Domain.Repositories
{
    public class MissionRepository
    {
        public MissionRepository()
        {
            _context = new GameBaseContext();
        }

        private readonly GameBaseContext _context;

        public void AddMission(Mission ms, string name)
        {
            if (!_context.Missions.Any()) ms.MissionId = 1;
            else
                ms.MissionId = _context.Missions.Max(m => m.MissionId) + 1;
            ms.Match = _context.Matches.ToList().Find(a => a.Name.Equals(name, StringComparison.Ordinal));
            _context.Missions.Add(ms);
            _context.SaveChanges();
        }

        public void DeleteMission(int id)
        {
            var missionToDelete = _context.Missions.Find(id);
            if (missionToDelete == null) return;
            _context.Missions.Remove(missionToDelete);
            _context.SaveChanges();
        }
    }
}