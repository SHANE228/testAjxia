using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace txtAjaxyy.Models
{
    public partial class staff : DbContext
    {
        public staff()
            : base("name=staff")
        {
        }

        public virtual DbSet<staffs> staffs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
