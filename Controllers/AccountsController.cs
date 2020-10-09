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


        /**/
        /*
        * NAME:
        *      Index
        * SYNOPSIS:
                Index()
        * DESCRIPTION:
                This function simply returns HTTP status code 200 if the user is already authorized, returns 401 if not
        * RETURNS
                an HTTP response signalling success or unauthorized
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/06/2020 
        */
        /**/
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

        /**/
        /*
        * NAME:
        *      LogIn
        * SYNOPSIS:
                LogIn(RegisterUserModel rUser)
                    rUser --> an instance of the RegisterUserModel class, which holds the information on the user that has
                            registered for a ProjectManager account
        * DESCRIPTION:
                Accepts an HTTP POST request, extracts values for the attributes of the RegisterUserModel object from the post request
                    body, and then uses the values stores in the objet to sign the user in
        * RETURNS
                an HTTP response signalling success or failure
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/06/2020 
        */
        /**/
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

        /**/
        /*
        * NAME:
        *      LogOut
        * SYNOPSIS:
                LogOut()
        * DESCRIPTION:
                Accepts an HTTP GET request, and signs the user out
        * RETURNS
                an HTTP response signalling success 
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/06/2020 
        */
        /**/
        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            //sign user out
            await _signInManager.SignOutAsync();
            return Ok();
        }

        /**/
        /*
        * NAME:
        *      Register
        * SYNOPSIS:
                Register(RegisterUserModel rUser)
                    rUser --> an instance of the RegisterUserModel class, which holds the information on the user that has
                            registered for a ProjectManager account
        * DESCRIPTION:
                Accepts an HTTP POST request, extracts values for the attributes of the RegisterUserModel object from the post request
                    body, and then uses the object to create a user account, and then sign the user in
        * RETURNS
                an HTTP response signalling success or failure
        * AUTHOR
        *      Biplab Thapa Magar
        * DATE
        *      10/06/2020 
        */
        /**/

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