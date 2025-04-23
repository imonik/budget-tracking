using FinanceAndBudgetTracking.Models.DTO;

namespace FinanceAndBudgetTracking.UI.ViewModels
{
    public class TransactionViewModel
    {
        public List<CategoryDTO> CategoryList { get; set; }
        public List<TransactionDTO> Transactions { get; set; }

        
    }
}
