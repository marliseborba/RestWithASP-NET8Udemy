using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithASP_NET8Udemy.Model;
using RestWithASP_NET8Udemy.Business;

namespace RestWithASP_NET8Udemy.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {

        private readonly ILogger<BookController> _logger;
        private IBookBusiness _bookBusiness;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Book> books = _bookBusiness.FindAll();
            return Ok(books);
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var books = _bookBusiness.FindById(id);
            if (books == null) return NotFound();
            return Ok(books);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book books)
        {
            if (books == null) return BadRequest();
            return Ok(_bookBusiness.Create(books));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Book books)
        {
            if (books == null) return BadRequest();
            return Ok(_bookBusiness.Update(books));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}
