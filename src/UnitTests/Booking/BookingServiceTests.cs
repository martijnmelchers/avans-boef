using System;
using System.Collections.Generic;
using System.Text;
using DomainServices;
using Models;
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


        public BookingServiceTests(DatabaseFixture fixture)
        {
            _modelMocks = fixture.Mocks;
            var bookingRepository = new BookingRepository(fixture.Db);
            var beestjeRepository = new BeestjeRepository(fixture.Db);
            var accessoireRepository = new AccessoireRepository(fixture.Db);

            _bookingService = new BookingService(bookingRepository,beestjeRepository,accessoireRepository);
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

            var beestjes = _bookingService.GetAllBeestjesByBooking(booking);

            Assert.Equal(selectedBeestjes.Count, beestjes.Count);
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

            booking = await _bookingService.GetBooking(booking.AccessToken);
            Assert.Equal(selectedAccessoires.Count, booking.BookingBeestjes.Count);
        }
    }
}
