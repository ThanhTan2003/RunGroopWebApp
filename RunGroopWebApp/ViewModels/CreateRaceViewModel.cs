using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RunGroopWebApp.ViewModels
{
    public class CreateRaceViewModel
    {
        [Key]
        public int Id { get; set; } // Khóa chính
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; } //  lưu đường dẫn của hình ảnh đại diện cho câu lạc bộ.

        [ForeignKey("Address")]
        public int AddressId { get; set; } // lưu trữ khoá ngoại ([ForeignKey("Address")]) liên kết với lớp Address.
        public Address Address { get; set; }
        public RaceCategory RaceCategory { get; set; } // biểu diễn mối quan hệ giữa 

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; } //cho phép giá trị của thuộc tính này có thể là null.
        public AppUser AppUser { get; set; }
    }
}
