using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurfsUpExam.Data;
using SurfsUpExam.Models;

namespace SurfsUpExam.Controllers
{
    public class SurfsBoardsController : Controller
    {
        private readonly SurfsUpExamContext _context;

        public SurfsBoardsController(SurfsUpExamContext context)
        {
            _context = context;
        }

        // GET: SurfsBoards
        public async Task<IActionResult> Index()
        {
            return View(await _context.SurfsBoard.ToListAsync());
        }

        // GET: SurfsBoards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surfsBoard = await _context.SurfsBoard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surfsBoard == null)
            {
                return NotFound();
            }

            return View(surfsBoard);
        }

        // GET: SurfsBoards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SurfsBoards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Length,Width,Thickness,Volume,BoardType,Price,Equipment")] SurfsBoard surfsBoard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surfsBoard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(surfsBoard);
        }

        // GET: SurfsBoards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surfsBoard = await _context.SurfsBoard.FindAsync(id);
            if (surfsBoard == null)
            {
                return NotFound();
            }
            return View(surfsBoard);
        }

        // POST: SurfsBoards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Length,Width,Thickness,Volume,BoardType,Price,Equipment")] SurfsBoard surfsBoard)
        {
            if (id != surfsBoard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surfsBoard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurfsBoardExists(surfsBoard.Id))
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
            return View(surfsBoard);
        }

        // GET: SurfsBoards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var surfsBoard = await _context.SurfsBoard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surfsBoard == null)
            {
                return NotFound();
            }

            return View(surfsBoard);
        }

        // POST: SurfsBoards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var surfsBoard = await _context.SurfsBoard.FindAsync(id);
            if (surfsBoard != null)
            {
                _context.SurfsBoard.Remove(surfsBoard);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurfsBoardExists(int id)
        {
            return _context.SurfsBoard.Any(e => e.Id == id);
        }
    }
}
