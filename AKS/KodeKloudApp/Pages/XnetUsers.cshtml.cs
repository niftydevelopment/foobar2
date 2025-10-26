using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KodeKloudApp.Services;
using KodeKloudApp.Models;

namespace KodeKloudApp.Pages;

public class XnetUsersModel : PageModel
{
    private readonly ILogger<XnetUsersModel> _logger;
    private readonly XnetUserService _xnetUserService;
    
    public List<AspNetUser> Users { get; set; } = new();
    public int UserCount { get; set; }
    public string DatabaseStatus { get; set; } = "Unknown";

    public XnetUsersModel(ILogger<XnetUsersModel> logger, XnetUserService xnetUserService)
    {
        _logger = logger;
        _xnetUserService = xnetUserService;
    }

    public async Task OnGetAsync()
    {
        try
        {
            // Test database connection and get users
            Users = await _xnetUserService.GetAllUsersAsync();
            UserCount = await _xnetUserService.GetUserCountAsync();
            DatabaseStatus = "Connected";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Xnet database connection failed");
            DatabaseStatus = $"Error: {ex.Message}";
        }
    }

    public async Task<IActionResult> OnPostCreateUserAsync(string firstName, string lastName, string countryCode, string nationalNumber, string? email)
    {
        try
        {
            await _xnetUserService.CreateUserAsync(firstName, lastName, countryCode, nationalNumber, email);
            
            TempData["Message"] = $"User {firstName} {lastName} created successfully!";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create Xnet user");
            TempData["Error"] = $"Failed to create user: {ex.Message}";
        }

        return RedirectToPage();
    }
}
