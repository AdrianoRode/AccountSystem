using AccountSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModel;
using System.Diagnostics;

namespace AccountSystem.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(Account? account)
        {
            return View(account);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Account? account)
        {
            if(account == null)
            {
                return NotFound();
            }

            var test = _context.Accounts.FirstOrDefault(x => x.Email == account.Email);
            if(test != null)
            {
                return RedirectToAction(nameof(Error), new {Message = "This email is already registered!"});
            }
            if(ModelState.IsValid)
            {
                _context.Add(account);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(Account? account) 
        {
            if(account == null) 
            {
                return NotFound();
            }
            var test = _context.Accounts.FirstOrDefault(x => x.Email == account.Email && x.Password == account.Password);
            if(test == null)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index), test);
            }
            return View();
        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = "This email is already registered" ?? HttpContext.TraceIdentifier };

            return View(viewModel);
        }
    }
}
