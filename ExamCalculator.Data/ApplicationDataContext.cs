using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public DbSet<Exam> Exams { get; set; }

        public DbSet<ExamTask> ExamTasks { get; set; }

        public DbSet<Examination> Examinations { get; set; }

        public DbSet<ExaminationTaskResult> ExaminationTaskResults { get; set; }

        public string DbPath { get; }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Core SQLite configuration
            options
                .UseSqlite($"Data Source={DbPath}");

            // Useful for debugging
            options
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<GroupPupil>()
                .HasKey(gp => new {gp.GroupId, gp.PupilId});

            modelBuilder.Entity<GroupPupil>()
                .HasOne(gp => gp.Group)
                .WithMany(g => g.G)
                .HasForeignKey(gp => gp.GroupId);*/
        }

        /// <summary>
        /// Ensures that the database is in a usable state
        /// </summary>
        public static void EnsureDatabase()
        {
            using var db = new ApplicationDataContext();
            Console.WriteLine($"Using DB at {db.DbPath}");
            
            // Returns true if database was created at this call
            if (!db.Database.EnsureCreated())
            {
                // Database already exists, do migrations
                var pendingMigrations = db.Database.GetPendingMigrations();
                Console.WriteLine($"There are {pendingMigrations.Count()} pending migrations");

                if (pendingMigrations.Any())
                {
                    db.Database.Migrate();
                }   
            }
        }

        // I sometimes need to test EF Core behaviour and use this method to do so
        public void Startup()
        {
            
            /*
            var p = Pupils.Add(new Pupil { PupilId = Guid.NewGuid(), FirstName = "A", LastName = "Z"});
            var g =Groups.Add(new Group { GroupId = Guid.NewGuid(), Name = "K"});

            SaveChanges();

            var pDb = Pupils.Include(pArg => pArg.Groups).First();
            pDb.Groups.Add(g.Entity);
            
            SaveChanges();
            */
        }
    }
}