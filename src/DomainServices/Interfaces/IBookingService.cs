using System;
using System.Threading.Tasks;
using Models;

namespace DomainServices.Interfaces
{
    public interface IBookingService
    {
        Task<string> CreateBooking();
        Task SelectDate(string accessToken, DateTime date);
        Task<Booking> GetBooking(string get);
    }
}