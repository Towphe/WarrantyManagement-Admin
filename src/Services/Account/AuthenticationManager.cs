using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using src.Model.Data.Account;
using src.Model.Repo;

namespace src.Services.Account;

public class AuthenticationManager : IAuthenticationManager{
  public AuthenticationManager(WarrantyrepoContext warrantyrepoContext, IPasswordHasher passwordHasher){
    _dbContext = warrantyrepoContext;
    _passwordHasher = passwordHasher;
  }
  private WarrantyrepoContext _dbContext;
  private IPasswordHasher _passwordHasher;
  public async Task<string> SignIn(UserDto userDto, HttpContext httpContext){
    User? user = await _dbContext.Users.Where(u => u.Username == userDto.Username).FirstOrDefaultAsync();
    if (user == null){
      return "UserNotFound";
    }
    if (!_passwordHasher.VerifyHashedPassword(user.Password, userDto.Password)){
      return "IncorrectPassword";
    }
    user.Password = _passwordHasher.HashPassword(userDto.Password);
    await _dbContext.SaveChangesAsync();
    httpContext.Session.SetString("admin", user.UserToken);
    return "Success";
  }
  public string SignOut(HttpContext httpContext){
    httpContext.Session.Remove("admin");
    return "Success";
  }
  public async Task<string> ForgotPassword(ForgotPasswordDto forgotPasswordDto){
    User? user = await _dbContext.Users.FindAsync(forgotPasswordDto.Id);
    if (user == null){
      return "UserNotFound";
    }
    if (forgotPasswordDto.ConfirmPassword != forgotPasswordDto.Password){
      return "PasswordsMismatch";
    }
    user.Password = _passwordHasher.HashPassword(forgotPasswordDto.Password);
    await _dbContext.SaveChangesAsync();
    return "Success";
  }
  public async Task<string> GetAuth(HttpContext httpContext){
    string? userToken = httpContext.Session.GetString("admin");
    if (userToken == null){
      return "NonExistentToken";
    }
    User? user = await _dbContext.Users.Where(u => u.UserToken == userToken).FirstOrDefaultAsync();
    if (user == null){
      return "UserNotFound";
    }
    return user.Id;
  }
}
