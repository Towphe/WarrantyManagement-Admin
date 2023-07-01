using System;
using System.Collections.Generic;

namespace src.Model.Repo;

public partial class Category
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string? Subtype { get; set; }

    public DateOnly DateAdded { get; set; }

    public string Uniq { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
