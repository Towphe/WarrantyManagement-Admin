using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using src.Model.Data;
using src.Model.Repo;
using src.Services.Account;
using src.Model.Data.Account;
using System.Linq;

namespace src.Controllers;

[Controller]
[Route("/[controller]")]
public class AccountController : Controller{
  public AccountController(IAccountManager acctMgr, IAuthenticationManager authMgr, WarrantyrepoContext dbCtx){
    _accountManager = acctMgr;
    _authManager = authMgr;
    _dbContext = dbCtx;
  }
  private IAccountManager _accountManager;
  private IAuthenticationManager _authManager;
  private WarrantyrepoContext _dbContext;
  // views
  [Route("signin")]
  public IActionResult SignInView(){
    ViewData["Title"] = "Sign In";
    return View();
  }
  
  // implement sign up later on
  /*
  [Route("signup")]
  public IActionResult SignUpView(){
    ViewData["Title"] = "Sign Up";
    return View();
  }
  */

  [TypeFilter(typeof(AuthorizationFilter))]
  [Route("edit")]
  public IActionResult EditView(){
    ViewData["Title"] = "Account Info";
    return View();
  }
  [Route("forgot-password")]
  public IActionResult ForgotPasswordViewPt1(){
    ViewData["Title"] = "Reset Password";
    return View();
  }
  // create filter for this
  [Route("reset-password")]
  public IActionResult ForgotPasswordViewPt2(){
    ViewData["Title"] = "Reset Password";
    return View();
  }
  // api

  // implement signup later on
  /*
  [HttpPost]
  [Route("signup-api")]
  public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto){
    if (signUpDto.Password != signUpDto.ConfirmPassword){
      return RedirectToAction(nameof(SignIn)); // add error that passwords mismatch
    }
    string createUserResult = await _accountManager.CreateUser(new UserDto(){
      Username = signUpDto.Username,
      Password = signUpDto.Password,
      Email = signUpDto.Email
    });
    if (createUserResult == "Success"){
      return RedirectToAction(actionName: "index", controllerName: "dashboard");
    } else{
      return RedirectToAction(actionName: "Error", controllerName: "home");
    }
  }
  */

  // to create: filter that makes sure proper body is present for request
  [HttpPost]
  [Route("signin-api")]
  public async Task<IActionResult> LogIn( UserDto userDto){
    string signInResult = await _authManager.SignIn(userDto, HttpContext);
    if (signInResult == "UserNotFound" || signInResult == "IncorrectPassword"){
      return RedirectToAction(nameof(SignInView)); // add message of incorrect login details
    }
    return RedirectToAction(controllerName: "Dashboard", actionName: "Index");
  }
  [HttpPost]
  [Route("signout-api")]
  public IActionResult LogOut(){
    _authManager.SignOut(HttpContext);
    return RedirectToAction(actionName: "index", controllerName: "home");
  }
  [TypeFilter(typeof(AuthorizationFilter))]
  [HttpPut]
  [Route("edit-api")]
  public async Task<IActionResult> EditUser(UserDto userDto){
    User? user = await _dbContext.Users.Where(u => u.Username == userDto.Username).FirstOrDefaultAsync();
    if (user == null){
      return RedirectToAction(actionName: "Error", controllerName: "Home");
    }
    string editSuccess = await _accountManager.EditUser(user ,userDto);
    return RedirectToAction(actionName: "Index", controllerName: "Dashboard");
  }
  [HttpPost]
  [Route("password-change-requested")]
  public async Task<IActionResult> RequestPasswordChange(string email){
    User? user = await _dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
    if (user == null){
      return RedirectToAction(nameof(ForgotPasswordViewPt1)); // add error here that email not in system
    }
    // email user link to change password (configure later)
    return View();
  }
}