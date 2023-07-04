using System.Collections.ObjectModel;
using src.Model.Data.Account;
using src.Model.Repo;

namespace src.Services.Account;

public interface IAccountManager{
  public Task<string> CreateUser(UserDto userInput);
  public Task<string> DeleteUser(User user);
  public Task<string> EditUser(User user, UserDto userInput);
  public Task<User>? FindUser(string userId);
  public IEnumerable<User> GetUsers(int page, int count);
  public bool VerifyCode(string userCode, string code);
  public Task<bool> IsEmailTaken(string email);
}