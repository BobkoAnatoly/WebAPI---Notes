﻿using Application.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Model.Database
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; } = null!;

    }
}
