using Microsoft.AspNetCore.Mvc;
using Portail_e_book.Models.BLL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Controllers
{
    [Route("api/DocumentAuthor")]
    [ApiController]
    public class DocumentAuthorController : Controller
    {
        #region API Calls

        [HttpGet]
        [Route("getAllDocumentAuthor")]
        public IActionResult getAll()
        {
            return Json(new { Data = BLL_DocumentAuthor.getAllDocumentAuthor() });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getDocumentAuthorBy")]
        public IActionResult getBy(string Field, string Value)
        {
            return Json(new { Data = BLL_DocumentAuthor.getDocumentAuthorBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getAllDocumentAuthorBy")]
        public IActionResult getAllBy(string Field, string Value)
        {
            return Json(new { Data = BLL_DocumentAuthor.getAllDocumentAuthorBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpPost]
        [Route("UpsertDocumentAuthor")]
        public IActionResult Upsert(DocumentAuthor documentAuthor)
        {
            return Json(BLL_DocumentAuthor.UpsertApi(documentAuthor));
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpDelete]
        [Route("DeleteDocumentAuthor")]
        public IActionResult Delete(int id)
        {
            return Json(BLL_DocumentAuthor.DeleteApi(id));
        }
        #endregion

    }
}
