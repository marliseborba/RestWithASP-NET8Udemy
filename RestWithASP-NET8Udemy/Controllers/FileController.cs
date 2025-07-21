using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASP_NET8Udemy.Business;
using RestWithASP_NET8Udemy.Data.VO;

namespace RestWithASP_NET8Udemy.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : Controller
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpPost("uploadFile")]
        [ProducesResponseType((200), Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            FileDetailVO detail = await _fileBusiness.SaveFileToDisk(file);
            return new OkObjectResult(detail);
        }
    }
}
