using System.Collections.Generic;
using System.Text;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Repository;
using Moq;
using UnitTests.Helpers;
using Xunit;
using Type = Models.Type;

namespace UnitTests
{
    [Collection("Database collection")]
    public class BeestjeServiceTests
    {
        private readonly ModelMocks _modelMocks;
        
        private static readonly Beestje _testBeestje = new Beestje()
        {
            BeestjeAccessoires = new List<BeestjeAccessoires>(),
            BookingBeestjes = new List<BookingBeestje>(),
            Image = "test.png",
            Price = 200,
            Type = Type.Boerderij,
            Id = 1000
        };

        public BeestjeServiceTests(DatabaseFixture fixture)
        {
            _modelMocks = fixture.Mocks;

            var beestjeRepository = new BeestjeRepository(fixture.Db);
            var accessoireRepository = new AccessoireRepository(fixture.Db);

            _beestjeService = new BeestjeService(beestjeRepository, accessoireRepository);
        }


        [Fact]
        public async  void CanAddGet()
        {
            var savedBeestje = await _beestjeService.CreateBeestje(_testBeestje);
            
            Assert.NotNull(savedBeestje);
            Assert.Equal(_testBeestje, savedBeestje);
        }

        [Fact]
        public async void CanGetBeestjes()
        {
            var beestjes = await _beestjeService.GetBeestjes();
            Assert.NotEmpty(beestjes);
            Assert.Equal(_modelMocks.Beestjes.Count, beestjes.Count);
        }


        private readonly BeestjeService _beestjeService;
    }
}
