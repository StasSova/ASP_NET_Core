using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _05_ViewModel_Session.Models.DataBase;
using _05_ViewModel_Session.Models.ViewModels;
using _05_ViewModel_Session.Services.DataBase;
using System.Security.AccessControl;

namespace _05_ViewModel_Session.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IRepository _rep;

        public MessagesController(IRepository repository)
        {
            _rep = repository;
        }
        // GET: Messages
        public async Task<IActionResult> Index()
        {
            return View(await _rep.GetMessages());
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
                await _rep.CreateReview(id ??= 0, message.Body, DateTime.UtcNow);
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(message);
        }

        private async Task<bool> MessageExists(int id)
        {
            return await _rep.ReviewExists(id);
        }
    }
}
