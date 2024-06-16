using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.DataContext;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;
using TuyenDung.Data.Model.Enum;
using TuyenDung.Service.Repository.Interface;
using TuyenDung.Service.Services.InterfaceIServices;
using static System.Net.Mime.MediaTypeNames;

namespace TuyenDung.Service.Services.RepositoryServices
{
    public class ApplicationsService : IApplicationsIService
    {
        private readonly MyDb _dbContext;
        private readonly IFormCvInterface _formCvInterface;
        public ApplicationsService(MyDb dbContext,IFormCvInterface formCvInterface)
        {
            _formCvInterface = formCvInterface;
            _dbContext = dbContext;
        }
        public Applications Create(ApplicationsDto applicationsDto, IFormFile formFile)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var app = _dbContext.Jobs.FirstOrDefault(x => x.Id == applicationsDto._JobId);
                    if (app == null)
                    {
                        throw new Exception("App not found");
                    }

                    var user = _dbContext.Users.FirstOrDefault(x => x.Id == applicationsDto._UserId);
                    if (user == null)
                    {
                        throw new Exception("User not found");
                    }

                    Applications applications = new Applications
                    {
                        Job_JobId = app.Id,
                        User_UserId = user.Id,
                        Status = StatusApplication.Pending,
                        Date = DateTime.Now,
                        StatusSubmissionType = StatusSubmissionType.Online,
                    };

                    if (formFile != null && formFile.Length > 0)
                    {
                        try
                        {
                            string documentPath = SaveDocumentFile(formFile);
                            applications.CvFilePath = documentPath;

                            _dbContext.Applications.Add(applications);
                            _dbContext.SaveChanges();

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("An error occurred while processing the application.", ex);
                        }
                    }

                    return applications;
                }
                catch (Exception ex)
                {
                    throw new Exception("There is an error when creating an application", ex);
                }
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                var app = _dbContext.Applications.FirstOrDefault(x => x.Id == Id);
                if(app == null)
                {
                    throw new Exception("app not found");
                }
                _dbContext.Remove(Delete);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("There was an error deleteting the Applications",ex);
            }
        }

        public List<FormCv> GetListFormCv()
        {
            try
            {
                var formCvList = _formCvInterface.GetFormCv();
                var wordCvList = new List<FormCv>();

                foreach (var formCv in formCvList)
                {
                    if (IsWordFile(formCv.CvFilePath))
                    {
                        wordCvList.Add(formCv);
                    }
                }

                return wordCvList;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the list of Word files.", ex);
            }
        }

        public string Update(int Id, IFormFile formFile)
        {
            try
            {
                var app = _dbContext.Applications.FirstOrDefault(x => x.Id == Id);
                if (app == null)
                {
                    throw new Exception("Application not found");
                }
                if (formFile != null && formFile.Length > 0)
                {
                    try
                    {
                        string documentPath = SaveDocumentFile(formFile);
                        app.CvFilePath = documentPath;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while processing the document file.", ex);
                    }
                }
                _dbContext.SaveChanges();
                return "Update successful";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the application.", ex);
            }
        }

        private string SaveDocumentFile(IFormFile document)
        {
            try
            {
                string currentDateFolder = DateTime.Now.ToString("dd-MM-yyyy");
                string documentsFolder = Path.Combine(@"C:\Users\Xuanthai98\OneDrive\Máy tính\Documents", "YourDocumentsFolder", currentDateFolder);
                if (!Directory.Exists(documentsFolder))
                {
                    Directory.CreateDirectory(documentsFolder);
                }
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(document.FileName);
                string filePath = Path.Combine(documentsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    document.CopyTo(stream);
                }
                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving the document: {ex.Message}");
            }
        }
        private bool IsWordFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return extension.ToLower() == ".doc" || extension.ToLower() == ".docx";
        }
    }
}
