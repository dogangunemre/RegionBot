using Microsoft.EntityFrameworkCore;
using RegionBot.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace  RegionBot.Context
{
    public class EntityContext : DbContext
    {

        public DbSet<Region> Regions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Region_db;Trusted_Connection=True;");
        }
    }
}
