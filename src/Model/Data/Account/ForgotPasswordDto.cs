namespace src.Model.Data.Account;

public class ForgotPasswordDto{
  public string? UserToken {get; set;}
  public string? Password {get; set;}
  public string? ConfirmPassword {get; set;}
}