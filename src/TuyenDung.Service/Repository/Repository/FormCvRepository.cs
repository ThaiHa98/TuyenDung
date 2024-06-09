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
            string directoryPath = @"C:\Users\Xuanthai98\OneDrive\Máy tính\ImageSlide\FormCv";
            var formCvList = new List<FormCv>();
            if (Directory.Exists(directoryPath))
            {
                var files = Directory.GetFiles(directoryPath);
                foreach (var file in files)
                {
                    var formCv = new FormCv
                    {
                        CvFilePath = file,
                    };

                    formCvList.Add(formCv);
                }
            }

            return formCvList;
        }
    }
}
