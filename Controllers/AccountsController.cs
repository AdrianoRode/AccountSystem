using AccountSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace AccountSystem.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Account? account)
        {
            if(account == null)
            {
                return NotFound();
            }

            var test = _context.Accounts.FirstOrDefault(x => x.Email == account.Email);
            if(test != null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _context.Add(account);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
