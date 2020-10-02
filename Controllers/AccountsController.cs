using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Models;
using ProjectManager.ProjectManagarUtilities;

namespace ProjectManager.Controllers
{
    [Route("/account")]
    [ApiController]
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

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody]RegisterUser rUser)
        {

            var user = await _userManager.FindByNameAsync(rUser.userName);

            //if the email is not registered
            if(user == null)
            {
                return NotFound();
            } // if valid email was not found 
             
            //attempt signing in
            var signInResult = await  _signInManager.PasswordSignInAsync(user, rUser.password, false, false);
            
            if(!signInResult.Succeeded) {
                return Unauthorized("Email and password do not match");
            } 
            //if sign in was successful, return Ok
            else {
                return Ok();
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            //sign user out
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser rUser)
        {

            // first, check if the email has already been used
            var checkUser = await _userManager.FindByNameAsync(rUser.userName);
            if(checkUser != null) 
            {
                return Unauthorized("Email has already been used to create an account.");
            }
            // create the user model
            var user = new AppUser
            {
                UserName = rUser.userName,
                FirstName = rUser.firstName,
                LastName = rUser.lastName,
            };

            //create the user
            var result = await _userManager.CreateAsync(user, rUser.password);

            //if creation was successful
            if (result.Succeeded)
            {
                //now, sign in the created user
                var signInResult = await  _signInManager.PasswordSignInAsync(user, rUser.password, false, false);
            
                if(!signInResult.Succeeded) {
                    return Unauthorized();
                } 
                //if sign in was successful, return Ok
                else {
                    return Ok();
                }
            } 
            return Unauthorized("Unable to register user using the given credentials.");

        }
       
    }
}