using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.Entity
{
    public class Collection
    {
        [Required]
        public int IdCollection { get; set; }
        public string Editor { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Description { get; set; }
        

        public Collection()
        { }

        public Collection(string pEditor, string pTheme, string pTitle, string pShortTitle, string pDescription)
        {
            this.Editor = pEditor;
            this.Theme = pTheme;
            this.Title = pTitle;
            this.ShortTitle = pShortTitle;
            this.Description = pDescription;
        }
    }
}
