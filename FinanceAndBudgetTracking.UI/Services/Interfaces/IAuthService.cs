﻿using FinanceAndBudgetTracking.Models;
using FinanceAndBudgetTracking.Models.DTO;
using FinanceAndBudgetTracking.UI.ViewModels;

namespace FinanceAndBudgetTracking.UI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserDTO> LoginAsync(LoginRequestDTO login);
        Task<bool> RegisterAsync(RegisterDTO register);
        Task<bool> LogoutAsync();
    }
}
