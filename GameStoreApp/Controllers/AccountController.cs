using GameStoreApp.Data;
using GameStoreApp.Data.Static;
using GameStoreApp.Data.ViewModel;
using GameStoreApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<GameStoreUser> _userManager;
        private readonly SignInManager<GameStoreUser> _signInManager;
        private readonly GameStoreAppDbContext _context;

        public AccountController(UserManager<GameStoreUser> userManager, SignInManager<GameStoreUser> signInManager,  GameStoreAppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var data = await _context.Users.ToListAsync();
            return View(data);
        }

        public IActionResult Login()
        {
            var response = new LoginVM();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) {
                return View(loginVM);
            }

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress!);
            if (user != null) 
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, loginVM.Password!);
                if (checkPassword)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password!, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Game");
                    }
                }
                TempData["Error"] = "The Email or Password is incorrect. Try Again!";
                return View(loginVM);
            }

            TempData["Error"] = "The Email or Password is incorrect. Try Again!";
            return View(loginVM);
        }

        public IActionResult Register()
        {
            var response = new RegisterVM();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress!);
            if(user != null)
            {
                TempData["Error"] = registerVM.EmailAddress + " is already registered";
                return View(registerVM);
            }

            var newUser = new GameStoreUser()
            {
                firstName = registerVM.FirstName!,
                lastName = registerVM.FamilyName!,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password!);

            if (newUserResponse.Succeeded) 
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return View("RegistrationSuccess");
            }

            TempData["Error"] = "Password must contain: At least 6 characters, OneUpperCase, OneLowerCase, OneNumber, Symbol as [$#@]";
            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Game");
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            return View();
        }

    }
}
