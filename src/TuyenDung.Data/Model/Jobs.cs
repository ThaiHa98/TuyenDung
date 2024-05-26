using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.Model.Enum;

namespace TuyenDung.Data.Model
{
    public class Jobs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; } // lưu trữ tiêu đề của công việc
        public string Description { get; set; } //lưu trữ mô tả chi tiết về công việc.
        public string Requirements { get; set; }// lưu trữ các yêu cầu đối với ứng viên cho công việc.
        public decimal Salary { get; set; }//Lương
        public string Location { get; set; } //địa điểm làm việc của công việc.
        public StatusJobType  JobType {  get; set; }
        public StatusJobs Status { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime Deadline { get; set;}
    }
}
