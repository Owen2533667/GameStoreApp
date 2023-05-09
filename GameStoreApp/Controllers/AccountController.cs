﻿using GameStoreApp.Data;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="context">The db context.</param>
        public AccountController(UserManager<GameStoreUser> userManager, SignInManager<GameStoreUser> signInManager,  GameStoreAppDbContext context)
        {
            // Assigning the passed parameters to the private fields
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        /// Returns a list of users asynchronously.
        /// </summary>
        /// <returns> The users view with the list of users.</returns>
        public async Task<IActionResult> Users()
        {
            // Get the list of users from the database.
            var data = await _context.Users.ToListAsync();

            // Return the view with the list of users.
            return View(data);
        }

        /// <summary>
        /// Returns the login view for the user.
        /// </summary>
        /// <returns>The login view with a new loginVM</returns>
        public IActionResult Login()
        {
            // Create a new instance of the LoginVM view model.
            var response = new LoginVM();

            // Return the login view with the LoginVM view model.
            return View(response);
        }

        /// <summary>
        /// Logs in the user asynchronously.
        /// </summary>
        /// <remarks>This method is called when an HTTP POST request is sent from the login view form when submitted by user.</remarks>
        /// <param name="loginVM">The login view model.</param>
        /// <returns>The game index view if login was successful, otherwise the login view with login view model.</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            // Check if the model state is not valid.
            if (!ModelState.IsValid) {
                return View(loginVM); // if the model is not valid then return login view with the login view model object.
            }

            // Find the user by email address.
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress!);
            // If the user is not null
            if (user != null) 
            {
                // Check if the password is correct.
                var checkPassword = await _userManager.CheckPasswordAsync(user, loginVM.Password!); // Uses the _userManger.CheckPassword to check if the password submiited is correct.
                if (checkPassword) // If true
                {
                    // Sign in the user and redirect to the game index view.
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password!, false, false); // This attempts to sign in the user with the password provided.
                    if (result.Succeeded) // If this is true, then sign was successful.
                    {
                        return RedirectToAction("Index", "Game"); // Redirect the user to the Index action of the game controller.
                    }
                }
                TempData["Error"] = "The Email or Password is incorrect. Try Again!"; // Send this error message back to login view to be displayed.
                return View(loginVM); // If the password check was false, meaning it did not match, return the login view and send the login view model object back.
            }

            // Set an error message and return the login view with the LoginVM view model
            TempData["Error"] = "The Email or Password is incorrect. Try Again!";
            return View(loginVM); 
        }

        /// <summary>
        /// This method is used to return the resigster view to the user along with an new instance of a Register view model.
        /// </summary>
        /// <returns>Returns the register view with an empty RegisterVM object</returns>
        public IActionResult Register()
        {
            var response = new RegisterVM(); // Create a new instance of RegisterVM.

            return View(response); // Return the view with the empty RegisterVM object.
        }

        /// <summary>
        /// This method will be used to register a new user to the game store application asynchronously. 
        /// </summary>
        /// <remarks>This method is called when an HTTP POST request is sent from the register form when submitted by user trying to register.</remarks>
        /// <param name="registerVM">The RegisterVM object containing the user's registration information.</param>
        /// <returns>If the registeration was successful, then return the view RegistrationSuccess. Otherwise, return user back to register view with error message.</returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            // Checks to see if the model state is not valid
            if (!ModelState.IsValid)
            {
                // If it's not valid, return the view register with the current RegisterVM object.
                return View(registerVM); 
            }

            // Check if the user already exists.
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress!); // Get user from database by using the email.
            if(user != null) // If this does not equal null, meaning that a user already exists.
            {
                TempData["Error"] = registerVM.EmailAddress + " is already registered"; // Set error message to be displayed
                return View(registerVM); // return the view register with the current RegisterVM object.
            }

            // Create a new GameStoreUser object with the registration information.
            var newUser = new GameStoreUser()
            {
                firstName = registerVM.FirstName!,
                lastName = registerVM.FamilyName!,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };

            // Create the new user and check if it succeeded.
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password!);

            if (newUserResponse.Succeeded) 
            {
                // Add the user to the User role and return the RegistrationSuccess view.
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return View("RegistrationSuccess");
            }

            // If it didn't succeed, set an error message and return the view with the current RegisterVM object.
            TempData["Error"] = "Password must contain: At least 6 characters, OneUpperCase, OneLowerCase, OneNumber, Symbol as [$#@]";
            return View(registerVM);
        }

        /// <summary>
        /// Logs out the user asynchronously.
        /// </summary>
        /// <remarks>This method is called when an HTTP POST request is sent from the logout button on the nav bar is pressed by user.</remarks>
        /// <returns>The game index view.</returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Signs out the user asynchronously.
            await _signInManager.SignOutAsync();

            // Redirects to the game index view.
            return RedirectToAction("Index", "Game");
        }

        /// <summary>
        /// Displays the access denied view.
        /// </summary>
        /// <param name="returnUrl">The URL to return to after login.</param>
        /// <returns>The access denied view.</returns>
        public IActionResult AccessDenied(string returnUrl)
        {
            // Returns the access denied view.
            return View();
        }

    }
}
