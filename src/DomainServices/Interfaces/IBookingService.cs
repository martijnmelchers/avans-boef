using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace DomainServices.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> GetBooking(string accessToken);
        List<Beestje> GetAllBeestjesByBooking(Booking booking);
        Task<List<Accessoire>> GetAvailableAccessoires(string accessToken);
        
        Task<string> CreateBooking();
        Task SelectDate(string accessToken, DateTime date);
        Task SelectBeestjes(string accessToken, List<int> selectedBeestjes);
        Task LinkAccountToBooking(string accessToken, string userId);
        Task SelectAccessoires(string accessToken, List<int> selectedAccessoires);
    }
}
