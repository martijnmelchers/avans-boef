using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async Task<Booking> GetBooking(string accessToken)
        {
            return await _bookingRepository.GetByAccessToken(accessToken);
        }

        public async Task<List<Beestje>> GetBeestjesByBooking(Booking booking)
        {
            return booking.BookingBeestjes.Select(bb => bb.Beestje).ToList();
        }
    }
}
