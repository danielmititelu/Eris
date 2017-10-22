using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eris.Models
{
    public class FirstTableContext : DbContext
    {
        public FirstTableContext(DbContextOptions<FirstTableContext> options) : base(options)
        {

        }
        public DbSet<FirstTable> FirstTables { get; set; }
    }
}
