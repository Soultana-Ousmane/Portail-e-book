using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models.Entity
{
    public class Author
    {
        [Required]
        public int IdAuthor { get; set; }
        [Required]
        public string Orcld { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ArName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Civility { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public int PostalCode { get; set; }

        public string Country { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Biography { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
       

        public Author()
        { }

        public Author(string pOrcld, string pFirstName, DateTime pDateOfBirth, string pAdress,string pCountry, string pEmail)
        {
            this.Orcld = pOrcld;
            this.FirstName = pFirstName;
            this.DateOfBirth = pDateOfBirth;
            this.Adress = pAdress;
            this.Country = pCountry;
            this.Email = pEmail;
        }
    }
}
