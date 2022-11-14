using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportSystem.Models;

public class Point
{
    [Key, Column("PointId")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid PointId { get; set; }
        
    [Required(ErrorMessage = "{0} moet ingevuld worden.")]
    [Display(Name = "Naam*")]
    public string? Name { get; set; }
        
    [Column("ApplicationUserId")]
    [ForeignKey("ApplicationUserId")]
    public string? ApplicationUserId { get; set; }
        
    [ReadOnly(true)] public ApplicationUser? ApplicationUser { get; set; }
    
    [NotMapped]
    public virtual ICollection<Option>? Option { get; set; }
    
}