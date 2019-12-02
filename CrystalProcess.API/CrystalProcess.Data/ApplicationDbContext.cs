using System;
using CrystalProcess.Models;
using Microsoft.EntityFrameworkCore;

namespace CrystalProcess.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
