using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinanceAndBudgetTracking.UI.Models;
using FinanceAndBudgetTracking.UI.Services.Interfaces;
using FinanceAndBudgetTracking.UI.ViewModels;
using FinanceAndBudgetTracking.Models.DTO;
using Microsoft.Win32;
using System.Security.Claims;
using System.Text.Json;

namespace FinanceAndBudgetTracking.UI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAuthService _authService;

    public HomeController(ILogger<HomeController> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var response = await _authService.LoginAsync(model.LoginRequest);

        if (response == null || string.IsNullOrEmpty(response.Token))
        {
            ModelState.AddModelError(string.Empty, "Invalid credentials");
            return View("Index", "Dashboard");
        }

        // Save token and user in session
        HttpContext.Session.SetString("JWT", response.Token);
        HttpContext.Session.SetString("AuthResponseUser", JsonSerializer.Serialize(response.User));
        return RedirectToAction("Index", "Dashboard");
    }

    public async Task<IActionResult> Register(LoginRegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
     
        var result = await _authService.RegisterAsync(model.LoginRegisterDTO);
        //var viewModel = new LoginRegisterViewModel
        //{
        //    LoginResponse = result,
        //    LoginRegister = new RegisterDTO()
        //};

        if (result)
        {
            return RedirectToAction("Index", "Dashboard");
        }
        // Handle registration failure
        ModelState.AddModelError("", "Invalid registration attempt");
        return View("Index", model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
