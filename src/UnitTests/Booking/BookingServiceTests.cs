using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainServices;
using Models;
using Models.Exceptions;
using Models.Repository;
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Booking
{

    [Collection("Database collection")]
    public class BookingServiceTests
    {
        private readonly ModelMocks _modelMocks;
        private readonly BookingService _bookingService;
        private readonly ApplicationDbContext _db;

        public BookingServiceTests(DatabaseFixture fixture)
        {
            var bookingRepository = new BookingRepository(fixture.Db);
            var beestjeRepository = new BeestjeRepository(fixture.Db);
            var accessoireRepository = new AccessoireRepository(fixture.Db);

            var discountService = new DiscountService();
            _bookingService = new BookingService(bookingRepository,beestjeRepository,accessoireRepository, discountService);

            _modelMocks = fixture.Mocks;
            _db = fixture.Db;
        }

        [Fact]
        public async void CanCreateBooking()
        {
            var accessToken = await _bookingService.CreateBooking();
            Assert.NotNull(accessToken);
        }

        [Fact]
        public async void CanGetBooking()
        {
            var booking = await _bookingService.GetBooking(_modelMocks.Bookings[0].AccessToken);
            Assert.NotNull(booking);
        }

        [Fact]
        public async void CanSelectDate()
        {
            var date = new DateTime(2020, 03, 30); 
            var booking = await _bookingService.GetBooking(_modelMocks.Bookings[0].AccessToken);
            await _bookingService.SelectDate(booking.AccessToken, date);

            Assert.Equal(date, (await _bookingService.GetBooking(booking.AccessToken)).Date);
        }

        [Fact]
        public async void CanSelectBeestjes()
        {
            var booking = await _bookingService.GetBooking(_modelMocks.Bookings[0].AccessToken);

            var selectedBeestjes = new List<int>()
            {
                _modelMocks.Beestjes[0].Id,
                _modelMocks.Beestjes[8].Id
            };

            await _bookingService.SelectBeestjes(booking.AccessToken, selectedBeestjes);
            _db.SaveChanges(); // Usually controller calls savechanges but now we do it manually.

            var beestjes =  booking.BookingBeestjes.Select(b => b.Beestje).ToList();

            Assert.Equal(selectedBeestjes.Count, beestjes.Count);
        }

        [Fact]
        public async void RemovesUnselectedBeestjes()
        {
            var booking = await _bookingService.GetBooking(_modelMocks.Bookings[0].AccessToken);

            var selectedBeestjes = new List<int>()
            {
                _modelMocks.Beestjes[0].Id,
                _modelMocks.Beestjes[8].Id
            };

            await _bookingService.SelectBeestjes(booking.AccessToken, selectedBeestjes);
            _db.SaveChanges(); // Usually controller calls savechanges but now we do it manually.


            selectedBeestjes = new List<int>()
            {
                _modelMocks.Beestjes[0].Id,
            };

            await _bookingService.SelectBeestjes(booking.AccessToken, selectedBeestjes);
            _db.SaveChanges(); // Usually controller calls savechanges but now we do it manually.



            booking = await _bookingService.GetBooking(_modelMocks.Bookings[0].AccessToken);

            var beestjes = booking.BookingBeestjes.Select(b => b.Beestje).ToList();

            Assert.Equal(1, beestjes.Count);
        }

        [Fact]
        public async void CanSelectAccessoires()
        {
            var booking = await _bookingService.GetBooking(_modelMocks.Bookings[0].AccessToken);

            var selectedAccessoires = new List<int>()
            {
                _modelMocks.Accessoires[0].Id,
            };

            await _bookingService.SelectAccessoires(booking.AccessToken, selectedAccessoires);
            _db.SaveChanges(); // Usually controller calls savechanges but now we do it manually.

            booking = await _bookingService.GetBooking(booking.AccessToken);
            Assert.Equal(selectedAccessoires.Count, booking.BookingBeestjes.Count);
        }

        [Fact]
        public async void InvalidAccessTokenThrowsException()
        {
            await Assert.ThrowsAsync<BookingNotFoundException>(async () => await _bookingService.GetBooking("Sascha is een slechte programmeur"));
        }
    }
}
