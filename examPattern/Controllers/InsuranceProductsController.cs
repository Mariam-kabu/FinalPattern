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
    public class InsuranceProductsController : Controller
    {
        private readonly InsuranceDbContext _context;

        public InsuranceProductsController(InsuranceDbContext context)
        {
            _context = context;
        }

        // GET: InsuranceProducts
        public async Task<IActionResult> Index()
        {
              return _context.InsuranceProducts != null ? 
                          View(await _context.InsuranceProducts.ToListAsync()) :
                          Problem("Entity set 'InsuranceDbContext.InsuranceProducts'  is null.");
        }

        // GET: InsuranceProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InsuranceProducts == null)
            {
                return NotFound();
            }

            var insuranceProduct = await _context.InsuranceProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceProduct == null)
            {
                return NotFound();
            }

            return View(insuranceProduct);
        }

        // GET: InsuranceProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InsuranceProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Category,Type,UserType,InsurancePremium,TermsOfService")] InsuranceProduct insuranceProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceProduct);
        }

        // GET: InsuranceProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InsuranceProducts == null)
            {
                return NotFound();
            }

            var insuranceProduct = await _context.InsuranceProducts.FindAsync(id);
            if (insuranceProduct == null)
            {
                return NotFound();
            }
            return View(insuranceProduct);
        }

        // POST: InsuranceProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Category,Type,UserType,InsurancePremium,TermsOfService")] InsuranceProduct insuranceProduct)
        {
            if (id != insuranceProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceProductExists(insuranceProduct.Id))
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
            return View(insuranceProduct);
        }

        // GET: InsuranceProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InsuranceProducts == null)
            {
                return NotFound();
            }

            var insuranceProduct = await _context.InsuranceProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceProduct == null)
            {
                return NotFound();
            }

            return View(insuranceProduct);
        }

        // POST: InsuranceProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InsuranceProducts == null)
            {
                return Problem("Entity set 'InsuranceDbContext.InsuranceProducts'  is null.");
            }
            var insuranceProduct = await _context.InsuranceProducts.FindAsync(id);
            if (insuranceProduct != null)
            {
                _context.InsuranceProducts.Remove(insuranceProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceProductExists(int id)
        {
          return (_context.InsuranceProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
