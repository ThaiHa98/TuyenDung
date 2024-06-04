using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;

namespace TuyenDung.Service.Services.InterfaceIServices
{
    public interface IJob_seekersIService
    {
        public Job_seekers Create(Job_seekersDto job_seekersDto, IFormFile image);
        Job_seekers Update(Job_seekersDto job_seekersDto, IFormFile image);
        bool Delete(int job_seekersid);
    }
}
