using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DomainServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
                var accessoires = await _bookingService.GetAvailableAccessoires(GetAccessToken());

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
                    return RedirectToAction("ShowContactInfo");

                return View("LoginOrRegister", (booking, new Register(), new Login()));

            }
            catch (BookingNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> ShowContactInfo()
        {
            return Ok();
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
        public async Task<IActionResult> AddDetailsAndCalculatePrice()
        {
            // TODO: Implement
            return Ok();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _signInManager.UserManager.FindByEmailAsync(login.Email);
                await _bookingService.LinkAccountToBooking(GetAccessToken(), user.Id);
                
                return RedirectToAction("ShowContactInfo");
            }

            // If login failed, show the view again
            ModelState.AddModelError(string.Empty, "Incorrecte gebruikersnaam en/of wachtwoord!");
            return await ShowLoginOrRegister();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register register)
        {
            //TODO: Register
            return Ok();
        }
         
        #endregion
    }
}
