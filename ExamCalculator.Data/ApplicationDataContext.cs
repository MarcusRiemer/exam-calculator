using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ExamCalculator.Data
{
    public class ApplicationDataContext : DbContext
    {
        public DbSet<Pupil> Pupils { get; set; }

        public string DbPath { get; private set; }
        
        public ApplicationDataContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}exams.sqlite";
        }
        
        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        public static void EnsureDatabase()
        {
            using var db = new ApplicationDataContext();
            Console.WriteLine($"Using DB at {db.DbPath}");
            db.Database.EnsureCreated();
            var pendingMigrations = db.Database.GetPendingMigrations();
            Console.Write($"There are {pendingMigrations.Count()} pending migrations");
                
            db.Database.Migrate();
        }
    }
}