namespace src.Services.Account;

public interface IHasher{
  public string HashPassword(string password);
  public bool VerifyHashedPassword(string hashedPassword, string password);
}