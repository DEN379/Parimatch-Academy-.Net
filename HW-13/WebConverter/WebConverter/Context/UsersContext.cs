using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebConverter.Models;

namespace WebConverter.Context
{
    /// <summary>
    /// Users Context
    /// </summary>
    public class UsersContext : DbContext
    {
        /// <summary>
        /// Data Base Set Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Users Context constructor
        /// </summary>
        /// <param name="options"></param>
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {

        }

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users");
        }
    }
}
