using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ReportSystem.Models;

public class Notice
{
    
    [Key, Column("NoticeId")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid NoticeId { get; set; }

    public string? Description { get; set; }
    public DateTime DateTime { get; set; }

    
    [Column("OptionId")]
    [ForeignKey("OptionId")]
    public Guid OptionId { get; set; }

    [ValidateNever] public Option? Option { get; set; }
}