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
    public class MagazineController : ControllerBase
    {
        //Requires Authentication to access items categorized as "Rare"

        [HttpGet("api/magazine/member/{token}")]
        public IEnumerable<ItemModel> GetAllMagazines(string token)
        {
            return MagazineProcessor.GetAllMagazines(token);
        }

        [HttpGet("api/magazine/member/{token}/{type}/{value}")]
        public IEnumerable<ItemModel> GetMagazinesByType(string token, string type, string value)
        {
            return MagazineProcessor.GetMagazinesByType(token, type, value);
        }

        [HttpGet("api/magazine/member/{token}/{id}")]
        public List<ItemModel> GetMagazineById(string token, int id)
        {
            return MagazineProcessor.GetMagazineById(token, id);
        }

        //Requires authentication to manipulate Journals

        [HttpPost("api/magazine/staff")]
        public AuthResponseModel SetMagazine([FromBody] ItemModel itemModel)
        {
            InsertLibraryItemLogic setMagazine = new InsertLibraryItemLogic();
            return setMagazine.SetLibraryItem(itemModel, "magazine");
        }

        [HttpPut("api/magazine/staff")]
        public AuthResponseModel UpdateMagazine([FromBody] DynamicUpdateModel updateModel)
        {
            UpdateLibraryItemLogic updateMagazine = new UpdateLibraryItemLogic();
            return updateMagazine.UpdateLibraryItem(updateModel, "magazine");
        }

        [HttpDelete("api/magazine/staff/{token}/{id}")]
        public AuthResponseModel DeleteMagazine(string token, int id)
        {
            DeleteLibraryItemLogic deleteMagazine = new DeleteLibraryItemLogic();
            return deleteMagazine.DeleteLibraryItem(token, id, "magazine");
        }
    }
}
