using System;
using System.Collections.Generic;

namespace src.Model.Repo;

public partial class Entry
{
    public string Id { get; set; } = null!;

    public int ProductId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ReferenceNum { get; set; } = null!;

    public string SalesOrderNum { get; set; } = null!;

    public string? Variation { get; set; }

    public string? MediaDir { get; set; }

    public DateOnly DatePurchased { get; set; }

    public DateOnly DateAdded { get; set; }

    public string? Description { get; set; }

    public string Store { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
