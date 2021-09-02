using Microsoft.EntityFrameworkCore;

namespace ExamCalculator.Data.Test
{
    public class TestDataContext : ApplicationDataContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Core SQLite configuration
            options
                .UseSqlite("Data Source=:memory:");
        }
    }
}