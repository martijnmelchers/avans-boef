using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DomainServices;
using Microsoft.EntityFrameworkCore.Query;
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
            await _bookingService.CalculateFinalPrice(_modelMocks.Bookings[2].AccessToken);
            await _db.SaveChangesAsync(); // Manual save

            var discounts = (await _bookingService.GetBooking(_modelMocks.Bookings[2].AccessToken)).Discounts;
            
            Assert.Empty(discounts);
        }

        [Fact]
        public async void ShouldGetNameDiscount()
        {
            /*var booking = await _bookingService.GetBooking(_modelMocks.Bookings[6].AccessToken);
            var discounts =  _discountService.GetDiscount(booking);*/
            
            await _bookingService.CalculateFinalPrice(_modelMocks.Bookings[6].AccessToken);
            await _db.SaveChangesAsync(); // Manual save

            var discounts = (await _bookingService.GetBooking(_modelMocks.Bookings[6].AccessToken)).Discounts;

            Assert.Single(discounts);
            Assert.Equal(2, discounts[0].Percentage);
        }


        [Fact]
        public async void ShouldGetDayDiscount()
        {
            await _bookingService.CalculateFinalPrice(_modelMocks.Bookings[4].AccessToken);
            await _db.SaveChangesAsync(); // Manual save

            var discounts = (await _bookingService.GetBooking(_modelMocks.Bookings[4].AccessToken)).Discounts;
            
            Assert.True((discounts.Sum(x => x.Percentage) == 17));
        }

        [Fact]
        public async void ShouldNotGetOverSixtyDiscount()
        {
            await _bookingService.CalculateFinalPrice(_modelMocks.Bookings[5].AccessToken);
            await _db.SaveChangesAsync(); // Manual save

            var discounts = (await _bookingService.GetBooking(_modelMocks.Bookings[5].AccessToken)).Discounts;
            
            Assert.NotEmpty(discounts);
            Assert.True((discounts.Sum(x => x.Percentage) <= 60));
        }


        [Fact]
        private async void ShouldGetDuckDiscount()
        {
            bool randomAchieved = false;
            int tries = 0;
            while (randomAchieved == false)
            {
                await _bookingService.CalculateFinalPrice(_modelMocks.Bookings[7].AccessToken); // Order with Name "Eend"
                await _db.SaveChangesAsync(); // Manual save

                var discounts = (await _bookingService.GetBooking(_modelMocks.Bookings[7].AccessToken)).Discounts;


                if ((discounts.Count > 0))
                {
                    randomAchieved = true;
                    break;
                }
                
                Assert.NotEqual(1000, tries);
                tries++;
            }

            var disc = (await _bookingService.GetBooking(_modelMocks.Bookings[7].AccessToken)).Discounts;
            Assert.Single(disc);
            Assert.True(randomAchieved);
        }


        [Fact]
        private async void ShouldGetTypeDiscount()
        {
            await _bookingService.CalculateFinalPrice(_modelMocks.Bookings[8].AccessToken);
            await _db.SaveChangesAsync(); // Manual save

            var discounts = (await _bookingService.GetBooking(_modelMocks.Bookings[8].AccessToken)).Discounts;
            Assert.Single(discounts);
            Assert.Equal(10, discounts[0].Percentage);
        }
    }
}
