﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyenDung.Data.Model
{
    public class Employers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; } // Ngành công nghiệp
        public string CompanySize { get; set; } //Số lượng nhân viên
        public string Website {  get; set; }  //web của công ty
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactPosition { get; set; }//Liên hệ vị trí
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
    }
}