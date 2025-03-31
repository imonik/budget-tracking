using System;
using System.Collections.Generic;

namespace FinanceAndBudgetTracking.Models;

public partial class AppUser
{
    public int AppUserId { get; set; }

    public string Email { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] SaltHash { get; set; } = null!;

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
