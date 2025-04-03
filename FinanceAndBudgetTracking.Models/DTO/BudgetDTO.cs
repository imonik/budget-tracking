namespace FinanceAndBudgetTracking.Models.DTO
{
    public class BudgetDTO
    {
        public int BudgetId { get; set; }
//        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
  //      public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
