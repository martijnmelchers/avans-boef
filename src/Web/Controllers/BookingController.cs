using System;
using System.Threading.Tasks;
using DomainServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Exceptions;
using Models.Form;

namespace Web.Controllers
{
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;
        
        public BookingController(ApplicationDbContext db, IBookingService bookingService) : base(db)
        {
            _bookingService = bookingService;
        }

        public async Task<IActionResult> SelectAnimals()
        {
            try
            {
                var booking = await _bookingService.GetBooking(Get("AccessToken"));

                (Booking booking, DateSelection date) data = (booking, null);
                return View(data);
            }
            catch (BookingNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }
        }


    }
}