using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace txtAjaxyy.Models
{
    public partial class belltables : DbContext
    {
        public belltables()
            : base("name=belltables")
        {
        }

        public virtual DbSet<bellTable> bellTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
