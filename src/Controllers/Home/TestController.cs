using Microsoft.AspNetCore.Mvc;
using src.Model.Data;
using src.Model.Data.Account;
using src.Model.Repo;
using src.Services.Account;

namespace src.Controllers.Home;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase{
  public TestController(WarrantyrepoContext warrantyrepoContext, IAccountManager acctMgr){
    _dbContext = warrantyrepoContext;
    _accountManager = acctMgr;
  }
  private WarrantyrepoContext _dbContext;
  private IAccountManager _accountManager;
  [HttpPost("signup")]
  public async Task<bool> SignUp(UserDto userDto){
    bool signupSuccess = await _accountManager.CreateUser(userDto);
    return signupSuccess;
  }
  [HttpPut("edit")]
  public async Task<bool> EditUser([FromQuery] string userId, [FromBody] UserDto userDto){
    User user = await _accountManager.FindUser(userId);
    if (user == null){
      return false;
    }
    bool editSucess = await _accountManager.EditUser(user, userDto);
    return editSucess;
  }
}