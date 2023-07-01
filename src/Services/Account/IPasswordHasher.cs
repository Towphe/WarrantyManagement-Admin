namespace src.Services.Account;

public interface IPasswordHasher{
  public string HashPassword(string password);
  public bool VerifyHashedPassword(string hashedPassword, string password);
}