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
    public class FormCvRepository : IFormCvInterface
    {
        private readonly MyDb _dbContext;
        public FormCvRepository(MyDb dbContext)
        {
            _dbContext = dbContext;
        }
        public List<FormCv> GetFormCv()
        {
            try
            {
                return _dbContext.FormCvs.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi xảy ra khi lấy danh sách FormCv: {ex.Message}");
                return new List<FormCv>();
            }
        }

        public FormCv GetFormCvId(int id)
        {
            var query = _dbContext.FormCvs.FirstOrDefault(x => x.UserId == id);
            if(query == null)
            {
                throw new Exception("Id not found");
            }
            return query;
        }
    }
}
