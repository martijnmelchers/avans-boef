using DomainServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Web.Controllers
{
    [Route("Boeking")]
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;
        
        public BookingController(ApplicationDbContext db, IBookingService bookingService) : base(db)
        {
            _bookingService = bookingService;
        }

    }
}