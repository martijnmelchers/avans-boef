using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using DomainServices.Interfaces;
using Models;
using Type = Models.Type;

namespace DomainServices
{
    class DiscountService : IDiscountService
    {
        private readonly IBookingService _bookingService;
        public DiscountService(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<List<Discount>> GetDiscount(Booking booking)
        {
            List<Discount> discounts = new List<Discount>();
            var typeDiscount = await GetTypeDiscount(booking);

            if(typeDiscount != null)
                discounts.Add(typeDiscount);

            var duckNameDiscount = await GetDuckNameDiscount(booking);
            if(duckNameDiscount != null)
                discounts.Add(duckNameDiscount);

            var charDiscounts =  await GetCharDiscount(booking);
            discounts.AddRange(charDiscounts);

            var dateDiscount = GetDateDiscount(booking);
            if(dateDiscount != null)
                discounts.Add(dateDiscount);

            var totalDiscount = 0;

            discounts.ForEach((discount) =>
            {
                if (totalDiscount + discount.Percentage > 60)
                {
                    discounts.Remove(discount);
                }
                else
                    totalDiscount += discount.Percentage;
            });

            return discounts;
        }


        private Discount GetDateDiscount(Booking booking)
        {
            if(booking.Date.DayOfWeek == DayOfWeek.Monday || booking.Date.DayOfWeek == DayOfWeek.Tuesday)
                return new Discount("De dag is maandag of dinsdag", 15);

            return null;
        }

        private async Task<Discount> GetDuckNameDiscount(Booking booking)
        {
            var beestjes = await _bookingService.GetBeestjesByBooking(booking);
            int discount = 0;

            if (beestjes.FirstOrDefault(b => b.Name == "Eend") != null)
            {
                var random = new Random();
                if (random.Next(1, 6) == 0)
                {
                    discount += 50;
                }
            }

            if (discount == 0)
                return null;

            return new Discount("1 op 6 kans met naam: Eend", discount);
        }

        private async Task<List<Discount>>GetCharDiscount(Booking booking)
        {
            var beestjes = await _bookingService.GetBeestjesByBooking(booking);
            var discounts = new List<Discount>();
            var curChar = 'a';

            while (beestjes.FirstOrDefault(b => b.Name.Contains(curChar)) != null)
            {
                discounts.Add(new Discount("Beestje met de letter: " + curChar.ToString(),2));
            }

            return discounts;
        }

        private async Task<Discount> GetTypeDiscount(Booking booking)
        {
            var beestjes =  await _bookingService.GetBeestjesByBooking(booking);

            if (beestjes.Count < 3)
                return null;

            var types =  beestjes.Select(b => b.Type);

            foreach (var type in types)
            {
                if(types.Count(x => x.Equals(type)) == 3)
                {
                    return new Discount("3 items van het zelfde type", 10);
                }
            }

            return null;
        }
    }
}
