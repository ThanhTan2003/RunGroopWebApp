using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Repositorys
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext context;

        public static int Index = 0;
        public ClubRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Club>> GetAll()
        {
            return await context.Clubs.ToListAsync();
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            return await context.Clubs.Include(c => c.Address).FirstOrDefaultAsync(x => x.Id == id); ;
        }

        public async Task<Club> GetByIdAsyncNoTracking(int id)
        {
            return await context.Clubs.Include(c => c.Address).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }
        public bool Add(Club club)
        {
            context.Add(club);
            return Save();
        }
        public bool Update(Club club)
        {
            context.Update(club);
            return Save();
        }
        public bool Delete(Club club)
        {
            context.Remove(club);
            return Save();
        }
        public bool Save()
        {
            var save = context.SaveChanges();
            return save > 0 ? true : false;
        }
    }
}
