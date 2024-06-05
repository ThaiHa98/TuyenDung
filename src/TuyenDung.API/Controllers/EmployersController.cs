using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TuyenDung.API.Common;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;
using TuyenDung.Service.Services.InterfaceIServices;

namespace TuyenDung.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly IEmployersIService _employersIService;
        public EmployersController(IEmployersIService employersService)
        {
            _employersIService = employersService;
        }
        [HttpPost("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployers([FromForm] EmployersDto employersDto, [FromForm] IFormFile image)
        {
            try
            {
                if(employersDto == null)
                {
                    return BadRequest("All data fields have not been filled in");
                }
                var employers = _employersIService.Create(employersDto, image);
                return Ok(new XBaseResult
                {
                    data = employers,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = employers.Id,
                    message = "Create employers Successfully"
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
        [HttpPut("Update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Update([FromForm]EmployersDto employersDto, [FromForm] IFormFile image)
        {
            try
            {
                if(employersDto == null || image == null)
                {
                    return BadRequest("All data fields have not been filled in");
                }
                var employers = _employersIService.Update(employersDto, image);
                return Ok(new XBaseResult
                {
                    data = employers,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = employers.Id,
                    message = "Update Employers Successfully"
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
        [HttpDelete("Delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Delete(int id)
        {
            try
            {
                if(id == null)
                {
                    return BadRequest("id not found");
                }
                var employers = _employersIService.Delete(id);
                return Ok(new XBaseResult
                {
                    data = employers,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = "Delete Employers Successfully"
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
    }
}
