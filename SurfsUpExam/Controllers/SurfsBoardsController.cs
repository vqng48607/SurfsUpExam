using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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
        public async Task<IActionResult> Index(string sortOrder,string currentFilter, string searchString, int? pageNumber)
        {
            //Sorter funktionen: kan også ses i viewet index
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "Name_Desc" : "";
            ViewData["LengthSortParm"] = sortOrder == "Length" ? "Length_Desc" : "Length";
            ViewData["WidthSortParm"] = sortOrder == "Width" ? "Width_Desc" : "Width";
            ViewData["ThicknessSortParm"] = sortOrder == "Thickness" ? "Thickness_Desc" : "Thickness";
            ViewData["VolumeSortParm"] = sortOrder == "Volume" ? "Volume_Desc" : "Volume";
            ViewData["BoardTypeSortParm"] = sortOrder == "BoardType" ? "BoardType_Desc" : "BoardType";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "Price_Desc" : "Price";

            //PaginatedList: nederst i index er der ting. Dette gør at man kan tykke next og tidligere. Her vil den blive grå hvis der så ikke er noget
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            //Filtre: kan også ses i viewet index
            ViewData["CurrentFilter"] = searchString;


            var surfsBoard = from s in _context.SurfsBoard
                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                surfsBoard = surfsBoard.Where(b => b.Name.ToLower().Contains(searchString) ||
                                           b.Length.ToString().Contains(searchString) ||
                                           b.Width.ToString().Contains(searchString) ||
                                           b.Thickness.ToString().Contains(searchString) ||
                                           b.Volume.ToString().Contains(searchString) ||
                                           b.BoardType.ToString().ToLower().Contains(searchString) ||
                                           String.IsNullOrEmpty(b.Equipment) == false && b.Equipment.ToLower().Contains(searchString) ||
                                           b.Price.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Name_Desc":
                    surfsBoard = surfsBoard.OrderByDescending(s => s.Name);
                    break;
                case "Length":
                    surfsBoard = surfsBoard.OrderBy(s => s.Length);
                    break;
                case "Length_Desc":
                    surfsBoard = surfsBoard.OrderByDescending(s => s.Length);
                    break;
                case "Width":
                    surfsBoard = surfsBoard.OrderBy(s => s.Width);
                    break;
                case "Width_Desc":
                    surfsBoard = surfsBoard.OrderByDescending(s => s.Width);
                    break;
                case "Thickness":
                    surfsBoard = surfsBoard.OrderBy(s => s.Thickness);
                    break;
                case "Thickness_Desc":
                    surfsBoard = surfsBoard.OrderByDescending(s => s.Thickness);
                    break;
                case "Volume":
                    surfsBoard = surfsBoard.OrderBy(s => s.Volume);
                    break;
                case "Volume_Desc":
                    surfsBoard = surfsBoard.OrderByDescending(s => s.Volume);
                    break;
                case "BoardType":
                    surfsBoard = surfsBoard.OrderBy(s => s.BoardType);
                    break;
                case "BoardType_Desc":
                    surfsBoard = surfsBoard.OrderByDescending(s => s.BoardType);
                    break;
                case "Price":
                    surfsBoard = surfsBoard.OrderBy(s => s.Price);
                    break;
                case "Price_Desc":
                    surfsBoard = surfsBoard.OrderByDescending(s => s.Price);
                    break;
                default:
                    surfsBoard = surfsBoard.OrderBy(s => s.Name);
                    break;
            }

            //PaginatedList
            int pageSize = 1;
            return View(await PaginatedList<SurfsBoard>.CreateAsync(surfsBoard.AsNoTracking(), pageNumber ?? 1, pageSize));
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
