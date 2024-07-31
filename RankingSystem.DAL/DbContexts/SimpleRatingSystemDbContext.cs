using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RankingSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingSystem.DAL.DbContexts
{
    public class SimpleRatingSystemDbContext:IdentityDbContext<ApplicationUser>
    {
        public SimpleRatingSystemDbContext(DbContextOptions<SimpleRatingSystemDbContext> options):base(options)
        {
            
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
