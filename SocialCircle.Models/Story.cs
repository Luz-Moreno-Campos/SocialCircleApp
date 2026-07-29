using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
// using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SocialCircle.Models;

public partial class Story
{
    [Key]
    public long StoryId { get; set; }

    [Required]
    public long UserId { get; set; }

    [Required(ErrorMessage = "Input Story text")]
    public string? StoryText { get; set; }

    public DateTime CreationTimestamp { get; set; }
    public DateTime? ExpirationDate { get; set; }
    
    public virtual User? User { get; set; } 

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
