using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReportSystem.Repositories;

namespace ReportSystem.Pages.User;

public class Logout : PageModel
{
    private readonly UserRepository _userRepository;

    public Logout(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await _userRepository.Logout();
        return RedirectToPage("/Index");
    }

    public IActionResult OnPostDontLogoutAsync()
    {
        return RedirectToPage("/Index");
    }
}