using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoApi.Models
{
    public class Status
    {
        public Status(){
            Cards = new HashSet<Card>();
        }
        public int StatusId { get; set; }
        public string Name { get; set; } = null!;

        [JsonIgnore]
        public ICollection<Card> Cards { get; set; }
    }
}