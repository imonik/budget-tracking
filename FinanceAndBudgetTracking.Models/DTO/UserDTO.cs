namespace FinanceAndBudgetTracking.Models.DTO
{
    public class UserDTO
    {
        public int AppUserId { get; set; }
        public string Email { get; set; } = null!;
        public string? Name { get; set; }

        public bool Success { get; set; }
    }
}
