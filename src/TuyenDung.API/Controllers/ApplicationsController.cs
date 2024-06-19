using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuyenDung.Service.Repository.Interface;

namespace TuyenDung.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationsInterface _applications;
        public ApplicationsController(IApplicationsInterface applications)
        {
            _applications = applications;
        }
        [HttpGet("CvFilePath/formCV/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllFormCV(int id)
        {
            try
            {
                var formCV = _applications.GetFormCvId(id);
                if (formCV == null || string.IsNullOrEmpty(formCV.CvFilePath))
                {
                    return NotFound("CvFilePath not found!");
                }
                var cvFilePath = _applications.ExtractTextFromDocx(formCV.CvFilePath);
                return Ok(cvFilePath);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred: {e.Message}");
            }
        }
    }
}
