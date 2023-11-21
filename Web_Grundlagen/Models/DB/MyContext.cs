using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Grundlagen.Models
{

    public class MyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // für den Pomelo-MySQL-Treiber
            // der MySQL-Sevrver befindet sich auf localhost, 
            //      der Datenbankname soll orm_4a_g2 lauten 
            //      die USer und sein Passwort(Anmeldung am MySQL.Server 
            //      ebenfalls angegeben werden 
            string connectionString = "Server=localhost;database=WebApp;user=root;password=2G6ao7jz";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }

    }
}
