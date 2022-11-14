using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReportSystem.Models;
using ReportSystem.Repositories;

namespace ReportSystem.Pages.User;

public class Register : PageModel
{
    private readonly UserRepository _userRepository;

    public Register(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [BindProperty] public RegisterUser RegisterUser { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var result = await _userRepository.RegisterUser(RegisterUser);


        if (!result.Succeeded)
        {
            result.Errors.ToList().ForEach(x =>
                ModelState.AddModelError("", x.Description ?? "Er is helaas iets mis gegaan."));
            return Page();
        }

        else
        {
            return RedirectToPage("/User/Login");
        }
    }
}