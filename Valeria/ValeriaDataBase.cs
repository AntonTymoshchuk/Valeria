using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Valeria
{
    class ValeriaDataBase : DbContext
    {
        public ValeriaDataBase() : base("Data Source=LAPTOP-OCJDU2KO;Initial Catalog=Valeria;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        { }
        public DbSet<DayInfo> Days { get; set; }
    }
}
