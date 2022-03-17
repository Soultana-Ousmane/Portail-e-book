using Portail_e_book.Models.DAL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.BLL
{
    public class BLL_Catalogue
    {
        public static IEnumerable<Catalogue> getAllCatalogue()
        {
            return DAL_Catalogue.getAllCatalogue();
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/

        public static Catalogue getCatalogueBy(string Champ, string Valeur)
        {
            return DAL_Catalogue.getCatalogueBy(Champ, Valeur);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        public static IEnumerable<Catalogue> getAllCatalogueBy(string Field, string Value)
        {
            return DAL_Catalogue.getAllCatalogueBy(Field, Value);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
       
        public static JsonResponse AddCatalogue(Catalogue catalogue)
        {
            return DAL_Catalogue.AddCatalogue(catalogue);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        
        public static JsonResponse UpdateCatalogue(Catalogue catalogue)
        {
            return DAL_Catalogue.UpdateCatalogue(catalogue);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
       
        public static JsonResponse DeleteCatalogue(int? idCatalogue)
        {
            /*----------------------------------*/
            List<DocumentCatalogue> listDocumentCatalogue = DAL_DocumentCatalogue.getAllDocumentCatalogueBy("IdCatalogue", idCatalogue.ToString());
            for (int i = 0; i < listDocumentCatalogue.Count; i++)
            {
                BLL_DocumentCatalogue.DeleteDocumentCatalogue(listDocumentCatalogue[i].IdDocumentCatalogue);
            }
            /*------------------------------*/
            return DAL_Catalogue.DeleteCatalogueBy("IdCatalogue", idCatalogue.ToString());
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        
        public static Catalogue Upsert(int? idCatalogue)
        {
            if (idCatalogue == null)
            {
                //create
                return new Catalogue();
            }
            else
            {
                //Update
                return getCatalogueBy("IdCatalogue", idCatalogue.ToString());
            }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        #region API Calls
        public static JsonResponse UpsertApi(Catalogue catalogue)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            if (catalogue.IdCatalogue == 0)
            {
                //create
                jr = AddCatalogue(catalogue);
            }
            else
            {
                //update
                jr = UpdateCatalogue(catalogue);
            }
            return jr;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        public static JsonResponse DeleteApi(int idCatalogue)
        {
            JsonResponse jr = new JsonResponse();
            var CatalogueFromDb = getCatalogueBy("IdCatalogue", idCatalogue.ToString());
            if (CatalogueFromDb == null)
            {
                jr.success = false;
                jr.message = "Error while Deleting";
                return jr;
            }
            else
            {
                jr = DeleteCatalogue(idCatalogue);
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
