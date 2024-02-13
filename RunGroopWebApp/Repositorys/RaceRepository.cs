using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Repositorys
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext context;

        public RaceRepository(ApplicationDbContext context) 
        {
            this.context = context;
        }
        public async Task<IEnumerable<Race>> GetAll()
        {
            return await context.Races.ToListAsync();
        }
        public async Task<Race> GetByIdAsync(int id)
        {
            return await context.Races.Include(c => c.Address).FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<Race> GetByIdAsyncNoTracking(int id)
        {
            return await context.Races.Include(c => c.Address).AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<IEnumerable<Race>> GetRaceByCity(string city)
        {
            return await context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }
        public bool Add(Race club)
        {
            context.Races.Add(club);
            return Save();
        }

        public bool Delete(Race club)
        {
            context.Races.Remove(club);
            return Save();
        }

        public bool Update(Race club)
        {
            context.Races.Update(club);
            return Save();
        }

        public bool Save()
        {
            var save = context.SaveChanges();
            return save > 0 ? true : false;
        }

        
    }
}
