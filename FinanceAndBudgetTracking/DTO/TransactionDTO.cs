namespace FinanceAndBudgetTracking.DTO
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }
        public bool Type { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; } = null!;
    }
}
