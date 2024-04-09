using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _05_ViewModel_Session.Models.DataBase;
using _05_ViewModel_Session.Models.ViewModels;

namespace _05_ViewModel_Session.Controllers
{
    public class MessagesController : Controller
    {
        private readonly GuestBookContext _context;

        public MessagesController(GuestBookContext context)
        {
            _context = context;
        }
        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var guestBookContext = _context.Messages.Include(m => m.User);
            return View(await guestBookContext.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VM_Message message)
        {
            if (ModelState.IsValid)
            {
                int? id = (int?)HttpContext.Session.GetInt32("userId");
                Message mes = new Message()
                {
                    Body = message.Body,
                    UserId = id ?? 0,
                    Date = DateTime.Now
                };


                _context.Add(mes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(message);
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
