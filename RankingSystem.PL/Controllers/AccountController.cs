using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RankingSystem.DAL.Models;
using RankingSystem.PL.ViewModels;
using System.Security.Claims;

namespace RankingSystem.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		/*RoleManager and SignInManager like the UserManger is the Repo Who Has CRUD Operation For IdentityUser
         to Use It You Must Allow Dependency Injection For it by
         Go to Configure Services and Add "builder.Services.AddAuthentication()" 
         This fuction is allow the Dep-Inj for (UserManger,RoleManger,SignInManager) */

		public AccountController(UserManager<ApplicationUser> userManager ,
                                 SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
			_signInManager = signInManager;
		}


		#region Register
		[HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) // Server Validation
            {
                var User = new ApplicationUser()
                {
                    UserName = model.Email.Split("@")[0],
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree
                };
                /* CreateAsync ==> inside it uses Services and their Services Has interface 
                 and a class that implement this interface
                 to Use Create Async you Must Add the Services Witch it Dependent on
                 Go To Configure services before AddAuthentication() 
                 builder.Services.AddIdentity<theUserClass,TheRoleClass>(options to configure the password or any prop)
                                 .AddEntityFrameWorkStores<The DbContext Which th Idenetity Work on it>();*/

                var Result = await _userManager.CreateAsync(User, model.Password);
                if (Result.Succeeded)
                {
                    /* if The Data are True and it added successfully take it to login action
                    who return a login view and has a form to submit your mail and pass then
                     if all login data true redirect it to index for item controller*/
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }


            }

            return View(model);
        }
        #endregion


        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var Result = await _userManager.CheckPasswordAsync(User, model.Password);
                    if (Result)
                    {
                        var LoginResult = await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);

                        if (LoginResult.Succeeded)
                            return RedirectToAction("Index", "Item");
                    }
                    else
                        ModelState.AddModelError(string.Empty, "Password is InCorrect");
                }
                else
                    ModelState.AddModelError(string.Empty, "Email is Not Exist");
            }
            return View(model);
        } 
        #endregion

        //Log Out
        public new async Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        //Forget Passwrd
        //Reset Password

    }
}
