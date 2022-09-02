using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class Card
    {
        public int CardId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Category { get; set; } = null!;
        public string? DueTime { get; set; }
        [StringLength(20)]
        public string EstimateCount { get; set; } = null!;
        [StringLength(20)]
        public string EstimateUnit { get; set; } = null!;
        [StringLength(20)]
        public string? Importance { get; set; }

        
        public virtual Status Status { get; set; } = null!;
        public int StatusId { get; set; }

        public virtual User User { get; set; } = null!;
        public int UserId { get; set; }
        // public User User { get; set;} = null!;
    }
    
}