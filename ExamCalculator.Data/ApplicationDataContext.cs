using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ExamCalculator.Data
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{Path.DirectorySeparatorChar}exams.sqlite";
        }

        public DbSet<Pupil> Pupils { get; set; }

        public DbSet<Group> Groups { get; set; }

        public string DbPath { get; }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options
                .UseSqlite($"Data Source={DbPath}");
            //.EnableSensitiveDataLogging()
            //.LogTo(Console.WriteLine);
        }

        public static void EnsureDatabase()
        {
            using var db = new ApplicationDataContext();
            Console.WriteLine($"Using DB at {db.DbPath}");
            db.Database.EnsureCreated();
            var pendingMigrations = db.Database.GetPendingMigrations();
            Console.WriteLine($"There are {pendingMigrations.Count()} pending migrations");

            db.Database.Migrate();
        }
    }
}