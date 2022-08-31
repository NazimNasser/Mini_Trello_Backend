using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoApi.Models
{
    public class User
    {
        public User(){
            Cards = new HashSet<Card>();
        }
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; } = null!;

        [JsonIgnore]
        public ICollection<Card> Cards { get; set; }
    }
}