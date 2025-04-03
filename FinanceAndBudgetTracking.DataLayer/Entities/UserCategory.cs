using System;
using System.Collections.Generic;

namespace FinanceAndBudgetTracking.DataLayer.Entities;

public partial class UserCategory
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public int? UserId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
