using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReportSystem.Models;
using ReportSystem.Repositories;

namespace ReportSystem.Pages.User;

public class Login : PageModel
{
    private readonly UserRepository _userRepository;

    public Login(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [BindProperty] public LoginUser? LoginUser { get; set; }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var result = await _userRepository.Login(LoginUser);

            if (result.Succeeded)
                return RedirectToPage("/Index");
        }
        ModelState.AddModelError("", "Gebruikersnaam of Wachtwoord is incorrect");
    
        return Page();
    }

}