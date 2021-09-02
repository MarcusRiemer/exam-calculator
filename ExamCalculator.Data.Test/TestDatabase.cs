using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ExamCalculator.Data.Test
{
    public class TestDatabase : IDisposable
    {
        public TestDatabase()
        {
            TestDataContext = new TestDataContext();
            TestDataContext.Database.OpenConnection();
        }

        public TestDataContext TestDataContext { get; }

        public DatabaseFacade Database => TestDataContext.Database;

        public void Dispose()
        {
            TestDataContext.Database.CloseConnection();
        }
    }
}