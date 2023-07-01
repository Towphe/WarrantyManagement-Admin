namespace src.Services;
public static class IDGenerator{
  public static string GenerateID(string type){
    Random rng = Random.Shared;
    
    for (int i=0; i<8; i++){
      type += (char)(rng.Next('A', 'Z' + 1));
    }
    return type;
  }
}