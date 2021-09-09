using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NUnit.Framework;

namespace ExamCalculator.Data.Test
{
    /// <summary>
    /// An in memory SQLite database that always keeps a connection open to
    /// ensure that the database is not lost during the lifetime of this
    /// instance.
    /// </summary>
    public class TestDatabase : IDisposable
    {
        public TestDatabase()
        {
            TestDataContext = new TestDataContext();
            TestDataContext.Database.OpenConnection();
            
            Assert.IsTrue(Database.EnsureCreated());
        }

        public TestDataContext TestDataContext { get; }

        public DatabaseFacade Database => TestDataContext.Database;

        public void Dispose()
        {
            TestDataContext.Database.CloseConnection();
        }
    }
}