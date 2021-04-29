﻿using Microsoft.EntityFrameworkCore;

namespace SULS.Data
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContext db)
        :base()
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Problem> Problems { get; set; }

        public DbSet<Submission> Submissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Suls;Integrated Security= true;");
            }
        }
    }
}