using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.DataContext;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;
using TuyenDung.Service.Services.InterfaceIServices;
using static System.Net.Mime.MediaTypeNames;

namespace TuyenDung.Service.Services.RepositoryServices
{
    public class FormCvService : IFormCvIService
    {
        private readonly MyDb _dbContext;
        public FormCvService(MyDb dbContext)
        {
            _dbContext = dbContext;
        }
        public FormCv Create(FormCvDto formCvDto, IFormFile CvFilePath)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Id == formCvDto.UserId);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var formCv = new FormCv
                {
                    UserId = user.Id,
                    Title = formCvDto.Title_,
                    Description = formCvDto.Description_,
                    DateCreated = DateTime.Now
                };

                if (CvFilePath != null && CvFilePath.Length > 0)
                {
                    string filePath = SaveFile(CvFilePath);
                    formCv.CvFilePath = filePath;
                }

                _dbContext.FormCvs.Add(formCv);
                _dbContext.SaveChanges();

                return formCv;
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error when creating a FormCv", ex);
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                var formCv = _dbContext.FormCvs.FirstOrDefault(x => x.Id == Id);
                if(formCv == null)
                {
                    throw new Exception("formCv not found");
                }
                _dbContext.Remove(formCv);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("There was an error deleteting the formCv", ex);
            }
        }

        public string Update(FormCvDto formCvDto, IFormFile CvFilePath)
        {
            try
            {
                var formCv = _dbContext.FormCvs.FirstOrDefault(x => x.Id == formCvDto.Id);
                if(formCv == null)
                {
                    throw new Exception("formCv not found");
                }
                formCv.Title = formCvDto.Title_;
                formCv.Description = formCvDto.Description_;
                if (CvFilePath != null && CvFilePath.Length > 0)
                {
                    string filePath = SaveFile(CvFilePath);
                    formCv.CvFilePath = filePath;
                }
                _dbContext.Update(formCv);
                _dbContext.SaveChanges();
                return "Update formCv Successfully";
            }
            catch(Exception ex) 
            {
                throw new Exception("There was an error updating the formCv",ex);
            }
        }
        private string SaveFile(IFormFile file)
        {
            try
            {
                // Định nghĩa các phần mở rộng tệp hợp lệ
                var allowedExtensions = new[] { ".docx", ".pptx", ".pdf", ".xlsx", ".txt" };

                // Lấy phần mở rộng của tệp và chuyển thành chữ thường để so sánh không phân biệt chữ hoa chữ thường
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    throw new Exception("Định dạng tệp không hợp lệ.");
                }
                string currentDateFolder = DateTime.Now.ToString("dd-MM-yyyy");
                string filesFolder = Path.Combine(@"C:\Users\Xuanthai98\OneDrive\Máy tính\UploadedFiles\FormCv", currentDateFolder);
                if (!Directory.Exists(filesFolder))
                {
                    Directory.CreateDirectory(filesFolder);
                }
                string fileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(filesFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Đã xảy ra lỗi khi lưu tệp: {ex.Message}");
            }
        }
    }
}
