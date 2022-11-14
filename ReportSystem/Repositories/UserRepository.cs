using Microsoft.AspNetCore.Identity;
using ReportSystem.Models;

namespace ReportSystem.Repositories;

public class UserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetCurrentUserId()
    {
        return _httpContextAccessor.HttpContext?.User.Claims.First().Value;
    }

    public async Task<IdentityResult> RegisterUser (RegisterUser registerUser)
    {
        if (registerUser.Password == null || registerUser.UserName == null || registerUser.Email == null) return IdentityResult.Failed();
        var user = new ApplicationUser()
        {
            UserName = registerUser.UserName,
            Email = registerUser.Email
        };

        //makes a new user
        var result = await _userManager.CreateAsync(user, registerUser.Password);

        //return
        return result;
    }
    
    public async Task<SignInResult> Login(LoginUser loginUser)
    {
        //kijkt of het wachtwoord overeen komt met de werkelijke gebruiker 
        return await _signInManager.PasswordSignInAsync(loginUser.UserName, loginUser.Password,
            loginUser.rememberMe, false);
    }
    public bool IsUserLoggedIn() => _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User.Claims.Any();

    public async Task Logout() => await _signInManager.SignOutAsync();
}