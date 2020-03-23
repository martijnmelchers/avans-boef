using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Form;

namespace DomainServices.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> GetBooking(string accessToken);
        Task<List<Accessoire>> GetAvailableAccessoires(string accessToken);
        Task<string> CreateBooking();
        Task SelectDate(string accessToken, DateTime date);
        Task SelectBeestjes(string accessToken, List<int> selectedBeestjes);
        Task LinkAccountToBooking(string accessToken, string userId);
        Task SelectAccessoires(string accessToken, List<int> selectedAccessoires);
        Task SaveContactInfo(string accessToken, ContactInfo contactInfo);
        Task CalculateFinalPrice(string accessToken);
        Task ConfirmBooking(string getAccessToken);
        Task<Booking> GetBookingById(int id);
        Task<List<Booking>> GetBookingByUserId(string userId);
    }
}
