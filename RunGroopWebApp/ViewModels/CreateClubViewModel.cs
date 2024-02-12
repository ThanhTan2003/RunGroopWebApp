using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RunGroopWebApp.ViewModels
{
    public class CreateClubViewModel
    {
        public int Id { get; set; } // Khóa chính
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; } //  lưu đường dẫn của hình ảnh đại diện cho câu lạc bộ.

        public int AddressId { get; set; } // lưu trữ khoá ngoại ([ForeignKey("Address")]) liên kết với lớp Address.
        public Address Address { get; set; }
        public ClubCategory ClubCategory { get; set; } // biểu diễn mối quan hệ giữa Club và ClubCategory.

        public string? AppUserId { get; set; } //cho phép giá trị của thuộc tính này có thể là null.
        public AppUser AppUser { get; set; }
    }
}
