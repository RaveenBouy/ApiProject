using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.BusinessLogic;
using DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class NewspaperController : ControllerBase
    {
        //Requires Authentication to access items categorized as "Rare"

        [HttpGet("api/newspaper/member/{token}")]
        public IEnumerable<ItemModel> GetAllNewspapers(string token)
        {
            return NewspaperProcessor.GetAllNewspapers(token);
        }

        [HttpGet("api/newspaper/member/{token}/{type}/{value}")]
        public IEnumerable<ItemModel> GetNewspapersByType(string token, string type, string value)
        {
            return NewspaperProcessor.GetNewspapersByType(token, type, value);
        }

        [HttpGet("api/newspaper/member/{token}/{id}")]
        public List<ItemModel> GetNewspaperById(string token, int id)
        {
            return NewspaperProcessor.GetNewspaperById(token, id);
        }

        //Requires authentication to manipulate Journals

        [HttpPost("api/newspaper/staff")]
        public AuthResponseModel SetNewspaper([FromBody] ItemModel itemModel)
        {
            InsertLibraryItemLogic setNewspaper = new InsertLibraryItemLogic();
            return setNewspaper.SetLibraryItem(itemModel, "newspaper");
        }

        [HttpPut("api/newspaper/staff")]
        public AuthResponseModel UpdateNewspaper([FromBody] DynamicUpdateModel updateModel)
        {
            UpdateLibraryItemLogic updateNewspaper = new UpdateLibraryItemLogic();
            return updateNewspaper.UpdateLibraryItem(updateModel, "newspaper");
        }

        [HttpDelete("api/newspaper/staff/{token}/{id}")]
        public AuthResponseModel DeleteNewspaper(string token, int id)
        {
            DeleteLibraryItemLogic deleteNewspaper = new DeleteLibraryItemLogic();
            return deleteNewspaper.DeleteLibraryItem(token, id, "newspaper");
        }
    }
}
