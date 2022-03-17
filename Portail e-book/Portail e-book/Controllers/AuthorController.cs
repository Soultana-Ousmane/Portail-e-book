using Microsoft.AspNetCore.Mvc;
using Portail_e_book.Models.BLL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Controllers
{
    [Route("api/Author")]
    [ApiController]
    public class AuthorController : Controller
    {
        #region API Calls
        [HttpGet]
        [Route("getAllAuthor")]
        public IActionResult getAll()
        {
            return Json(new { Data = BLL_Author.getAllAuthor() });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getAuthorBy")]
        public IActionResult getBy(string Field, string Value)
        {
            return Json(new { Data = BLL_Author.getAuthorBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("getAllAuthorBy")]
        public IActionResult getAllBy(string Field, string Value)
        {
            return Json(new { Data = BLL_Author.getAllAuthorBy(Field, Value) });
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpPost]
        [Route("UpsertAuthor")]
        public IActionResult Upsert(Author author)
        {
            return Json(BLL_Author.UpsertApi(author));
        }
        /*----------------------------------------------------------------------------------------------------------------------------*/
        [HttpDelete]
        [Route("DeleteAuthor")]
        public IActionResult Delete(int id)
        {
            return Json(BLL_Author.DeleteApi(id));
        }
        #endregion
    }
}
