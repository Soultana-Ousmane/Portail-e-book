using Portail_e_book.Models.DAL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.BLL
{
    public class BLL_Ebook
    {
        public static IEnumerable<Ebook> getAllEbook()
        {
            return DAL_Ebook.getAllEbook();
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Get the details of a particular Ebook
        public static Ebook getEbookBy(string Champ, string Valeur)
        {
            return DAL_Ebook.getEbookBy(Champ, Valeur);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        public static IEnumerable<Ebook> getAllEbookBy(string Field, string Value)
        {
            return DAL_Ebook.getAllEbookBy(Field, Value);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Add new Ebook record
        public static JsonResponse AddEbook(Ebook ebook)
        {
            return DAL_Ebook.AddEbook(ebook);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Update the records of a particluar Ebook
        public static JsonResponse UpdateEbook(Ebook ebook)
        {
            return DAL_Ebook.UpdateEbook(ebook);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Delete the records of a particular Ebook
        public static JsonResponse DeleteEbook(int? idEbook)
        {
            return DAL_Ebook.DeleteEbookBy("IdEbook", idEbook.ToString());
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Create or Update Ebook
        public static Ebook Upsert(int? idEbook)
        {
            if (idEbook == null)
            {
                //create
                return new Ebook();
            }
            else
            {
                //Update
                return getEbookBy("IdEbook", idEbook.ToString());
            }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        #region API Calls
        public static JsonResponse UpsertApi(Ebook ebook)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            if (ebook.IdEbook == 0)
            {
                //create
                jr = AddEbook(ebook);
            }
            else if (ebook.IdEbook != 0)
            {
                //update
                jr = UpdateEbook(ebook);
            }
            return jr;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        public static JsonResponse DeleteApi(int idEbook)
        {
            JsonResponse jr = new JsonResponse();
            var EbookFromDb = getEbookBy("IdEbook", idEbook.ToString());
            if (EbookFromDb == null)
            {
                jr.success = false;
                jr.message = "Error while Deleting";
                return jr;
            }
            else
            {
                jr = DeleteEbook(idEbook);
                if (jr.message == "1")
                {
                    jr.success = true;
                    jr.message = "Delete successful";
                }
                else
                {
                    jr.success = false;
                    jr.message = jr.message;
                }
            }
            return jr;
        }
        #endregion

    }
}
