using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DomainServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Exceptions;
using Models.Form;

namespace Web.Controllers
{
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;
        private readonly IBeestjeService _beestjeService;
        private readonly IDiscountService _discountService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public BookingController(
            ApplicationDbContext db, IBookingService bookingService,
            IDiscountService discountService,
            IBeestjeService beestjeService, SignInManager<IdentityUser> signInManager) : base(db)
        {
            _bookingService = bookingService;
            _beestjeService = beestjeService;
            _discountService = discountService;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> ShowAvailableBeestjes()
        {
            try
            {
                var booking = await _bookingService.GetBooking(GetAccessToken());
                var beestjes = await _beestjeService.GetAvailableBeestjes(booking);

                if (booking.Step >= BookingStep.Price)
                    return RedirectToAction("ShowPricing");
                
                var data = (booking, beestjes);
                return View(data);
            }
            catch (BookingNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> ShowAvailableAccessories()
        {
            try
            {
                var booking = await _bookingService.GetBooking(GetAccessToken());
                var accessoires = new List<Accessoire>();

                if (booking.Step >= BookingStep.Price)
                    return RedirectToAction("ShowPricing");
                
                var data = (booking, accessoires);
                return View(data);
            }
            catch (BookingNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> ShowLoginOrRegister()
        {
            try
            {
                var booking = await _bookingService.GetBooking(GetAccessToken());

                if (booking.Step >= BookingStep.Price)
                    return RedirectToAction("ShowPricing");

                if (_signInManager.IsSignedIn(User))
                    return RedirectToAction("SelectPrice");

                return View("LoginOrRegister", booking);

            }
            catch (BookingNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> ShowPricing()
        {
            // TODO: Implement
            return Ok();
        }

        #region POST Actions

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectBeestjes(List<int> selectedBeestjes)
        {
            try
            {
                await _bookingService.SelectBeestjes(GetAccessToken(), selectedBeestjes);
            }
            catch (BookingNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return RedirectToAction("ShowAvailableAccessories");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectAccessoires(List<int> selectedAccessoires)
        {
            // TODO: Implement
            return Ok();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectPrice()
        {
            // TODO: Implement
            return Ok();
        }
        
        #endregion
    }
}
