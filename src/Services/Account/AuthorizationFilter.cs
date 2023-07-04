using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using src.Model.Repo;
using src.Services.Account;
using Microsoft.AspNetCore.Mvc.Filters;

namespace src.Services.Account;

public class AuthorizationFilter : IAsyncActionFilter{
  public AuthorizationFilter(WarrantyrepoContext dbCtx){
    _dbContext = dbCtx;
  }
  private WarrantyrepoContext _dbContext;
  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next){
    User? user = await _dbContext.Users.Where(u => u.UserToken == context.HttpContext.Session.GetString("admin")).FirstOrDefaultAsync();
    if (user == null){
      context.HttpContext.Response.Redirect("/account/signin");
    }
    await next();
  }
}