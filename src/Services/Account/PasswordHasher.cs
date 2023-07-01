namespace src.Services.Account;

public class PasswordHasher : IPasswordHasher{
  public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
  // returns true if match, false if otherwise
  public bool VerifyHashedPassword(string hashedPassword, string password) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}