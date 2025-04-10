using System.ComponentModel.DataAnnotations;

namespace FinanceAndBudgetTracking.UI.ViewModels
{
    public class LoginViewModel
    {
        [Required] public required string Email { get; set; }
        [Required][DataType(DataType.Password)] public required string Password { get; set; }
    }
}
