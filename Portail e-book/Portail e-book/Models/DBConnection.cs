using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Portail_e_book.Models
{
    public class DBConnection
    {
        static string DbConnnectionString = @"Data Source=DESKTOP-SOAFB01\SQLEXPRES;Initial Catalog=Portail e-book;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(DbConnnectionString);
        }
    }
}
