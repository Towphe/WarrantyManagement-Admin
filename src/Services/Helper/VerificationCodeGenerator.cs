namespace src.Services.Helper;
public static class VerificationCodeGenerator{
  public static string GenerateCode(){
    string code = "";
    Random rng = Random.Shared;
    for (int i=0; i<6; i++){
      code += (char)(rng.Next('A', 'Z' + 1));
    }
    return code;
  }
}