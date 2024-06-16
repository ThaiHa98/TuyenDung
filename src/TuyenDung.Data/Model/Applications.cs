using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.Model.Enum;

namespace TuyenDung.Data.Model
{
    public class Applications // DonXinViec
    {
        [Key]
        public int Id { get; set; }
        public int Job_JobId { get; set; }
        public int User_UserId { get; set; }
        public StatusApplication Status { get; set; }
        public DateTime Date { get; set; }
        public StatusSubmissionType StatusSubmissionType { get; set; }
        public string CvFilePath { get; set; }
    }
}
