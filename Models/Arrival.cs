using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeZoneBack.Models
{
    public class Arrival
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public double Price { get; set; }
        public bool IsDeactive { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
