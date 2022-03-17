using Microsoft.AspNetCore.Mvc;
using Portail_e_book.Models.BLL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Controllers
{
    [Route("api/Catalogue")]
    [ApiController]
    public class CatalogueController : Controller
    {
        #region API Calls

        [HttpGet]
        [Route("getAllCatalogue")]
        public IActionResult getAll()
        {
            return Json(new { Data = BLL_Catalogue.getAllCatalogue() });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getCatalogueBy")]
        public IActionResult getBy(string Field, string Value)
        {
            return Json(new { Data = BLL_Catalogue.getCatalogueBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getAllCatalogueBy")]
        public IActionResult getAllBy(string Field, string Value)
        {
            return Json(new { Data = BLL_Catalogue.getAllCatalogueBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpPost]
        [Route("UpsertCatalogue")]
        public IActionResult Upsert(Catalogue catalogue)
        {
            return Json(BLL_Catalogue.UpsertApi(catalogue));
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpDelete]
        [Route("DeleteCatalogue")]
        public IActionResult Delete(int id)
        {
            return Json(BLL_Catalogue.DeleteApi(id));
        }
        #endregion

    }
}
