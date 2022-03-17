using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.Entity
{
    public class Catalogue
    {
        [Required]
        public int IdCatalogue { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Description { get; set; }

        public Catalogue()
        { }

        public Catalogue( string pTitle, string pShortTitle, string pDescription)
        {
           
            this.Title = pTitle;
            this.ShortTitle = pShortTitle;
            this.Description = pDescription;
        }
    }
}
