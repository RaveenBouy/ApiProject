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
    public class ManuscriptController : ControllerBase
    {
        //Requires Authentication to access items categorized as "Rare"

        [HttpGet("api/manuscript/member/{token}")]
        public IEnumerable<ItemModel> GetAllManuscripts(string token)
        {
            return ManuscriptProcessor.GetAllManuscripts(token);
        }

        [HttpGet("api/manuscript/member/{token}/{type}/{value}")]
        public IEnumerable<ItemModel> GetManuscriptsByType(string token, string type, string value)
        {
            return ManuscriptProcessor.GetManuscriptsByType(token, type, value);
        }

        [HttpGet("api/manuscript/member/{token}/{id}")]
        public List<ItemModel> GetManuscriptById(string token, int id)
        {
            return ManuscriptProcessor.GetManuscriptById(token, id);
        }

        //Requires authentication to manipulate Journals

        [HttpPost("api/manuscript/staff")]
        public AuthResponseModel SetManuscript([FromBody] ItemModel itemModel)
        {
            InsertLibraryItemLogic setManuscript = new InsertLibraryItemLogic();
            return setManuscript.SetLibraryItem(itemModel, "manuscript");
        }

        [HttpPut("api/manuscript/staff")]
        public AuthResponseModel UpdateManuscript([FromBody] DynamicUpdateModel updateModel)
        {
            UpdateLibraryItemLogic updateManuscript = new UpdateLibraryItemLogic();
            return updateManuscript.UpdateLibraryItem(updateModel, "manuscript");
        }

        [HttpDelete("api/manuscript/staff/{token}/{id}")]
        public AuthResponseModel DeleteManuscript(string token, int id)
        {
            DeleteLibraryItemLogic deleteManuscript = new DeleteLibraryItemLogic();
            return deleteManuscript.DeleteLibraryItem(token, id, "manuscript");
        }
    }
}
