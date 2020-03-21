using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Models.Repository.Interfaces
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<Booking> GetByAccessToken(string accessToken);
        Task<List<BookingBeestje>> GetBookingsByBeestje(Beestje beestje);
        Task<List<BookingBeestje>> GetBookingBeestjeByDate(DateTime date);

    }
}
