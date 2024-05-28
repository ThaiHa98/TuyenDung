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
    public class EmployersRepository : IEmployersInterface
    {
        private readonly MyDb _dbContext;
        public EmployersRepository(MyDb dbContext)
        {
            _dbContext = dbContext;
        }
        public Employers GetById(int id)
        {
            return _dbContext.Employers.FirstOrDefault(x => x.Id == id);
        }
    }
}
