using System;
using System.IO;
using System.Threading.Tasks;
using DomainServices.Interfaces;
using Models;
using Models.Exceptions;
using Models.Repository;
using Models.Repository.Interfaces;

namespace DomainServices
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<string> CreateBooking()
        {
            var booking = await _bookingRepository.Insert(new Booking());

            return booking.AccessToken;
        }

        public async Task SelectDate(string accessToken, DateTime date)
        {
            var booking = await _bookingRepository.GetByAccessToken(accessToken);

            if(booking == null)
                throw new BookingNotFoundException();
            
            if(DateTime.Now > date)
                throw new InvalidDateException();
            
            booking.Date = date;
        }
    }
}
