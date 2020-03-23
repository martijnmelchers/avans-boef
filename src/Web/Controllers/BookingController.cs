using System.Collections.Generic;
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
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public BookingController(
            ApplicationDbContext db, IBookingService bookingService, 
            IBeestjeService beestjeService, SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) : base(db)
        {
            _bookingService = bookingService;
            _beestjeService = beestjeService;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
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
                return View("ShowAvailableBeestjes", data);
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
            try
            {
                var booking = await _bookingService.GetBooking(GetAccessToken());

                if (booking.Step >= BookingStep.Price)
                    return RedirectToAction("ShowPricing");

                if (!_signInManager.IsSignedIn(User))
                    return RedirectToAction("ShowLoginOrRegister");

                return View("ShowContactInfo", (booking, new ContactInfo()));
            }
            catch (BookingNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> ShowPricing()
        {
            try
            {
                var booking = await _bookingService.GetBooking(GetAccessToken());

                if (booking.Step != BookingStep.Price)
                    return booking.Step == BookingStep.Finished
                        ? RedirectToAction("Details", new { id = booking.Id})
                        : RedirectToAction("ShowContactInfo");

                if (!_signInManager.IsSignedIn(User))
                    return RedirectToAction("ShowLoginOrRegister");

                return View("ShowPricing", booking);
            }
            catch (BookingNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var booking = await _bookingService.GetBookingById(id);

                var user = await _userManager.GetUserAsync(User);

                if (booking.UserId != user.Id)
                    return RedirectToAction("Index", "Home");

                return View("Details", booking);

            }
            catch (BookingNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var bookings = await _bookingService.GetBookingByUserId(user.Id);
                
                return View("Index", bookings);

            }
            catch (BookingNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }
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
            catch (CantHaveFarmAnimalException)
            {
                ModelState.AddModelError(string.Empty,
                    "Je mag geen beestje boeken met de naam ‘Leeuw’ of ‘IJsbeer’ als je ook een beestje boekt van het type ‘Boerderijdier’");

                return await ShowAvailableBeestjes();
            }

            return RedirectToAction("ShowAvailableAccessories");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectAccessoires(List<int> selectedAccessoires)
        {
            try
            {
                await _bookingService.SelectAccessoires(GetAccessToken(), selectedAccessoires);
            }
            catch (BookingNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("ShowLoginOrRegister");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDetailsAndCalculatePrice(ContactInfo contactInfo)
        {
            try
            {
                await _bookingService.SaveContactInfo(GetAccessToken(), contactInfo);
                await _bookingService.CalculateFinalPrice(GetAccessToken());
            }
            catch (BookingNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("ShowPricing");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmBooking()
        {
            try
            {
                var booking = await _bookingService.GetBooking(GetAccessToken());
                await _bookingService.ConfirmBooking(GetAccessToken());
                
                // Remove access token, we're done!
                RemoveAccessToken();
                
                return RedirectToAction("Details", new { id = booking.Id });
            }  catch (BookingNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
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
            var result =
                await _userManager.CreateAsync(new IdentityUser { UserName = register.Email, Email = register.Email },
                    register.Password);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(register.Email);

                if (register.IsAdmin)
                {
                    // If the role does not exist yet we need to create it once, need to find better solution if this was ever be used...
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));

                    await _userManager.AddToRoleAsync(user, "Admin");
                }

                await _signInManager.SignInAsync(user, false);
                await _bookingService.LinkAccountToBooking(GetAccessToken(), user.Id);

                return RedirectToAction("ShowContactInfo");
            }

            // If register failed, show the view again
            ModelState.AddModelError(string.Empty, "Er is iets misgegaan tijdens de registratie!");
            return await ShowLoginOrRegister();
        }

        #endregion
    }
}