using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BLL.Classes;
using Presentation.Data;

namespace Presentation.Controllers
{
    public class SeriesController : Controller
    {
        private readonly SeriesCatalogueContext _context;

        public SeriesController(SeriesCatalogueContext context)
        {
            _context = context;
        }

        // GET: Series
        public async Task<IActionResult> Index()
        {
            var seriesCatalogueContext = _context.SeriesModel.Include(s => s.Category);
            return View(await seriesCatalogueContext.ToListAsync());
        }

        // GET: Series/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SeriesModel == null)
            {
                return NotFound();
            }

            var seriesModel = await _context.SeriesModel
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seriesModel == null)
            {
                return NotFound();
            }

            return View(seriesModel);
        }

        // GET: Series/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.CategoryModel, "Id", "Genre");
            return View();
        }

        // POST: Series/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Title,Description,Year,Id")] SeriesModel seriesModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seriesModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.CategoryModel, "Id", "Genre", seriesModel.CategoryId);
            return View(seriesModel);
        }

        // GET: Series/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SeriesModel == null)
            {
                return NotFound();
            }

            var seriesModel = await _context.SeriesModel.FindAsync(id);
            if (seriesModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.CategoryModel, "Id", "Genre", seriesModel.CategoryId);
            return View(seriesModel);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Title,Description,Year,Id")] SeriesModel seriesModel)
        {
            if (id != seriesModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seriesModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeriesModelExists(seriesModel.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.CategoryModel, "Id", "Genre", seriesModel.CategoryId);
            return View(seriesModel);
        }

        // GET: Series/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SeriesModel == null)
            {
                return NotFound();
            }

            var seriesModel = await _context.SeriesModel
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seriesModel == null)
            {
                return NotFound();
            }

            return View(seriesModel);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SeriesModel == null)
            {
                return Problem("Entity set 'SeriesCatalogueContext.SeriesModel'  is null.");
            }
            var seriesModel = await _context.SeriesModel.FindAsync(id);
            if (seriesModel != null)
            {
                _context.SeriesModel.Remove(seriesModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeriesModelExists(int id)
        {
          return _context.SeriesModel.Any(e => e.Id == id);
        }
    }
}
