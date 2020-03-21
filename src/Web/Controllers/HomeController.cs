using System.Diagnostics;
using System.Threading.Tasks;
using DomainServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Exceptions;
using Models.Form;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBookingService _bookingService;

        public HomeController(ApplicationDbContext db, IBookingService bookingService) : base(db)
        {
            _bookingService = bookingService;
        }

        public async Task<IActionResult> Index()
        {
            // Check if accessToken is present, if not set one.
            if (!HasAccessToken())
                SetAccessToken(await _bookingService.CreateBooking());

            return View();
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DateSelection date)
        {
            if (!HasAccessToken())
                return Redirect("/");

            try
            {
                await _bookingService.SelectDate(GetAccessToken(), date.BookingDate);
                return RedirectToAction("ShowAvailableBeestjes", "Booking");
            }
            catch (BookingNotFoundException)
            {
                // This should not happen, but in the case it does, we set a new token
                SetAccessToken(await _bookingService.CreateBooking());
                ModelState.AddModelError(string.Empty, "Er is iets fout gegaan, probeer het opnieuw!");

                
                return View("Index", date);
            }
            catch (InvalidDateException)
            {
                ModelState.AddModelError(string.Empty, "Selecteer een datum die in de toekomst ligt");
                
                return View("Index", date);
            }
        }

        public IActionResult OrderStart()
        {



            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}