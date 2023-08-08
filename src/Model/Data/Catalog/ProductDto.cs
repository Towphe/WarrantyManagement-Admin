using System;

namespace src.Model.Data.Catalog;

public class ProductDto{
  public string Name {get; set;}
  public int CategoryId {get; set;}
  public int MerchantId {get; set;}
  public int DistributorId {get; set;}
}