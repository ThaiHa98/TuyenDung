using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;

namespace TuyenDung.Service.Services.InterfaceIServices
{
    public interface IJobsIService
    {
        public Jobs Create (JobsDto jobsDto);
        Jobs Update (JobsUpdateDto jobsUpdateDto);
        bool Delete (int id);
    }
}
