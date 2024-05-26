﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyenDung.Data.Model
{
    public class Job_seekers
    {
        public int Id {  get; set; }
        public int UserID { get; set; }
        public string Education { get; set; }//học vấn của người tìm việc
        public string WorkExperience { get; set; }//kinh nghiệm làm việc
        public string Skills { get; set; }// kỹ năng
        public string Certifications { get; set; }//chứng chỉ mà người tìm việc đã đạt được. 
        public string Projects { get; set; }// thông tin về các dự án mà người tìm việc đã tham gia
    }
}
