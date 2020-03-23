using System;
using Models;
using UnitTests.Helpers;

namespace UnitTests
{
    public class DatabaseFixture : IDisposable
    {
        public ApplicationDbContext Db { get; }    
        public ModelMocks Mocks { get; }    
        public DatabaseFixture()
        {
            Mocks = new ModelMocks();
            Db = Mocks.InitializeDatabase();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}