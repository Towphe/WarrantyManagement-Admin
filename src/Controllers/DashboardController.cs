using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using src.Model.Data;
using src.Model.Repo;
using src.Services.Account;

namespace src.Controllers;

[Controller]
[Route("/[controller]")]
[TypeFilter(typeof(AuthorizationFilter))]
public class DashboardController : Controller{
  public DashboardController(IAccountManager acctMgr, IAuthenticationManager authMgr, WarrantyrepoContext dbCtx){
    _accountManager = acctMgr;
    _authManager = authMgr;
    _dbContext = dbCtx;
  }
  private IAccountManager _accountManager;
  private IAuthenticationManager _authManager;
  private WarrantyrepoContext _dbContext;
  // pages
  [Route("index")]
  public IActionResult Index(){
    ViewData["Title"] = "Dashboard";
    return View();
  }
  // api
}