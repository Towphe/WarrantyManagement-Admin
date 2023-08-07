using System;
using System.Collections.Generic;

namespace src.Model.Repo;

public partial class Product
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int MerchantId { get; set; }

    public int DistributorId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly DateAdded { get; set; }

    public string? Variants { get; set; }

    public bool Deleted { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Distributor Distributor { get; set; } = null!;

    public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();

    public virtual Merchant Merchant { get; set; } = null!;
}
