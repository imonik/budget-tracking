namespace FinanceAndBudgetTracking.UI.ViewModels
{
    public class AuthResponse
    {
        public int AppUserId { get; set; }
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public bool Success { get; set; }
    }
}
