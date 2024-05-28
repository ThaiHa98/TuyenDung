using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TuyenDung.API.Common;
using TuyenDung.Data.Dto;
using TuyenDung.Service.Services.InterfaceIServices;

namespace TuyenDung.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobsIService _jobsIService;
        public JobsController(IJobsIService jobsIService)
        {
            _jobsIService = jobsIService;
        }
        [HttpPost("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Create(JobsDto jobsDto)
        {
            try
            {
                if(jobsDto == null)
                {
                    return BadRequest("All data fields have not been filled in");
                }
                var create = _jobsIService.Create(jobsDto);
                return Ok(new XBaseResult
                {
                    data = create,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = create.Id,
                    message = "Create Jobs Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new XBaseResult()
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        [HttpPut("Update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Update(JobsUpdateDto jobsUpdateDto) 
        {
            try
            {
                if (jobsUpdateDto == null)
                {
                    return BadRequest("All data fields have not been filled in");
                }
                var updateJobs = _jobsIService.Update(jobsUpdateDto);
                return Ok(new XBaseResult
                {
                    data = updateJobs,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = updateJobs.Id,
                    message = "Update Jobs Successfully"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        [HttpDelete("Delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            try
            {
                var deleteJobs = _jobsIService.Delete(id);
                return Ok(new XBaseResult
                {
                    data = deleteJobs,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Delete Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
    }
}
