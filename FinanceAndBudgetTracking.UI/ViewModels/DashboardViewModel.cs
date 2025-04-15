using FinanceAndBudgetTracking.Models.DTO;

namespace FinanceAndBudgetTracking.UI.ViewModels
{
    public class DashboardViewModel
    {
        public  UserDTO User { get; set; }
        public List<CategoryDTO>? CategoryList { get; set; }
        public List<TransactionDTO>? TransactionList { get; set; }
    }
}
