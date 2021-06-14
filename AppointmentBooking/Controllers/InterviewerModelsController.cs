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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AppointmentBooking.Controllers
{

    public class InterviewerModelsController : Controller
    {
        private readonly AppointmentDbContext _context;

        public InterviewerModelsController(AppointmentDbContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: InterviewerModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Interviewer.ToListAsync());
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost("login")]
        public async Task<IActionResult> ValidateAdmin(string username, string password, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (username == "admin" && password == "pizza")
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("username", username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                claims.Add(new Claim(ClaimTypes.Name, username));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect(returnUrl);
            }
            TempData["Error"] = "Error. Username or Password is incorrect.";
            return View("login");
        }

        //Denied Page
        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }

        //logout user
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        // GET: InterviewerModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interviewerModel = await _context.Interviewer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (interviewerModel == null)
            {
                return NotFound();
            }

            return View(interviewerModel);
        }

        // GET: InterviewerModels/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: InterviewerModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,NumberOfSlots")] InterviewerModel interviewerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interviewerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(interviewerModel);
        }

        // GET: InterviewerModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interviewerModel = await _context.Interviewer.FindAsync(id);
            if (interviewerModel == null)
            {
                return NotFound();
            }
            return View(interviewerModel);
        }

        // POST: InterviewerModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,NumberOfSlots")] InterviewerModel interviewerModel)
        {
            if (id != interviewerModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interviewerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterviewerModelExists(interviewerModel.ID))
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
            return View(interviewerModel);
        }

        // GET: InterviewerModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interviewerModel = await _context.Interviewer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (interviewerModel == null)
            {
                return NotFound();
            }

            return View(interviewerModel);
        }

        // POST: InterviewerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interviewerModel = await _context.Interviewer.FindAsync(id);
            _context.Interviewer.Remove(interviewerModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterviewerModelExists(int id)
        {
            return _context.Interviewer.Any(e => e.ID == id);
        }
    }
}
