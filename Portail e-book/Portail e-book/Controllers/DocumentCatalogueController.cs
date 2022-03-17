using Microsoft.AspNetCore.Mvc;
using Portail_e_book.Models.BLL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Controllers
{
    [Route("api/DocumentCatalogue")]
    [ApiController]
    public class DocumentCatalogueController : Controller
    {
        #region API Calls

        [HttpGet]
        [Route("getAllDocumentCatalogue")]
        public IActionResult getAll()
        {
            return Json(new { Data = BLL_DocumentCatalogue.getAllDocumentCatalogue() });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getDocumentCatalogueBy")]
        public IActionResult getBy(string Field, string Value)
        {
            return Json(new { Data = BLL_DocumentCatalogue.getDocumentCatalogueBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getAllDocumentCatalogueBy")]
        public IActionResult getAllBy(string Field, string Value)
        {
            return Json(new { Data = BLL_DocumentCatalogue.getAllDocumentCatalogueBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpPost]
        [Route("UpsertDocumentCatalogue")]
        public IActionResult Upsert(DocumentCatalogue documentCatalogue)
        {
            return Json(BLL_DocumentCatalogue.UpsertApi(documentCatalogue));
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpDelete]
        [Route("DeleteDocumentCatalogue")]
        public IActionResult Delete(int id)
        {
            return Json(BLL_DocumentCatalogue.DeleteApi(id));
        }
        #endregion
    }
}
