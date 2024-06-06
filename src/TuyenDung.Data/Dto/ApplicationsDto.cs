using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.Model.Enum;

namespace TuyenDung.Data.Dto
{
    public class ApplicationsDto
    {
        public int _Id { get; set; }
        public int _JobId { get; set; }
        public int _UserId { get; set; }
        public string CvFilePath { get; set; } // Chi tiết gửi CV
    }
}
