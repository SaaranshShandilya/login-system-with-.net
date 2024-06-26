using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loginsystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Loginsystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}