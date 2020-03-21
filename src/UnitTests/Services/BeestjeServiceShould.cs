using DomainServices;
using DomainServices.Interfaces;
using Models.Repository;
using Models;
using Moq;

namespace UnitTests.Services
{
    class BeestjeServiceShould
    {
        private readonly IBeestjeService _beestjeService;

        public BeestjeServiceShould()
        {
            var _dbMock = new Mock<ApplicationDbContext>();
            
            _beestjeService = new BeestjeService(new BeestjeRepository(_dbMock.Object));
        }
    }
}
