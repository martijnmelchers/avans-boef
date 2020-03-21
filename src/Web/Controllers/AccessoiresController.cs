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
    public class AccessoiresController : BaseController
    {
        private readonly IAccessoireService _accessoireService;


        public AccessoiresController(ApplicationDbContext db, IAccessoireService Accessoireservice) : base(db)
        {
            _accessoireService = Accessoireservice;
        }

        // GET: Accessoires
        public async Task<IActionResult> Index()
        {
            return View(await _accessoireService.GetAccessoires());
        }

        // GET: Accessoires/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Accessoire = await _accessoireService.GetAccessoire(id);

            if (Accessoire == null)
            {
                return NotFound();
            }

            return View(Accessoire);
        }

        // GET: Accessoires/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accessoires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Price,Image")] Accessoire Accessoire)
        {
            if (ModelState.IsValid)
            {
                Accessoire = await _accessoireService.CreateAccessoire(Accessoire);
                return RedirectToAction(nameof(Index));
            }
            return View(Accessoire);
        }

        // GET: Accessoires/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Accessoire = await _accessoireService.GetAccessoire(id);
            if (Accessoire == null)
            {
                return NotFound();
            }
            return View(Accessoire);
        }

        // POST: Accessoires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Price,Image")] Accessoire Accessoire)
        {
            await _accessoireService.EditAccessoire(id, Accessoire);
            return View(Accessoire);
        }

        // GET: Accessoires/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Accessoire = await _accessoireService.GetAccessoire(id);
            if (Accessoire == null)
            {
                return NotFound();
            }

            return View(Accessoire);
        }

        //POST: Accessoires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             await _accessoireService.DeleteAccessoire(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AccessoireExists(int id)
        {
            return _accessoireService.GetAccessoire(id) != null;
        }
    }
}
