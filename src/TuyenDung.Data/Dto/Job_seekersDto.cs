using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyenDung.Data.Dto
{
    public class Job_seekersDto
    {
        public int Id { get; set; }
        public int UserId_Job_seekers { get; set; }
        public string Name_Job_seekers { get; set; }
        public string DateofBirth_Job_seekers { get; set; }
        public string Gender_Job_seekers { get; set; }
    }
}
