using System;
using System.Collections.Generic;

namespace src.Model.Repo;

public partial class Distributor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly DateAdded { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
