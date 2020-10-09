/* AccountsContoller.cs
 This file contains the AccountsController class, which represents one of the three controller classes in the ProjectManager project
 A controller class in ASP.NET Core is responsible for defining and handling HTTP requests to specific routes. The AccountsController class handles
 routes and HTTP requests relating to user accounts in the ProjectManager Web Application
*/

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Models;
using ProjectManager.Models.UtilityModels;


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

        /**/
        /*
        * NAME:
        *      AccountsController - constructor for the AccountsController class
        * SYNOPSIS:
                AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
                    userManager --> an instance of the UserManager class, which is a class provided by Asp.Net Core Identity to handle
                                    use registration and retrieval
                    signInManager --> an instance of the SignInManager class, which is a class provided by Asp.Net Core Identity to handle
                                    user sign in         
        * DESCRIPTION:
                Initializes the AccountsController class
        * RETURNS
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/06/2020 
        * /
        /**/
        public AccountsController (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //simply returns status code 200 if the user is already athurized, returns 401 if not
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return Ok();
            } 
            else 
            {
                return Unauthorized();
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody]RegisterUserModel rUser)
        {

            var user = await _userManager.FindByNameAsync(rUser.userName);

            //if the email is not registered
            if(user == null)
            {
                return Unauthorized("Invalid username/password");
            } 
             
            //attempt signing in
            var signInResult = await  _signInManager.PasswordSignInAsync(user, rUser.password, false, false);
            
            if(!signInResult.Succeeded) {
                return Unauthorized("Invalid username/password");
            } 
            //if sign in was successful, return Ok
            else {
                return Ok("logged in");
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            //sign user out
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel rUser)
        {

            // first, check if the email has already been used
            var checkUser = await _userManager.FindByNameAsync(rUser.userName);
            if(checkUser != null) 
            {
                return Unauthorized("Username has already been used to create an account.");
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
                    return Ok("registered");
                }
            } 
            return Unauthorized("Unable to register user using the given credentials.");

        }
       
    }
}