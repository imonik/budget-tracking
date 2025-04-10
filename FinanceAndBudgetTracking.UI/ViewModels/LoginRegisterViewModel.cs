using System.ComponentModel.DataAnnotations;
using FinanceAndBudgetTracking.Models.DTO;
using Microsoft.AspNetCore.Identity.Data;

namespace FinanceAndBudgetTracking.UI.ViewModels
{
    public class LoginRegisterViewModel
    {
        public LoginRequestDTO? LoginRequest { get; set; }
        public UserDTO? LoginResponse { get; set; }
        public RegisterDTO? LoginRegisterDTO { get; set; }
        public RegisterDTO? LoginRegister { get; internal set; }
    }
}
