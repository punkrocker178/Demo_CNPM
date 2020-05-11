using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext (DbContextOptions<StoreContext> options) : base(options)
        {

        }

        public DbSet<Models.Item> Item { get; set; }
        public DbSet<Models.Category> Category { get; set; }
        public DbSet<Models.Image> Image { get; set; }

    }
}
