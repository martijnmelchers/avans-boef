using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DomainServices.Interfaces
{
    public interface IBookingService
    {
        Task<string> CreateBooking();
        Task SelectDate(string accessToken, DateTime date);
        Task<Booking> GetBooking(string accessToken);
        Task<List<Beestje>> GetBeestjesByBooking(Booking booking);
    }
}
