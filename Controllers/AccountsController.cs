using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class AccountsController : ControllerBase
    {
        //the manager for handling user creation, deletion, etc..
        private readonly UserManager<AppUser> _userManager;
        //the manager for handling signing in and out for users
        private readonly SignInManager<AppUser> _signInManager;

        public AccountsController (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return null;
        }

        public async Task<IActionResult> LogIn(string email, string password)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if(user != null)
            {
                var signInResult = await  _signInManager.PasswordSignInAsync(user, password, false, false);
            }

            //wrong password 

            //user did not find any records matching
            return null;
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return null;
        }

        public async Task<IActionResult> Register(string firstName, string lastName, string middleName, string email, string password)
        {

            var user = new AppUser
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
            };


            //create the user
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                //sign user in here
            }


            return null;
        }

       
    }
}