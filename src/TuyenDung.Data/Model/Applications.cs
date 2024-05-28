using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.Model.Enum;

namespace TuyenDung.Data.Model
{
    public class Applications //DonXinViec
    {
        public int Id { get; set; }
        public int jobId { get; set; }
        public int UserId { get; set; }
        public int ResumeID { get; set; }
        public StatusApplication Status {  get; set; }
        public DateTime Date {  get; set; }
        public StatusSubmissionType StatusSubmissionType { get; set; }
        public string OfflineSubmissionDetails { get; set; }// chi tiết về ứng dụng
    }
}
