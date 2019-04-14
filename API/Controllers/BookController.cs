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

        [HttpGet("api/books/{id}")]
        public List<ItemModel> GetBookById(int id)
        {
            return BookProcessor.GetBookById(id);
        }

        //Requires Authentication to access items categorized as "Rare"

        [HttpGet("api/books/member/{token}")]
        public IEnumerable<ItemModel> GetAllBooks(string token)
        {
            return BookProcessor.GetAllBooks(token);
        }

        [HttpGet("api/books/member/{token}/{type}/{value}")]
        public IEnumerable<ItemModel> GetBookByType(string token, string type, string value)
        {
            return BookProcessor.GetBookByType(token, type, value);
        }

        [HttpGet("api/books/member/{token}/{id}")]
        public List<ItemModel> GetBookById(string token, int id)
        {
            return BookProcessor.GetBookById(token, id);
        }

        //Requires authentication to manipulate books

        [HttpPost("api/books/staff")]
        public AuthResponseModel SetBook([FromBody] ItemModel itemModel)
        {
            InsertLibraryItemLogic setBook = new InsertLibraryItemLogic();
            return setBook.SetLibraryItem(itemModel, "book");
        }

        [HttpPut("api/books/staff")]
        public AuthResponseModel UpdateBook([FromBody] DynamicUpdateModel updateModel)
        {
            UpdateLibraryItemLogic updateBook = new UpdateLibraryItemLogic();
            return updateBook.UpdateLibraryItem(updateModel, "book");
        }

        [HttpDelete("api/books/staff/{token}/{id}")]
        public AuthResponseModel DeleteBook(string token, int id)
        {
            DeleteLibraryItemLogic deleteBook = new DeleteLibraryItemLogic();
            return deleteBook.DeleteLibraryItem(token, id, "book");
        }
    }
}
