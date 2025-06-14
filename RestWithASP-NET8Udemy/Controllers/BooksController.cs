using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithASP_NET8Udemy.Model;
using RestWithASP_NET8Udemy.Business;

namespace RestWithASP_NET8Udemy.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BooksController : ControllerBase
    {

        private readonly ILogger<BooksController> _logger;
        private IBooksBusiness _booksBusiness;

        public BooksController(ILogger<BooksController> logger, IBooksBusiness booksBusiness)
        {
            _logger = logger;
            _booksBusiness = booksBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Books> books = _booksBusiness.FindAll();
            return Ok(books);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var books = _booksBusiness.FindById(id);
            if (books == null) return NotFound();
            return Ok(books);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Books books)
        {
            if (books == null) return BadRequest();
            return Ok(_booksBusiness.Create(books));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Books books)
        {
            if (books == null) return BadRequest();
            return Ok(_booksBusiness.Update(books));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _booksBusiness.Delete(id);
            return NoContent();
        }
    }
}
