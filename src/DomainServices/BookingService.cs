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
using DayOfWeek = System.DayOfWeek;
using Type = Models.Type;

namespace DomainServices
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBeestjeRepository _beestjeRepository;

        public BookingService(IBookingRepository bookingRepository, IBeestjeRepository beestjeRepository)
        {
            _bookingRepository = bookingRepository;
            _beestjeRepository = beestjeRepository;
        }

        public async Task<string> CreateBooking()
        {
            var booking = await _bookingRepository.Insert(new Booking());

            return booking.AccessToken;
        }

        public async Task SelectDate(string accessToken, DateTime date)
        {
            var booking = await GetBooking(accessToken);

            if(booking == null)
                throw new BookingNotFoundException();
            
            if(DateTime.Now > date)
                throw new InvalidDateException();

            // Set step to one further
            booking.Step = BookingStep.Beestjes;
            booking.Date = date;
        }

        public async Task<Booking> GetBooking(string accessToken)
        {
            var booking = await _bookingRepository.GetByAccessToken(accessToken);
            
            if(booking == null)
                throw new BookingNotFoundException();

            return booking;
        }


        public List<Beestje> GetAllBeestjesByBooking(Booking booking)
        {
            return booking.BookingBeestjes.Select(b => b.Beestje).ToList();
        }

        public async Task SelectBeestjes(string accessToken, List<int> selectedBeestjes)
        {
            var booking = await GetBooking(accessToken);
            
            if(booking == null)
                throw new BookingNotFoundException();

            foreach (var beestjeId in selectedBeestjes)
            {
                var beestje = await _beestjeRepository.Get(beestjeId);
                
                //TODO: Implement validation rules

                booking.BookingBeestjes.Add(new BookingBeestje
                {
                    Booking = booking,
                    Beestje =  beestje
                });
            }

            booking.Step = BookingStep.Accessoires;
        
        }


    }
}
