using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyenDung.Data.Model
{
    public class Job_seekers //Người tìm việc
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string User_Name { get; set; }
        public string Image { get; set; }
        public string DateofBirth { get; set; }
        public string Gender { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
