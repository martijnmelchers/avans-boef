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



        public DiscountServiceTests(DatabaseFixture fixture)
        {
            var bookingRepository = new BookingRepository(fixture.Db);
            var beestjeRepository = new BeestjeRepository(fixture.Db);
            var accessoireRepository = new AccessoireRepository(fixture.Db);


            var discountService = new DiscountService(new BookingService(bookingRepository,beestjeRepository,accessoireRepository));

            _modelMocks = fixture.Mocks;
            _db = fixture.Db;
            _discountService = discountService;
        }

        [Fact]
        public async void ShouldGetNoDiscount()
        {
            var booking = _modelMocks.Bookings[2]; //Booking id 3 with Penguin attached.
            var discounts = await _discountService.GetDiscount(booking.AccessToken);
            Assert.Empty(discounts);
        }

        [Fact]
        public async void ShouldGetNameDiscount()
        {
            var booking = _modelMocks.Bookings[6];
            var discounts = await _discountService.GetDiscount(booking.AccessToken);

            Assert.Single(discounts);
            Assert.Equal(2, discounts[0].Percentage);
        }


        [Fact]
        public async void ShouldGetDayDiscount()
        {
            var booking = _modelMocks.Bookings[4];
            var discounts = await _discountService.GetDiscount(booking.AccessToken);

            Assert.Single(discounts);
            Assert.True((discounts.Sum(x => x.Percentage) == 15));
        }

        [Fact]
        public async void ShouldNotGetOverSixtyDiscount()
        {
            var booking = _modelMocks.Bookings[5];
            var discounts = await _discountService.GetDiscount(booking.AccessToken);
            Assert.NotEmpty(discounts);
            Assert.True((discounts.Sum(x => x.Percentage) <= 60));
        }
    }
}
