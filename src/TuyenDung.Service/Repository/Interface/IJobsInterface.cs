using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.Model;

namespace TuyenDung.Service.Repository.Interface
{
    public interface IJobsInterface
    {
        public Jobs GetById(int EmployerId);
    }
}
