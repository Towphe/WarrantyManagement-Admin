using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using src.Model.Data;
using src.Model.Data.Catalog;
using src.Model.Repo;
using src.Services.Account;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace src.Controllers;

[Controller]
[Route("/[controller]")]
//[TypeFilter(typeof(AuthorizationFilter))]
public class CatalogController : Controller{
  public CatalogController(IAccountManager acctMgr, IAuthenticationManager authMgr, WarrantyrepoContext dbCtx){
    _accountManager = acctMgr;
    _authManager = authMgr;
    _dbContext = dbCtx;
  }
  private IAccountManager _accountManager;
  private IAuthenticationManager _authManager;
  private WarrantyrepoContext _dbContext;
  
  // pages
  public IActionResult Index(){
    ViewData["Title"] = "Catalog";
    return View();
  }
  [Route("Products")]
  public IActionResult Products([FromQuery] int count = 10, [FromQuery] int page = 1){
    ViewData["Title"] = "Products";
    IEnumerable<Product> products = _dbContext.Products.Where(p => p.Deleted == false).Skip((page - 1) * count).Include(p => p.Distributor).Take(count).AsEnumerable();
    TempData["productCount"] = _dbContext.Products.Count().ToString();
    return View(products);
  }
  [Route("Products/{id}")]
  public async Task<IActionResult> Product([FromRoute] int id){
    ViewData["Title"] = $"Product# {id.ToString()}";
    Product? product = await _dbContext.Products.Where(p => p.Id == id).Include(p => p.Distributor).FirstOrDefaultAsync();
    return View(product);
  }
  [Route("Merchants")]
  public IActionResult Merchants([FromQuery] int count = 10, [FromQuery] int page = 1){
    ViewData["Title"] = "Merchants";
    IEnumerable<Merchant> merchants = _dbContext.Merchants.Where(p => p.Deleted == false).Skip((page - 1) * count).Take(count).AsEnumerable();
    return View(Merchants);
  }
  [Route("Merchants/{id}")]
  public async Task<IActionResult> Merchant([FromRoute] int id){
    Merchant merchant = await _dbContext.Merchants.FindAsync(id);
    return View(merchant);
  }
  [Route("Distributors")]
  public IActionResult Distributors([FromQuery] int count = 10, [FromQuery] int page = 1){
    ViewData["Title"] = "Distributors";
    IEnumerable<Distributor> distributors = _dbContext.Distributors.Skip((page - 1) * count).Take(count).AsEnumerable();
    return View(distributors);
  }
  [Route("Distributors/{id}")]
  public async Task<IActionResult> Distributor([FromRoute] int id){
    Distributor distributor = await _dbContext.Distributors.FindAsync(id);
    return View(distributor);
  }
  // api
  [HttpPost]
  [Route("Products/Add")]
  public async Task<int> AddProduct([FromBody] ProductDto productDto){
    Product product = new Product(){
      Name = productDto.Name,
      CategoryId = productDto.CategoryId,
      DistributorId = productDto.DistributorId,
      MerchantId = productDto.MerchantId,
      DateAdded = DateOnly.FromDateTime(DateTime.Now)
    };
    await _dbContext.Products.AddAsync(product);
    await _dbContext.SaveChangesAsync();
    return 200;
  }
  [HttpDelete]
  [Route("Products/Delete/{id}")]
  public async Task<int> DeleteProduct([FromRoute] int id){
    Product? product = await _dbContext.Products.FindAsync(id);
    if (product == null){
      return 404;
    }
    product.Deleted = true;
    await _dbContext.SaveChangesAsync();
    return 200;
  }
  [HttpPost]
  [Route("Merchants/Add")]
  public async Task<int> AddMerchant([FromBody] MerchantDto merchantDto){
    Merchant merchant = new Merchant(){
      Name = merchantDto.Name,
      Company = merchantDto.Company,
      Platform = merchantDto.Platform,
      Uniq = merchantDto.Uniq,
      DateAdded = DateOnly.FromDateTime(DateTime.Now),
      Deleted = false
    };
    await _dbContext.Merchants.AddAsync(merchant);
    await _dbContext.SaveChangesAsync();
    return 200;
  }
  [HttpDelete]
  [Route("Merchants/Delete/{id}")]
  public async Task<int> DeleteMerchant([FromRoute] int id){
    Merchant? merchant = await _dbContext.Merchants.FindAsync(id);
    if (merchant == null){
      return 404;
    }
    merchant.Deleted = true;
    await _dbContext.SaveChangesAsync();
    return 200;
  }
  [HttpPost]
  [Route("Distributors/Add")]
  public async Task<int> AddDistributor([FromBody] DistributorDto distributorDto){
    Distributor distributor = new Distributor(){
      Name = distributorDto.DistributorName,
      DateAdded = DateOnly.FromDateTime(DateTime.Now)
    };
    await _dbContext.Distributors.AddAsync(distributor);
    await _dbContext.SaveChangesAsync();
    return 200;
  }
  [HttpDelete]
  [Route("Distributors/Delete/{id}")]
  public async Task<int> DeleteDistributor([FromRoute] int id){
    Distributor? distributor = await _dbContext.Distributors.FindAsync(id);
    if (distributor == null){
      return 404;
    }
    distributor.Deleted = true;
    await _dbContext.SaveChangesAsync();
    return 200;
  }
  [HttpPost]
  [Route("Categories/Add")]
  public async Task<int> AddCategory([FromBody] CategoryDto categoryDto){
    Category category = new Category(){
      Type = categoryDto.Type,
      Subtype = categoryDto.SubType,
      Uniq = categoryDto.Uniq,
      DateAdded = DateOnly.FromDateTime(DateTime.Now)
    };
    await _dbContext.Categories.AddAsync(category);
    await _dbContext.SaveChangesAsync();
    return 200;
  }
  [HttpDelete]
  [Route("Categories/Delete/{id}")]
  public async Task<int> DeleteCategory([FromRoute] int id){
    Category? category = await _dbContext.Categories.FindAsync(id);
    if (category == null){
      return 404;
    }
    category.Deleted = true;
    await _dbContext.SaveChangesAsync();
    return 200;
  }
}