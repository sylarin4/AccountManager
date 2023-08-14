using AccountManager.Services;
using Microsoft.AspNetCore.Mvc;
using AccountManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AccountManager.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AccountsController : Controller
    {
        private readonly IDataManagerService _dataManagerService;

        public AccountsController(IDataManagerService dataManagerService)
        {
            _dataManagerService = dataManagerService;
        }

        public IActionResult Index(string? search)
        {
            List<Account> filteredAccounts;

            if (!string.IsNullOrWhiteSpace(search))
            {
                filteredAccounts = _dataManagerService.LoadAccounts()
                    .Where(a =>
                        a.Username.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        a.Firstname.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        a.Lastname.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        a.Birthplace.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        a.Residence.Contains(search, StringComparison.OrdinalIgnoreCase)
                    )
                    .ToList();
            }
            else
            {
                filteredAccounts = _dataManagerService.LoadAccounts();
            }

            return View(filteredAccounts);
        }

        public IActionResult Details(int id)
        {
            Account? account = _dataManagerService.GetAccountById(id);

            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(int id, [Bind("Id,Username,Firstname,Lastname,Birthdate,Birthplace,Residence,Password")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dataManagerService.UpdateAccount(account);
                return RedirectToAction(nameof(Index));
            }

            return View(account);
        }
    }
}
