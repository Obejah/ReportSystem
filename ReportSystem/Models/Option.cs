using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportSystem.Models;

public class Option
{
    [Key, Column("OptionId")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid OptionId { get; set; }
    
    [Required]
    public string? OptionName { get; set; }
    [Required]
    public int Urgency { get; set; }

    /*[Key]
    [Column("PointId")]
    [ForeignKey("PointId")]*/
    public Guid PointId { get; set; }

    public Point? Point { get; set; }
    
    [NotMapped]
    public virtual ICollection<Notice>? Notice { get; set; }
    
}