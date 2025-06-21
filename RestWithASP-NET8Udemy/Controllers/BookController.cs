using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithASP_NET8Udemy.Business;
using RestWithASP_NET8Udemy.Data.VO;
using RestWithASP_NET8Udemy.Hypermedia.Filters;

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
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            List<BookVO> books = _bookBusiness.FindAll();
            return Ok(books);
        }
        
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var books = _bookBusiness.FindById(id);
            if (books == null) return NotFound();
            return Ok(books);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookVO books)
        {
            if (books == null) return BadRequest();
            return Ok(_bookBusiness.Create(books));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVO books)
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
