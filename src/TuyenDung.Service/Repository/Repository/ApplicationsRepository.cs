using System.Diagnostics;
using TuyenDung.Data.DataContext;
using TuyenDung.Data.Model;
using TuyenDung.Service.Repository.Interface;
using System;
using System.Diagnostics;
using System.IO;
using Spire.Doc;
using Spire.Doc.Documents;

namespace TuyenDung.Service.Repository.Repository
{
    public class ApplicationsRepository : IApplicationsInterface
    {
        private readonly MyDb _dbContext;
        public ApplicationsRepository(MyDb dbContext)
        {
            _dbContext = dbContext;
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

        public Applications GetFormCvId(int id)
        {
            return _dbContext.Applications.FirstOrDefault(x => x.Id == id);
        }
    }
}
