using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using src.Model.Data.Account;
using src.Model.Repo;

namespace src.Services.Account;

public interface IAuthenticationManager{
  public Task<string> SignIn(UserDto userDto, HttpContext httpContext);
  public string SignOut(HttpContext httpContext);
  public Task<string> ForgotPassword(ForgotPasswordDto forgotPasswordDto);
  public Task<string> GetAuth(HttpContext httpContext);
}