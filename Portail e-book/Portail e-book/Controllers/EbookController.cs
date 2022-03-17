using Microsoft.AspNetCore.Mvc;
using Portail_e_book.Models.BLL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Controllers
{
    [Route("api/Ebook")]
    [ApiController]
    public class EbookController : Controller
    {
        #region API Calls
        [HttpGet]
        [Route("getAllEbook")]
        public IActionResult getAll()
        {
            return Json(new { Data = BLL_Ebook.getAllEbook() });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getEbookBy")]
        public IActionResult getBy(string Field, string Value)
        {
            return Json(new { Data = BLL_Ebook.getEbookBy(Field, Value) });
        }
        ///*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getAllEbookBy")]
        public IActionResult getAllBy(string Field, string Value)
        {
            return Json(new { Data = BLL_Ebook.getAllEbookBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpPost]
        [Route("UpsertEbook")]
        public IActionResult Upsert(Ebook ebook)
        {
            return Json(BLL_Ebook.UpsertApi(ebook));
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpDelete]
        [Route("DeleteEbook")]
        public IActionResult Delete(int id)
        {
            return Json(BLL_Ebook.DeleteApi(id));
        }
        #endregion
    }
}
