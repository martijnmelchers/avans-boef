using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Repository.Interfaces;

namespace Models.Repository
{
    public class AccessoireRepository : IAccessoireRepository
    {
        private readonly ApplicationDbContext _db;

        public AccessoireRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Accessoire> Get(int id)
        {
            return await _db.Accessoires.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Accessoire>> GetAll()
        {
            return await _db.Accessoires.ToListAsync();
        }

        public async Task<Accessoire> GetWhere(Expression<Func<Accessoire, bool>> expression)
        {
            return await _db.Accessoires.FirstOrDefaultAsync(expression);
        }

        public async Task<List<Accessoire>> GetAllWhere(Expression<Func<Accessoire, bool>> expression)
        {
            return await _db.Accessoires.Where(expression).ToListAsync();
        }

        public async Task<Accessoire> Insert(Accessoire item)
        {
            await _db.Accessoires.AddAsync(item);
            await _db.SaveChangesAsync();

            return item;
        }

        public async Task<List<Accessoire>> InsertAll(List<Accessoire> items)
        {
            await _db.Accessoires.AddRangeAsync(items);
            await _db.SaveChangesAsync();

            return items;
        }

        public async Task Delete(int id)
        {
            _db.Accessoires.Remove(await Get(id));
            await _db.SaveChangesAsync();
        }

        public async Task DeleteWhere(Expression<Func<Accessoire, bool>> expression)
        {
            _db.Accessoires.RemoveRange(await GetAllWhere(expression));
            await _db.SaveChangesAsync();
        }


    }
}
