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
    public class DiscountService : IDiscountService
    {
        public List<Discount> GetDiscount(Booking booking)
        {
            var discounts = new List<Discount>();
            var typeDiscount = GetTypeDiscount(booking);

            if(typeDiscount != null)
                discounts.Add(typeDiscount);

            var duckNameDiscount = GetDuckNameDiscount(booking);
            if(duckNameDiscount != null)
                discounts.Add(duckNameDiscount);

            var charDiscounts =  GetCharDiscount(booking);
            discounts.AddRange(charDiscounts);

            var dateDiscount = GetDateDiscount(booking);
            if(dateDiscount != null)
                discounts.Add(dateDiscount);

            var totalDiscount = 0;

            discounts.ForEach(discount =>
            {
                if (totalDiscount + discount.Percentage > 60)
                    discounts.Remove(discount);
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

        private Discount GetDuckNameDiscount(Booking booking)
        {
            var beestjes = GetAllBeestjesByBooking(booking);
            int discount = 0;

            if (beestjes.FirstOrDefault(b => b.Name == "Eend") != null)
                if (new Random().Next(1, 6) == 0)
                    discount += 50;

            return discount == 0 ? null : new Discount("1 op 6 kans met naam: Eend", discount);
        }

        private List<Discount> GetCharDiscount(Booking booking)
        {
            var beestjes = GetAllBeestjesByBooking(booking);
            var discounts = new List<Discount>();
            char curChar = 'a';

            while (beestjes.FirstOrDefault(b => b.Name.Contains(curChar)) != null)
            {
                discounts.Add(new Discount("Beestje met de letter: " + curChar,2));
                curChar++;
            }

            return discounts;
        }

        private Discount GetTypeDiscount(Booking booking)
        {
            var beestjes =  GetAllBeestjesByBooking(booking);


            if (beestjes.Count < 3)
                return null;

            var types =  beestjes.Select(b => b.Type).ToList();

            return types.Any(type => types.Count(x => x.Equals(type)) == 3) ? new Discount("3 items van het zelfde type", 10) : null;
        }

        private static List<Beestje> GetAllBeestjesByBooking(Booking booking)
        {
            return booking.BookingBeestjes.Select(b => b.Beestje).ToList();
        }
    }
}
