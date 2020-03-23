using System.Threading.Tasks;
using DomainServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccessoiresController : BaseController
    {
        private readonly IAccessoireService _accessoireService;


        public AccessoiresController(ApplicationDbContext db, IAccessoireService accessoireService) : base(db)
        {
            _accessoireService = accessoireService;
        }

        // GET: Accessoires
        public async Task<IActionResult> Index()
        {
            return View(await _accessoireService.GetAccessoires());
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
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Price,Image")] Accessoire accessoire)
        {
            if (!ModelState.IsValid) return View(accessoire);
            
            await _accessoireService.CreateAccessoire(accessoire);
            return RedirectToAction("Index");
        }

        // GET: Accessoires/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var accessoire = await _accessoireService.GetAccessoire(id);
            
            if (accessoire == null)
                return NotFound();

            return View(accessoire);
        }

        // POST: Accessoires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Price,Image")] Accessoire accessoire)
        {
            await _accessoireService.EditAccessoire(id, accessoire);
            return View(accessoire);
        }

        // GET: Accessoires/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var accessoire = await _accessoireService.GetAccessoire(id);
            if (accessoire == null)
                return NotFound();

            return View(accessoire);
        }

        //POST: Accessoires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _accessoireService.DeleteAccessoire(id);
            return RedirectToAction(nameof(Index));
        }
    }
}