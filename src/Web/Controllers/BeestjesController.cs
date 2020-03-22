using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BeestjesController : BaseController
    {
        private readonly IBeestjeService _beestjeService;
        private readonly IAccessoireService _accessoireService;


        public BeestjesController(ApplicationDbContext db, IBeestjeService beestjeService,  IAccessoireService accessoireService) : base(db)
        {
            _beestjeService = beestjeService;
            _accessoireService = accessoireService;
        }

        // GET: Beestjes
        public async Task<IActionResult> Index()
        {

           await _beestjeService.GetBeestjes();
            return View(await _beestjeService.GetBeestjes());
        }


        // GET: Beestjes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Beestjes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Price,Image")] Beestje beestje)
        {
            if (ModelState.IsValid)
            {
                beestje = await _beestjeService.CreateBeestje(beestje);
                return RedirectToAction(nameof(Index));
            }
            return View(beestje);
        }

        // GET: Beestjes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beestje = await _beestjeService.GetBeestje(id);
            if (beestje == null)
            {
                return NotFound();
            }


            return View(beestje);
        }


        // GET: Beestjes/Edit/5
        public async Task<IActionResult> AddAccessoires(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beestje = await _beestjeService.GetBeestje(id);
            if (beestje == null)
            {
                return NotFound();
            }

  

            ViewBag.Accessoires = await _accessoireService.GetAccessoires();
            return View(beestje);
        }


            // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAccessoires(int id, [Bind("Accessoires")] List<int> accessoires)
        {
            var beestje = await _beestjeService.GetBeestje(id);

            beestje = await _beestjeService.SelectAccessoires(beestje, accessoires);
            ViewBag.Accessoires = await _accessoireService.GetAccessoires();
            return View(beestje);
        }


        // POST: Beestjes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Price,Image")] Beestje beestje)
        {


            await _beestjeService.EditBeestje(id, beestje);
            return View(beestje);
        }

        // GET: Beestjes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beestje = await _beestjeService.GetBeestje(id);
            if (beestje == null)
            {
                return NotFound();
            }

            return View(beestje);
        }

        //POST: Accessoires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _beestjeService.DeleteBeestje(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BeestjeExists(int id)
        {
            return _beestjeService.GetBeestje(id) != null;
        }
    }
}
