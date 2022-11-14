using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ReportSystem.Models;

public class ApplicationUser : IdentityUser
{
    [NotMapped] public virtual ICollection<Point>? Points { get; set; }
}