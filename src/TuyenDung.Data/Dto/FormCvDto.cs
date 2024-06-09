using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyenDung.Data.Dto
{
    public class FormCvDto
    {
        public int Id {  get; set; }
        public string Title_ { get; set; } // Tiêu đề của CV
        public string Description_ { get; set; } // Mô tả chi tiết về CV
        public int UserId { get; set; } // ID người dùng đã tạo CV (nếu cần thiết)
    }
}
