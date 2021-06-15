using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppointmentBooking.Data;
using AppointmentBooking.Models;

namespace AppointmentBooking.Controllers
{
  public class BookedAppointmentsController : Controller
  {
    private readonly AppointmentDbContext _context;

    public BookedAppointmentsController(AppointmentDbContext context)
    {
      _context = context;
    }

    // GET: BookedAppointments
    public async Task<IActionResult> Index()
    {
      var appointmentDbContext = _context.Appointment.Include(b => b.Interviewer);
      return View(await appointmentDbContext.ToListAsync());
    }

    // GET: BookedAppointments/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var bookedAppointment = await _context.Appointment
          .Include(b => b.Interviewer)
          .FirstOrDefaultAsync(m => m.ID == id);
      if (bookedAppointment == null)
      {
        return NotFound();
      }

      return View(bookedAppointment);
    }

    // GET: BookedAppointments/Create
    public IActionResult Create()
    {
      var options = _context.Interviewer.Select(a =>
      new SelectListItem
      {
        Value = a.ID.ToString(),
        Text = a.FirstName + " " + a.LastName
      }).ToList();

      var times = _context.AvailableTimes.Select(a =>
      new SelectListItem
      {

        Text = a.Time.ToString("dddd, dd MMMM yyyy  HH:mm:ss")
      }).ToList();

      options.Insert(0, new SelectListItem()
      {
        Text = "----Select----",
        Value = string.Empty
      });
      //ViewData["InterviewerID"] = new SelectList(_context.Interviewer, "ID", "ID");
      ViewData["test"] = options;
      ViewData["times"] = times;

      return View();
    }

    // POST: BookedAppointments/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Dates,InterviewerID")] BookedAppointment bookedAppointment)
    {
      if (ModelState.IsValid)
      {
        _context.Add(bookedAppointment);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Details), new { ID = bookedAppointment.ID });
      }
      ViewData["InterviewerID"] = new SelectList(_context.Interviewer, "ID", "ID", bookedAppointment.InterviewerID);
      return View(bookedAppointment);
    }

    // GET: BookedAppointments/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var bookedAppointment = await _context.Appointment.FindAsync(id);
      if (bookedAppointment == null)
      {
        return NotFound();
      }
      ViewData["InterviewerID"] = new SelectList(_context.Interviewer, "ID", "ID", bookedAppointment.InterviewerID);

      var options = _context.Interviewer.Select(a =>
      new SelectListItem
      {
        Value = a.ID.ToString(),
        Text = a.FirstName + " " + a.LastName
      }).ToList();

      var times = _context.AvailableTimes.Select(a =>
      new SelectListItem
      {
        Value = a.ID.ToString(),
        Text = a.Time.ToString("dddd, dd MMMM yyyy  HH:mm:ss")
      }).ToList();

      ViewData["options"] = options;
      ViewData["times"] = times;


      return View(bookedAppointment);
    }

    // POST: BookedAppointments/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Dates,InterviewerID")] BookedAppointment bookedAppointment)
    {
      if (id != bookedAppointment.ID)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(bookedAppointment);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!BookedAppointmentExists(bookedAppointment.ID))
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
      ViewData["InterviewerID"] = new SelectList(_context.Interviewer, "ID", "ID", bookedAppointment.InterviewerID);

      return View(bookedAppointment);


    }

    // GET: BookedAppointments/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var bookedAppointment = await _context.Appointment
          .Include(b => b.Interviewer)
          .FirstOrDefaultAsync(m => m.ID == id);
      if (bookedAppointment == null)
      {
        return NotFound();
      }

      return View(bookedAppointment);
    }

    // POST: BookedAppointments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var bookedAppointment = await _context.Appointment.FindAsync(id);
      _context.Appointment.Remove(bookedAppointment);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool BookedAppointmentExists(int id)
    {
      return _context.Appointment.Any(e => e.ID == id);
    }
  }
}
