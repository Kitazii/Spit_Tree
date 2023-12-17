using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace K_Burns_SpitTree.Models
{
    public class SpitTreeDbContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }

        public SpitTreeDbContext()
            : base("SpitTreeConnection2", throwIfV1Schema: false)
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        public static SpitTreeDbContext Create()
        {
            return new SpitTreeDbContext();
        }
    }
}