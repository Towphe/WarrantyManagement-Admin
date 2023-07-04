using System.Collections.ObjectModel;
using src.Model.Data.Account;
using src.Model.Repo;
using src.Services.Helper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections;

namespace src.Services.Account;

public class AccountManager : IAccountManager{
  public AccountManager(WarrantyrepoContext warrantyrepoContext, IPasswordHasher passwordHasher){
    _dbContext = warrantyrepoContext;
    _passwordHasher = passwordHasher;
  }
  private WarrantyrepoContext _dbContext;
  private IPasswordHasher _passwordHasher;
  public async Task<bool> CreateUser(UserDto userInput){
    string id = IDGenerator.GenerateID("USR");
    User user = new User(){
      Id = id,
      Username = userInput.Username,
      Password = _passwordHasher.HashPassword(userInput.Password),
      Email = userInput.Email,
      UserToken = _passwordHasher.HashPassword(id),
      Vcode = VerificationCodeGenerator.GenerateCode()
    };  
    try{
      await _dbContext.Users.AddAsync(user);
      await _dbContext.SaveChangesAsync();
      return true;
    }
    catch{
      return false;
    }
  }
  public async Task<bool> DeleteUser(User user){
    _dbContext.Users.Remove(user);
    await _dbContext.SaveChangesAsync();
    return true;
  }
  public async Task<bool> EditUser(User user, UserDto userInput){
    user.Username = userInput.Username;
    user.Email = userInput.Email;
    if (userInput.Password != null){
      user.Password = _passwordHasher.HashPassword(userInput.Password);
    }
    await _dbContext.SaveChangesAsync();
    return true;
  }
  public async Task<User>? FindUser(string userId){
    User? user = await _dbContext.Users.FindAsync(userId);
    if (user == null){
      return null;
    }
    return user;
  }
  public IEnumerable<User> GetUsers(int page, int count) => _dbContext.Users.Take(count).Skip((page - 1) * count);
  public bool VerifyCode (string userCode, string code){
    if (userCode == code){
      return true;
    }
    return false;
  }
  public async Task<bool> IsEmailTaken(string email){
    User? checkUser = await _dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
    if (checkUser != null){
      return true;
    }
    return false;
  }
}