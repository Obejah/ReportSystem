using System.ComponentModel.DataAnnotations;

namespace ReportSystem.Models;

public class RegisterUser
{
    [Required(ErrorMessage = "{0} moet ingevuld worden.")]
    [StringLength(50, ErrorMessage = "{0} moet tussen de {2} en {1} zitten.", MinimumLength = 6)]
    [Display(Name = "Username")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "{0} moet ingevuld worden.")]
    [DataType(dataType: DataType.Password)]
    [StringLength(50, ErrorMessage = "{0} moet tussen de {2} en {1} zitten.", MinimumLength = 6)]
    [Display(Name = "Password")]
    public string? Password { get; set; }
    
    [Required(ErrorMessage = "{0} moet ingevuld worden.")]
    [StringLength(50, ErrorMessage = "{0} moet tussen de {2} en {1} zitten.")]
    [Display(Name = "Email")]
    public string? Email { get; set; }
    
}