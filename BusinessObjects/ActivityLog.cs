using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects;

public partial class ActivityLog
{
    [Key]
    public int ActivityId { get; set; }
    
    [Required]
    public short UserId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Action { get; set; } = null!; // CREATE, UPDATE, DELETE, VIEW
    
    [Required]
    [MaxLength(100)]
    public string EntityType { get; set; } = null!; // NewsArticle, Category, Tag, SystemAccount
    
    [Required]
    [MaxLength(100)]
    public string EntityId { get; set; } = null!;
    
    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = null!;
    
    [Required]
    public DateTime Timestamp { get; set; }
    
    public string? OldValues { get; set; } // JSON string for tracking changes
    
    public string? NewValues { get; set; } // JSON string for tracking changes
    
    [MaxLength(100)]
    public string? IpAddress { get; set; }
    
    [MaxLength(500)]
    public string? UserAgent { get; set; }
    
    // Navigation property
    [ForeignKey("UserId")]
    public virtual SystemAccount User { get; set; } = null!;
}