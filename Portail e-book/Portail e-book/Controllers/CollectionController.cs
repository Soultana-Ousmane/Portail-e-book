using Microsoft.AspNetCore.Mvc;
using Portail_e_book.Models.BLL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Controllers
{
    [Route("api/Collection")]
    [ApiController]
    public class CollectionController : Controller
    {
        #region API Calls

        [HttpGet]
        [Route("getAllCollection")]
        public IActionResult getAll()
        {
            return Json(new { Data = BLL_Collection.getAllCollection() });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getCollectionBy")]
        public IActionResult getBy(string Field, string Value)
        {
            return Json(new { Data = BLL_Collection.getCollectionBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getAllCollectionBy")]
        public IActionResult getAllBy(string Field, string Value)
        {
            return Json(new { Data = BLL_Collection.getAllCollectiontBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpPost]
        [Route("UpsertCollection")]
        public IActionResult Upsert(Collection collection)
        {
            return Json(BLL_Collection.UpsertApi(collection));
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpDelete]
        [Route("DeleteCollection")]
        public IActionResult Delete(int id)
        {
            return Json(BLL_Collection.DeleteApi(id));
        }
        #endregion

    }
}
