using System.ComponentModel.DataAnnotations;

namespace ReportSystem.Models;

public class LoginUser
{
    public Guid id { get; set; }

    [Required(ErrorMessage = "{0} moet ingevuld worden.")]
    [StringLength(50, ErrorMessage = "{0} moet tussen de {2} en {1} zitten.", MinimumLength = 3)]
    [Display(Name = "Username")]
    public string? UserName { get; set; } 

    [Required(ErrorMessage = "{0} moet ingevuld worden.")]
    [StringLength(50, ErrorMessage = "{0} moet tussen de {2} en {1} zitten.", MinimumLength = 6)]
    [DataType(dataType: DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }
    
    [Display(Name = "Remember Me")] 
    public bool rememberMe { get; set; }
}