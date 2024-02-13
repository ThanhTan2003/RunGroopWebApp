using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroopWebApp.ViewModels
{
    public class EditRaceViewModel
    {
        public int Id { get; set; } // Khóa chính
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; } //  lưu đường dẫn của hình ảnh đại diện cho câu lạc bộ.
        public string ? URL { get; set; }
        public int AddressId { get; set; } // lưu trữ khoá ngoại ([ForeignKey("Address")]) liên kết với lớp Address.
        public Address Address { get; set; }
        public RaceCategory RaceCategory { get; set; } // biểu diễn mối quan hệ giữa 
    }
}
