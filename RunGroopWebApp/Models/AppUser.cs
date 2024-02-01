using System.ComponentModel.DataAnnotations;

namespace RunGroopWebApp.Models
{
    public class AppUser
    {
        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; } // List có thể thêm - sửa - xóa trong .Net Framework
        public ICollection<Race> Races { get; set; } 
        
    }
}
