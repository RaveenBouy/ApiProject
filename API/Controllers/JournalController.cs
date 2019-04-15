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
    public class JournalController : ControllerBase
    {
        //Requires Authentication to access items categorized as "Rare"

        [HttpGet("api/journal/member/{token}")]
        public IEnumerable<ItemModel> GetAllJournals(string token)
        {
            return JournalProcessor.GetAllJournals(token);
        }

        [HttpGet("api/journal/member/{token}/{type}/{value}")]
        public IEnumerable<ItemModel> GetJournalsByType(string token, string type, string value)
        {
            return JournalProcessor.GetJournalsByType(token, type, value);
        }

        [HttpGet("api/journal/member/{token}/{id}")]
        public List<ItemModel> GetJournalById(string token, int id)
        {
            return JournalProcessor.GetJournalById(token, id);
        }

        //Requires authentication to manipulate Journals

        [HttpPost("api/journal/staff")]
        public AuthResponseModel SetJournal([FromBody] ItemModel itemModel)
        {
            InsertLibraryItemLogic setJournal = new InsertLibraryItemLogic();
            return setJournal.SetLibraryItem(itemModel, "journal");
        }

        [HttpPut("api/journal/staff")]
        public AuthResponseModel UpdateJournal([FromBody] DynamicUpdateModel updateModel)
        {
            UpdateLibraryItemLogic updateJournal = new UpdateLibraryItemLogic();
            return updateJournal.UpdateLibraryItem(updateModel, "journal");
        }

        [HttpDelete("api/journal/staff/{token}/{id}")]
        public AuthResponseModel DeleteJournal(string token, int id)
        {
            DeleteLibraryItemLogic deleteJournal = new DeleteLibraryItemLogic();
            return deleteJournal.DeleteLibraryItem(token, id, "journal");
        }
    }
}
