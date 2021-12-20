using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webApiSample01.Models;

namespace webApiSample01.Controllers
{
    public class UseraccountsController : Controller
    {
        private readonly LeaveSystemContext _context;

        public UseraccountsController(LeaveSystemContext context)
        {
            _context = context;
        }

        // GET: Useraccounts
        public async Task<IActionResult> Index()
        {
            var orderingDBContext = _context.Useraccount.Include(u => u.UserrolesNavigation);
            return View(await orderingDBContext.ToListAsync());
        }

        // GET: Useraccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccount
                .Include(u => u.UserrolesNavigation)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (useraccount == null)
            {
                return NotFound();
            }

            return View(useraccount);
        }

        // GET: Useraccounts/Create
        public IActionResult Create()
        {
            ViewData["Userroles"] = new SelectList(_context.Userroles, "Roleid", "Roleid");
            return View();
        }

        // POST: Useraccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Username,Userpassword,Userroles,Createddate")] Useraccount useraccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(useraccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userroles"] = new SelectList(_context.Userroles, "Roleid", "Roleid", useraccount.Userroles);
            return View(useraccount);
        }

        // GET: Useraccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccount.FindAsync(id);
            if (useraccount == null)
            {
                return NotFound();
            }
            ViewData["Userroles"] = new SelectList(_context.Userroles, "Roleid", "Roleid", useraccount.Userroles);
            return View(useraccount);
        }

        // POST: Useraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Userid,Username,Userpassword,Userroles,Createddate")] Useraccount useraccount)
        {
            if (id != useraccount.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(useraccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (id != useraccount.Userid)
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
            ViewData["Userroles"] = new SelectList(_context.Userroles, "Roleid", "Roleid", useraccount.Userroles);
            return View(useraccount);
        }

        // GET: Useraccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var useraccount = await _context.Useraccount
                .Include(u => u.UserrolesNavigation)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (useraccount == null)
            {
                return NotFound();
            }

            return View(useraccount);
        }

        // POST: Useraccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var useraccount = await _context.Useraccount.FindAsync(id);
            _context.Useraccount.Remove(useraccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UseraccountExists(int id)
        {
            return _context.Useraccount.Any(e => e.Userid == id);
        }
    }
}
