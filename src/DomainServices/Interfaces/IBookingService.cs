using System;
using System.Threading.Tasks;

namespace DomainServices.Interfaces
{
    public interface IBookingService
    {
        Task<string> CreateBooking();
        Task SelectDate(string accessToken, DateTime date);
    }
}