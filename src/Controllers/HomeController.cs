using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using src.Model.Data;
using src.Model.Repo;
using src.Services.Account;

namespace src.Controllers;

[Controller]
public class HomeController : Controller{
  public HomeController(IAccountManager acctMgr, IAuthenticationManager authMgr, WarrantyrepoContext dbCtx){
    _accountManager = acctMgr;
    _authManager = authMgr;
    _dbContext = dbCtx;
  }
  private IAccountManager _accountManager;
  private IAuthenticationManager _authManager;
  private WarrantyrepoContext _dbContext;
  // pages
  [Route("/")]
  public IActionResult Index(){
    ViewData["Title"] = "Homepage";
    return View();
  }
  [Route("/Error")]
  public IActionResult Error(){
    ViewData["Title"] = "Error";
    return View();
  }
  // api
}