using System;
using Models;

namespace UnitTests.Helpers
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