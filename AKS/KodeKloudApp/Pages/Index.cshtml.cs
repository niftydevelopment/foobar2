using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KodeKloudApp.Services;
using KodeKloudApp.Models;

namespace KodeKloudApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserService _userService;
    
    public string IPAddress { get; set; }
    public string Messsage { get; set; }
    public List<User> Users { get; set; } = new();
    public int UserCount { get; set; }
    public string DatabaseStatus { get; set; } = "Unknown";

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, IHttpContextAccessor contextAccessor, UserService userService)
    {
        _logger = logger;
        _configuration = configuration;
        _contextAccessor = contextAccessor;
        _userService = userService;
    }

    public async Task OnGetAsync()
    {
        Messsage = _configuration["Message"] ?? "Hello World";
        IPAddress = _contextAccessor.HttpContext.Connection.LocalIpAddress.ToString();

        try
        {
            // Test database connection and get users
            Users = await _userService.GetAllUsersAsync();
            UserCount = await _userService.GetUserCountAsync();
            DatabaseStatus = "Connected";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database connection failed");
            DatabaseStatus = $"Error: {ex.Message}";
        }
    }

    public async Task<IActionResult> OnPostCreateUserAsync()
    {
        try
        {
            // Create a sample user
            var random = new Random();
            var firstName = $"User{random.Next(1000, 9999)}";
            var lastName = "Test";
            var email = $"{firstName.ToLower()}@example.com";

            await _userService.CreateUserAsync(firstName, lastName, email);
            
            TempData["Message"] = $"User {firstName} {lastName} created successfully!";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create user");
            TempData["Error"] = $"Failed to create user: {ex.Message}";
        }

        return RedirectToPage();
    }
}