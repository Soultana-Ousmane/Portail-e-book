using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.Entity
{
    public class Ebook : Document
    {
        [Required]
        public int IdEbook { get; set; }
        public int EditionNum { get; set; }
        public string EditionPlace { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Category { get; set; }
        public int NbPages { get; set; }

        public Ebook() : base()
        { }

        public Ebook(string pEditor, string pDoi, string pOriginalTitle, string pSubtitle, string pForeword, string pKeywords, string pOriginalLanguage, string pState, Decimal pPrice, DateTime pPublicationDate, string pCountry, string pPhysicalDescription, string pAccompanyingMaterials, int pAccompanyingMaterialsNb, int pVolumeNb, string pAbstract, string pNotes, int pEditionNum, string pEditionPlace, string pISBN, string pGenre, string pCategory, int pNbPages) : base(pEditor, pDoi, pOriginalTitle, pSubtitle, pForeword, pKeywords, pOriginalLanguage, pState, pPrice, pPublicationDate, pCountry, pPhysicalDescription, pAccompanyingMaterials, pAccompanyingMaterialsNb, pVolumeNb, pAbstract, pNotes)
        {
            this.EditionNum = pEditionNum;
            this.EditionPlace = pEditionPlace;
            this.ISBN = pISBN;
            this.Genre = pGenre;
            this.Category = pCategory;
            this.NbPages = pNbPages;

        }
    }
}
