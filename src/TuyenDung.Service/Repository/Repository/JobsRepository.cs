using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.DataContext;
using TuyenDung.Data.Model;
using TuyenDung.Service.Repository.Interface;

namespace TuyenDung.Service.Repository.Repository
{
    public class JobsRepository : IJobsInterface
    {
        private readonly MyDb _dbContext;
        public JobsRepository(MyDb dbContext)
        {
            _dbContext = dbContext;
        }
        public Jobs GetById(int EmployerId)
        {
            return _dbContext.Jobs.FirstOrDefault(x => x.Id == EmployerId);
        }
    }
}
