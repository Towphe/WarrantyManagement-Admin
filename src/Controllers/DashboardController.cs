using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using src.Model.Data;
using src.Model.Repo;
using src.Services.Account;
using Microsoft.EntityFrameworkCore;

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
  [Route("Entries")]  
  public async Task<IActionResult> Entries([FromQuery] int count = 10, [FromQuery] int page = 1){
    ViewData["Title"] = "Entries";
    //IEnumerable<Entry> entries = _dbContext.Entries.Skip((page - 1) * count).Take(count).AsEnumerable();
    IEnumerable<Entry> entries = _dbContext.Entries.Skip((page - 1) * count).Take(count).Include(e => e.Product).AsEnumerable();
    return View(entries);
  }
  [Route("Entries/{id}")]
  public async Task<IActionResult> Entry([FromRoute] string id){
    Entry? entry = await _dbContext.Entries.Where(e => e.Id == id).Include(e => e.Product).FirstAsync();
    ViewData["Title"] = $"{id}";
    return View(entry);
  }
  // api
  [Route("Approve/{id}")]
  public async Task<IActionResult> Approve([FromRoute] string id){
    Entry? entry = await _dbContext.Entries.FindAsync(id);
    entry.Status = "ACCEPTED";
    // email/contact user (TO FOLLOW)
    await _dbContext.SaveChangesAsync();
    return RedirectToAction(nameof(Entries));
  }
  [Route("Reject/{id}")]
  public async Task<IActionResult> Reject([FromRoute] string id){
    Entry? entry = await _dbContext.Entries.FindAsync(id);
    entry.Status = "REJECTED";
    // email/contact user (TO FOLLOW)
    await _dbContext.SaveChangesAsync();
    return RedirectToAction(nameof(Entries));
  }
}