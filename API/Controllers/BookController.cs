using DataLibrary.BusinessLogic;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    public class BookController : ControllerBase
    {
        //Public Access to any book categorized as "public"

        [HttpGet("api/books")]
        public IEnumerable<ItemModel> GetAllBooks()
        {
            return BookProcessor.GetAllBooks();
        }

        [HttpGet("api/books/{type}/{value}")]
        public IEnumerable<ItemModel> GetBookByType(string type, string value)
        {
            return BookProcessor.GetBookByType(type, value);
        }

        [HttpGet]
        [Route("api/books/{id}")]
        public List<ItemModel> GetBookById(int id)
        {
            return BookProcessor.GetBookById(id);
        }

        //Requires Authentication to access items categorized as "Rare"

        [HttpGet]
        [Route("api/books/member/{token}")]
        public IEnumerable<ItemModel> GetAllBooks(string token)
        {
            return BookProcessor.GetAllBooks(token);
        }

        [HttpGet]
        [Route("api/books/member/{token}/{type}/{value}")]
        public IEnumerable<ItemModel> GetBookByType(string token, string type, string value)
        {
            return BookProcessor.GetBookByType(token, type, value);
        }

        [HttpGet]
        [Route("api/books/member/{token}/{id}")]
        public List<ItemModel> GetBookById(string token, int id)
        {
            return BookProcessor.GetBookById(token, id);
        }

        //Requires authentication to manipulate books

        [HttpPost]
        [Route("api/books/staff")]
        public AuthResponseModel AddBook([FromBody] ItemModel itemModel)
        {
            return null;
        }

        [HttpPut]
        [Route("api/books/staff")]
        public AuthResponseModel Put([FromBody] int token, int id, string type, string value)
        {
            return null;
        }

        [HttpDelete]
        [Route("api/books/staff/{token}/{id}")]
        public AuthResponseModel DeleteBook(string token, int id)
        {
            return null;
        }
    }
}
