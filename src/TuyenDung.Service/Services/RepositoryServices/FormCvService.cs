using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using TuyenDung.Data.DataContext;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;
using TuyenDung.Service.Services.InterfaceIServices;
using System;
using System.Diagnostics;
using System.IO;
using Spire.Doc;
using Spire.Doc.Documents;

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
                if (formCvDto == null)
                {
                    throw new Exception("User not found");
                }
                var formCv = new FormCv
                {
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
                if (formCv == null)
                {
                    throw new Exception("formCv not found");
                }
                _dbContext.Remove(formCv);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error deleteting the formCv", ex);
            }
        }

        public string Update(FormCvDto formCvDto, IFormFile CvFilePath)
        {
            try
            {
                var formCv = _dbContext.FormCvs.FirstOrDefault(x => x.Id == formCvDto.Id);
                if (formCv == null)
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
            catch (Exception ex)
            {
                throw new Exception("There was an error updating the formCv", ex);
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
                string filesFolder = Path.Combine(@"C:\Users\Xuanthai98\OneDrive\Máy tính\ImageSlide\MauCV");
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

        public string ExtractTextFromDocx(string cvFilePath)
        {
            try
            {
                if (string.IsNullOrEmpty(cvFilePath) || !File.Exists(cvFilePath))
                {
                    throw new FileNotFoundException("Không tìm thấy tệp!", cvFilePath);
                }

                // Tải tệp DOCX bằng Spire.Doc
                Document document = new Document();
                document.LoadFromFile(cvFilePath);

                // Điều chỉnh tài liệu để phù hợp vào một trang PDF
                foreach (Section section in document.Sections)
                {
                    section.PageSetup.PageSize = PageSize.A4;
                    section.PageSetup.Orientation = PageOrientation.Portrait;
                    section.PageSetup.Margins.Top = 20f;
                    section.PageSetup.Margins.Bottom = 20f;
                    section.PageSetup.Margins.Left = 20f;
                    section.PageSetup.Margins.Right = 20f;

                    foreach (Paragraph paragraph in section.Paragraphs)
                    {
                        paragraph.Format.LineSpacing = 12f; // Điều chỉnh khoảng cách dòng
                        paragraph.Format.AfterSpacing = 0;
                        paragraph.Format.BeforeSpacing = 0;
                    }
                }

                // Lưu tài liệu thành tệp PDF
                string pdfFilePath = "result.pdf";
                document.SaveToFile(pdfFilePath, FileFormat.PDF);

                // Tùy chọn mở tệp PDF
                Process.Start(new ProcessStartInfo(pdfFilePath) { UseShellExecute = true });

                return "PDF đã được tạo thành công!";
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Không tìm thấy tệp: {ex.Message}");
                throw; // Ném lại FileNotFoundException để xử lý ở cấp cao hơn nếu cần
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi khi trích xuất văn bản: {ex.Message}");
                throw; // Ném lại ngoại lệ để được xử lý bởi mã gọi nếu cần thiết
            }
        }
    }
}

