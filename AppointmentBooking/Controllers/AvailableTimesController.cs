using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppointmentBooking.Data;
using AppointmentBooking.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppointmentBooking.Controllers
{
    public class AvailableTimesController : Controller
    {
        private readonly AppointmentDbContext _context;

        public AvailableTimesController(AppointmentDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: AvailableTimes
        public async Task<IActionResult> Index()
        {
            var appointmentDbContext = _context.AvailableTimes.Include(a => a.Interviewer);
            return View(await appointmentDbContext.ToListAsync());
        }
        [Authorize]
        // GET: AvailableTimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableTimes = await _context.AvailableTimes
                .Include(a => a.Interviewer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (availableTimes == null)
            {
                return NotFound();
            }

            return View(availableTimes);
        }
        [Authorize]
        // GET: AvailableTimes/Create
        public IActionResult Create()
        {
            var options = _context.Interviewer.Select(a =>
                                     new SelectListItem
                                     {
                                         Value = a.ID.ToString(),
                                         Text = a.FirstName + " " + a.LastName
                                     }).ToList();
            options.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            //ViewData["InterviewerID"] = new SelectList(_context.Interviewer, "ID", "ID");
            ViewData["interviewerInfo"] = options;


            return View();
        }
        [Authorize]
        // POST: AvailableTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Time,InterviewerID")] AvailableTimes availableTimes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(availableTimes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InterviewerID"] = new SelectList(_context.Interviewer, "ID", "ID", availableTimes.InterviewerID);
            return View(availableTimes);
        }
        [Authorize]
        // GET: AvailableTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableTimes = await _context.AvailableTimes.FindAsync(id);
            if (availableTimes == null)
            {
                return NotFound();
            }
            ViewData["InterviewerID"] = new SelectList(_context.Interviewer, "ID", "ID", availableTimes.InterviewerID);
            return View(availableTimes);
        }
        [Authorize]
        // POST: AvailableTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Time,InterviewerID")] AvailableTimes availableTimes)
        {
            if (id != availableTimes.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(availableTimes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvailableTimesExists(availableTimes.ID))
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
            ViewData["InterviewerID"] = new SelectList(_context.Interviewer, "ID", "ID", availableTimes.InterviewerID);
            return View(availableTimes);
        }
        [Authorize]
        // GET: AvailableTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availableTimes = await _context.AvailableTimes
                .Include(a => a.Interviewer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (availableTimes == null)
            {
                return NotFound();
            }

            return View(availableTimes);
        }
        [Authorize]
        // POST: AvailableTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var availableTimes = await _context.AvailableTimes.FindAsync(id);
            _context.AvailableTimes.Remove(availableTimes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailableTimesExists(int id)
        {
            return _context.AvailableTimes.Any(e => e.ID == id);
        }
    }
}
