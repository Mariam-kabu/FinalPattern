using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using examPattern.Models;

namespace examPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInsuranceProductsController : Controller
    {
        private readonly InsuranceDbContext _context;

        public UserInsuranceProductsController(InsuranceDbContext context)
        {
            _context = context;
        }

        // GET: UserInsuranceProducts
        public async Task<IActionResult> Index()
        {
            var insuranceDbContext = _context.UserInsuranceProducts.Include(u => u.InsuranceProduct).Include(u => u.User);
            return View(await insuranceDbContext.ToListAsync());
        }

        // GET: UserInsuranceProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserInsuranceProducts == null)
            {
                return NotFound();
            }

            var userInsuranceProduct = await _context.UserInsuranceProducts
                .Include(u => u.InsuranceProduct)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userInsuranceProduct == null)
            {
                return NotFound();
            }

            return View(userInsuranceProduct);
        }

        // GET: UserInsuranceProducts/Create
        public IActionResult Create()
        {
            ViewData["InsuranceProductId"] = new SelectList(_context.InsuranceProducts, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UserInsuranceProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,InsuranceProductId")] UserInsuranceProduct userInsuranceProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userInsuranceProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceProductId"] = new SelectList(_context.InsuranceProducts, "Id", "Name", userInsuranceProduct.InsuranceProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userInsuranceProduct.UserId);
            return View(userInsuranceProduct);
        }

        // GET: UserInsuranceProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserInsuranceProducts == null)
            {
                return NotFound();
            }

            var userInsuranceProduct = await _context.UserInsuranceProducts.FindAsync(id);
            if (userInsuranceProduct == null)
            {
                return NotFound();
            }
            ViewData["InsuranceProductId"] = new SelectList(_context.InsuranceProducts, "Id", "Name", userInsuranceProduct.InsuranceProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userInsuranceProduct.UserId);
            return View(userInsuranceProduct);
        }

        // POST: UserInsuranceProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,InsuranceProductId")] UserInsuranceProduct userInsuranceProduct)
        {
            if (id != userInsuranceProduct.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userInsuranceProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserInsuranceProductExists(userInsuranceProduct.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceProductId"] = new SelectList(_context.InsuranceProducts, "Id", "Name", userInsuranceProduct.InsuranceProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userInsuranceProduct.UserId);
            return View(userInsuranceProduct);
        }

        // GET: UserInsuranceProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserInsuranceProducts == null)
            {
                return NotFound();
            }

            var userInsuranceProduct = await _context.UserInsuranceProducts
                .Include(u => u.InsuranceProduct)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userInsuranceProduct == null)
            {
                return NotFound();
            }

            return View(userInsuranceProduct);
        }

        // POST: UserInsuranceProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserInsuranceProducts == null)
            {
                return Problem("Entity set 'InsuranceDbContext.UserInsuranceProducts'  is null.");
            }
            var userInsuranceProduct = await _context.UserInsuranceProducts.FindAsync(id);
            if (userInsuranceProduct != null)
            {
                _context.UserInsuranceProducts.Remove(userInsuranceProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserInsuranceProductExists(int id)
        {
          return (_context.UserInsuranceProducts?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
