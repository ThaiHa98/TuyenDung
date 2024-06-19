using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.Model;

namespace TuyenDung.Service.Repository.Interface
{
    public interface IApplicationsInterface
    {
        public Applications GetFormCvId(int id);
        public string ExtractTextFromDocx(string cvFilePath);
    }
}
