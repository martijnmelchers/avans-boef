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
        Task SelectBeestjes(string accessToken, List<int> selectedBeestjes);

        List<Beestje> GetAllBeestjesByBooking(Booking booking);
        Task<List<Accessoire>> GetAvailableAccessoires(string accessToken);
    }
}
