﻿using RunGroopWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroopWebApp.Models
{
    public class Club
    {
        [Key]
        public int Id { get; set; } // Khóa chính
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image {  get; set; } //  lưu đường dẫn của hình ảnh đại diện cho câu lạc bộ.
        
        [ForeignKey("Address")]
        public int AddressId { get; set; } // lưu trữ khoá ngoại ([ForeignKey("Address")]) liên kết với lớp Address.
        public Address Address { get; set; } 
        public ClubCategory ClubCategory { get; set; } // biểu diễn mối quan hệ giữa Club và ClubCategory.
        
        [ForeignKey("AppUser")]
        public string? AppUserId {  get; set; } //cho phép giá trị của thuộc tính này có thể là null.
        public AppUser AppUser { get; set; }
    }
}
