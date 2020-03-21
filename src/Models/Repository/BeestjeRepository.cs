using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Repository.Interfaces;

namespace Models.Repository
{
    public class BeestjeRepository : IBeestjeRepository
    {
        private readonly ApplicationDbContext _db;

        public BeestjeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public async Task<Beestje> Get(int id)
        {
            return await _db.Beestjes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Beestje>> GetAll()
        {
            return await _db.Beestjes.ToListAsync();
        }

        public async Task<Beestje> GetWhere(Expression<Func<Beestje, bool>> expression)
        {
            return await _db.Beestjes.FirstOrDefaultAsync(expression);
        }

        public async Task<List<Beestje>> GetAllWhere(Expression<Func<Beestje, bool>> expression)
        {
            return await _db.Beestjes.Where(expression).Include(b => b.BookingBeestjes).ToListAsync();
        }

        public async Task<Beestje> Insert(Beestje item)
        {
            await _db.Beestjes.AddAsync(item);
            await _db.SaveChangesAsync();

            return item;
        }

        public async Task<List<Beestje>> InsertAll(List<Beestje> items)
        {
            await _db.Beestjes.AddRangeAsync(items);
            await _db.SaveChangesAsync();

            return items;
        }

        public async Task Delete(int id)
        {
            _db.Beestjes.Remove(await Get(id));
            await _db.SaveChangesAsync();
        }

        public async Task DeleteWhere(Expression<Func<Beestje, bool>> expression)
        {
            _db.Beestjes.RemoveRange(await GetAllWhere(expression));
            await _db.SaveChangesAsync();
        }

    }
}
