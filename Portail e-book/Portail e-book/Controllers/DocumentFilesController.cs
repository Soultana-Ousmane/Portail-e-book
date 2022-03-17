using Microsoft.AspNetCore.Mvc;
using Portail_e_book.Models.BLL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Controllers
{
    [Route("api/DocumentFiles")]
    [ApiController]
    public class DocumentFilesController : Controller
    {
        #region API Calls

        [HttpGet]
        [Route("getAllDocumentFiles")]
        public IActionResult getAll()
        {
            return Json(new { Data = BLL_DocumentFiles.getAllDocumentFiles() });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getDocumentFilesBy")]
        public IActionResult getBy(string Field, string Value)
        {
            return Json(new { Data = BLL_DocumentFiles.getDocumentFilesBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getAllDocumentFilesBy")]
        public IActionResult getAllBy(string Field, string Value)
        {
            return Json(new { Data = BLL_DocumentFiles.getAllDocumentFilesBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpPost]
        [Route("UpsertDocumentFiles")]
        public IActionResult Upsert(DocumentFiles documentFiles)
        {
            return Json(BLL_DocumentFiles.UpsertApi(documentFiles));
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpDelete]
        [Route("DeleteDocumentFiles")]
        public IActionResult Delete(int id)
        {
            return Json(BLL_DocumentFiles.DeleteApi(id));
        }
        #endregion

    }
}
