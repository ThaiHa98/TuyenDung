using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyenDung.Data.Model
{
    public class FormCv
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } // Tiêu đề của CV
        public string Description { get; set; } // Mô tả chi tiết về CV
        public string CvFilePath { get; set; } // Đường dẫn đến file template của CV
        public DateTime DateCreated { get; set; } // Ngày tạo CV
        public int UserId { get; set; } // ID người dùng đã tạo CV (nếu cần thiết)
    }
}
