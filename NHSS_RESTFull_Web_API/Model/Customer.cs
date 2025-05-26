using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NHSS_RESTFull_Web_API.Model
{
    public class Customer
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("firstname")]
        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }

        [JsonPropertyName("surname")]
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }
    }
}