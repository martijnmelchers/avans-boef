using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Repository.Interfaces;

namespace Models.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _db;

        public BookingRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Booking> Get(int id)
        {
            return await _db.Bookings
                .Include(x => x.BookingBeestjes)
                .ThenInclude(x => x.Beestje)
                .Include(x => x.BookingAccessoires)
                .ThenInclude(x => x.Accessoire)
                .Include(x => x.Discounts)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Booking>> GetAll()
        {
            return await _db.Bookings.ToListAsync();
        }

        public async Task<Booking> GetWhere(Expression<Func<Booking, bool>> expression)
        {
            return await _db.Bookings.FirstOrDefaultAsync(expression);
        }

        public async Task<List<Booking>> GetAllWhere(Expression<Func<Booking, bool>> expression)
        {
            return await _db.Bookings.Where(expression).ToListAsync();
        }

        public async Task<Booking> Insert(Booking item)
        {
            await _db.Bookings.AddAsync(item);
            await _db.SaveChangesAsync();

            return item;
        }

        public async Task<List<Booking>> InsertAll(List<Booking> items)
        {
            await _db.Bookings.AddRangeAsync(items);
            await _db.SaveChangesAsync();

            return items;
        }

        public async Task Delete(int id)
        {
            _db.Bookings.Remove(await Get(id));
            await _db.SaveChangesAsync();
        }

        public async Task DeleteWhere(Expression<Func<Booking, bool>> expression)
        {
            _db.Bookings.RemoveRange(await GetAllWhere(expression));
            await _db.SaveChangesAsync();
        }

        public async Task<Booking> GetByAccessToken(string accessToken)
        {
            return await _db.Bookings
                .Include(x => x.BookingBeestjes)
                .ThenInclude(x => x.Beestje)
                .Include(x => x.BookingAccessoires)
                .ThenInclude(x => x.Accessoire)
                .Include(x => x.Discounts)
                .FirstOrDefaultAsync(x => x.AccessToken == accessToken);
        }
    }
}