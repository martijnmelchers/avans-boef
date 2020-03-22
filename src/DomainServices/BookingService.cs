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
        private readonly IAccessoireRepository _accessoireRepository;

        public BookingService(IBookingRepository bookingRepository, IBeestjeRepository beestjeRepository,
            IAccessoireRepository accessoireRepository)
        {
            _bookingRepository = bookingRepository;
            _beestjeRepository = beestjeRepository;
            _accessoireRepository = accessoireRepository;
        }

        public async Task<string> CreateBooking()
        {
            var booking = await _bookingRepository.Insert(new Booking());

            return booking.AccessToken;
        }

        public async Task SelectDate(string accessToken, DateTime date)
        {
            var booking = await GetBooking(accessToken);

            if (DateTime.Now > date)
                throw new InvalidDateException();

            // Clear the accessories, discounts and beestjes so the booking is essentially reset
            booking.BookingBeestjes.Clear();
            booking.BookingAccessoires.Clear();
            booking.Discounts.Clear();

            // Set step to one further
            booking.Step = BookingStep.Beestjes;
            booking.Date = date;
        }

        public async Task<Booking> GetBooking(string accessToken)
        {
            var booking = await _bookingRepository.GetByAccessToken(accessToken);

            if (booking == null)
                throw new BookingNotFoundException();

            return booking;
        }


        public List<Beestje> GetAllBeestjesByBooking(Booking booking)
        {
            return booking.BookingBeestjes.Select(b => b.Beestje).ToList();
        }

        public async Task LinkAccountToBooking(string accessToken, string userId)
        {
            var booking = await GetBooking(accessToken);

            booking.Step = BookingStep.PersonalData;
            booking.UserId = userId;
        }

        public async Task SelectBeestjes(string accessToken, List<int> selectedBeestjes)
        {
            var booking = await GetBooking(accessToken);

            foreach (var bookingBeestje in booking.BookingBeestjes.ToList())
            {
                if (!selectedBeestjes.Contains(bookingBeestje.BeestjeId))
                    booking.BookingBeestjes.Remove(bookingBeestje);
                else
                    selectedBeestjes.Remove(bookingBeestje.BeestjeId);
            }


            foreach (var beestjeId in selectedBeestjes)
            {
                var beestje = await _beestjeRepository.Get(beestjeId);

                booking.BookingBeestjes.Add(new BookingBeestje
                {
                    Booking = booking,
                    Beestje = beestje
                });
            }

            if (booking.BookingBeestjes.Select(x => x.Beestje).Count(x => x.Type == Type.Boerderij) > 0)
            {
                if (booking.BookingBeestjes.Select(x => x.Beestje).Any(x => x.Name == "Leeuw" || x.Name == "Ijsbeer"))
                {
                    booking.BookingBeestjes.Clear();
                    throw new CantHaveFarmAnimalException();
                }
            }


            // Clear accessoires
            booking.BookingAccessoires.Clear();

            booking.Step = BookingStep.Accessoires;
        }

        public async Task SelectAccessoires(string accessToken, List<int> selectedAccessoires)
        {
            var booking = await GetBooking(accessToken);

            foreach (var bookingAccessoire in booking.BookingAccessoires.ToList())
            {
                if (!selectedAccessoires.Contains(bookingAccessoire.AccessoireId))
                    booking.BookingAccessoires.Remove(bookingAccessoire);
                else
                    selectedAccessoires.Remove(bookingAccessoire.AccessoireId);
            }


            foreach (var accessoireId in selectedAccessoires)
            {
                var accessoire = await _accessoireRepository.Get(accessoireId);

                booking.BookingAccessoires.Add(new BookingAccessoires
                {
                    Booking = booking,
                    Accessoire = accessoire
                });
            }

            booking.Step = BookingStep.Login;
        }


        public async Task<List<Accessoire>> GetAvailableAccessoires(string accessToken)
        {
            var booking = await _bookingRepository.GetByAccessToken(accessToken);
            var beestjes = booking.BookingBeestjes.Select(x => x.Beestje).ToList();


            var availableAccessoires = new List<Accessoire>();
            foreach (var beestje in beestjes)
                availableAccessoires
                    .AddRange((await _beestjeRepository.Get(beestje.Id)).BeestjeAccessoires
                        .Select(x => x.Accessoire)
                        .ToList());


            return availableAccessoires;
        }
    }
}