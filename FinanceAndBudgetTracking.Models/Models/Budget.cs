using System;
using System.Collections.Generic;

namespace FinanceAndBudgetTracking.Models;

public partial class Budget
{
    public int BudgetId { get; set; }

    public int UserId { get; set; }

    public int CategoryId { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual AppUser User { get; set; } = null!;
}
