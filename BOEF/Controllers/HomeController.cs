using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DomainServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Exceptions;
using Models.Form;
using Newtonsoft.Json;
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
            if (!Present("AccessToken"))
                Set("AccessToken", await _bookingService.CreateBooking());
            
            return View();
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DateSelection date)
        {
            if (!Present("AccessToken"))
                return Redirect("/");

            try
            {
                await _bookingService.SelectDate(Get("AccessToken"), date.BookingDate);
                return RedirectToAction("Index", "Home", date);
            }
            catch (BookingNotFoundException)
            {
                Remove("AccessToken");
                return RedirectToAction("Index", "Home", new { booking = false });
            }
            catch (InvalidDateException)
            {
                ModelState.AddModelError(string.Empty, "Selecteer een datum die in de toekomst ligt");
                
                return View("Index", date);
            }

        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult OrderStart()
        {
            
            
            
            return View("OrderStart");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}