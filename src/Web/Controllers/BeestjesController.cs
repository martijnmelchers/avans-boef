using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Web.Controllers
{
    public class BeestjesController : BaseController
    {
        private readonly IBeestjeService _beestjeService;


        public BeestjesController(ApplicationDbContext db, IBeestjeService beestjeService) : base(db)
        {
            _beestjeService = beestjeService;
        }

        // GET: Beestjes
        public async Task<IActionResult> Index()
        {
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
