using Portail_e_book.Models.DAL;
using Portail_e_book.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.BLL
{
    public class BLL_Collection
    {
        public static IEnumerable<Collection> getAllCollection()
        {
            return DAL_Collection.getAllCollection();
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Get the details of a particular Collection
        public static Collection getCollectionBy(string Champ, string Valeur)
        {
            return DAL_Collection.getCollectionBy(Champ, Valeur);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        public static IEnumerable<Collection> getAllCollectiontBy(string Field, string Value)
        {
            return DAL_Collection.getAllCollectionBy(Field, Value);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Add new Collection record
        public static JsonResponse AddCollection(Collection collection)
        {
            return DAL_Collection.AddCollection(collection);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Update the records of a particluar Collection
        public static JsonResponse UpdateCollection(Collection collection)
        {
            return DAL_Collection.UpdateCollection(collection);
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Delete the records of a particular Collection
        public static JsonResponse DeleteCollection(int? idCollection)
        { 
            
            /*----------------------------------*/
            List<Document> listDocument = DAL_Document.getAllDocumentBy("IdCollection", idCollection.ToString());
            for (int i = 0; i < listDocument.Count; i++)
            {
                BLL_Document.DeleteDocument( listDocument[i].IdDocument);
            }
            /*------------------------------*/
            return DAL_Collection.DeleteCollectionBy("IdCollection", idCollection.ToString());

        }
        /*-----------------------------------------------------------------------------------------------------------------------------------*/
        //Create or Update Collection
        public static Collection Upsert(int? idCollection)
        {
            if (idCollection == null)
            {
                //create
                return new Collection();
            }
            else
            {
                //Update
                return getCollectionBy("IdCollection", idCollection.ToString());
            }
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        #region API Calls
        public static JsonResponse UpsertApi(Collection collection)
        {
            JsonResponse jr = new JsonResponse();
            jr.success = false;
            jr.message = "Erreur";
            if (collection.IdCollection == 0)
            {
                //create
                jr = AddCollection(collection);
            }
            else
            {
                //update
                jr = UpdateCollection(collection);
            }
            return jr;
        }
        /*--------------------------------------------------------------------------------------------------------------------------------------*/
        public static JsonResponse DeleteApi(int idCollection)
        {
            JsonResponse jr = new JsonResponse();
            var CollectionFromDb = getCollectionBy("IdCollection", idCollection.ToString());
            if (CollectionFromDb == null)
            {
                jr.success = false;
                jr.message = "Error while Deleting";
                return jr;
            }
            else
            {
                jr = DeleteCollection(idCollection);
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
