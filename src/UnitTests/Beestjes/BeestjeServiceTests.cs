using System.Collections.Generic;
using DomainServices;
using Models;
using Models.Repository;
using UnitTests.Helpers;
using Xunit;
using Type = Models.Type;

namespace UnitTests.Beestjes
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
        public async  void CanAddBeestje()
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

        [Fact]
        public async void CanEditBeestje()
        {
            var beestje = await _beestjeService.GetBeestje(2);
            beestje.Name = "Joep";
            beestje.Price = 300;
            beestje.Image = "ijsbeer.png";
            beestje.Type = Type.Sneeuw;

            await _beestjeService.EditBeestje(2, beestje);

            var beestjeUpdated = await _beestjeService.GetBeestje(2);

            Assert.True((beestjeUpdated.Name == "Joep"));
        }

        [Fact]
        public async void CanDeleteBeestje()
        {
            await _beestjeService.DeleteBeestje(1);
            _modelMocks.Beestjes.Remove(_modelMocks.Beestjes[0]);
            Assert.Null(await _beestjeService.GetBeestje(1));
        }

        [Fact]
        public async void CanGetAvailableBeestjes()
        {
            var booking = _modelMocks.Bookings[2];
            var availableBeestjes =  await  _beestjeService.GetAvailableBeestjes(booking);
            
            Assert.NotEmpty(availableBeestjes);
            Assert.False(availableBeestjes[8].available);
        }

        private readonly BeestjeService _beestjeService;
    }
}
