using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DomainServices;
using Models;
using Models.Repository;
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Discount
{

    [Collection("Database collection")]
    public class DiscountServiceTests
    {
        private readonly ModelMocks _modelMocks;
        private readonly DiscountService _discountService;
        private readonly ApplicationDbContext _db;
        private readonly BookingService _bookingService;



        public DiscountServiceTests(DatabaseFixture fixture)
        {
            var bookingRepository = new BookingRepository(fixture.Db);
            var beestjeRepository = new BeestjeRepository(fixture.Db);
            var accessoireRepository = new AccessoireRepository(fixture.Db);


            var discountService = new DiscountService();
            

            _modelMocks = fixture.Mocks;
            _db = fixture.Db;
            _discountService = discountService;
            _bookingService = new BookingService(bookingRepository,beestjeRepository,accessoireRepository, discountService);
        }

        [Fact]
        public async void ShouldGetNoDiscount()
        {
            var booking = await _bookingService.GetBooking(_modelMocks.Bookings[2].AccessToken);
            var discounts =  _discountService.GetDiscount(booking);
            Assert.Empty(discounts);
        }

        [Fact]
        public async void ShouldGetNameDiscount()
        {
            var booking = await _bookingService.GetBooking(_modelMocks.Bookings[6].AccessToken);
            var discounts =  _discountService.GetDiscount(booking);

            Assert.Single(discounts);
            Assert.Equal(2, discounts[0].Percentage);
        }


        [Fact]
        public async void ShouldGetDayDiscount()
        {
            var booking = await _bookingService.GetBooking(_modelMocks.Bookings[4].AccessToken);
            var discounts =  _discountService.GetDiscount(booking);

            Assert.Single(discounts);
            Assert.True((discounts.Sum(x => x.Percentage) == 15));
        }

        [Fact]
        public async void ShouldNotGetOverSixtyDiscount()
        {
            var booking = await _bookingService.GetBooking(_modelMocks.Bookings[5].AccessToken);
            var discounts =  _discountService.GetDiscount(booking);
            Assert.NotEmpty(discounts);
            Assert.True((discounts.Sum(x => x.Percentage) <= 60));
        }
    }
}
