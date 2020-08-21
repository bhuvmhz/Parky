using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Extensions;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository 
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _db;

        public TrailRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateTrail(Trail trail)
        {
            _db.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _db.Trails.Remove(trail);
            return Save();
        }

        public Trail GetTrail(int trailId)
        {
            return _db.Trails.Include(c => c.NationalPark).FirstOrDefault(x => x.Id == trailId);
        }

        public ICollection<Trail> GetTrails()
        {
            return _db.Trails.Include(c => c.NationalPark).OrderBy(a => a.Name).ToList();
        }

        public bool TrailExists(string name)
        {
            bool value = _db.Trails.Any(x => x.Name.TrimAndLower() == name.TrimAndLower());
            return value;
        }

        public bool TrailExists(int TrailId)
        {
            return _db.Trails.Any(x => x.Id == TrailId);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public bool UpdateTrail(Trail Trail)
        {
            _db.Trails.Update(Trail);
            return Save();
        }

        public ICollection<Trail> GetTrailsInNationalPark(int nationalParkId)
        {
            return _db.Trails.Include(c => c.NationalPark).Where(c => c.NationalParkId == nationalParkId).ToList();
        }
    }
}
