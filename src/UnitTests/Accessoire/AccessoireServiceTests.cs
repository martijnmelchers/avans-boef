using DomainServices;
using Models.Repository;
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Accessoire
{

    [Collection("Database collection")]
    public class AccessoireServiceTests
    {
        private readonly ModelMocks _modelMocks;
        private readonly AccessoireService _accessoireService;


        private static readonly Models.Accessoire _testAccessoire = new Models.Accessoire()
        {
            Name = "test accessoire",
            Price = 200,
            Id = 1000
        };


        public AccessoireServiceTests(DatabaseFixture fixture)
        {
            _modelMocks = fixture.Mocks;
            var accessoireRepository = new AccessoireRepository(fixture.Db);
            _accessoireService = new AccessoireService(accessoireRepository);
        }

        [Fact]
        public async void CanCreateAccessoire()
        {
            var accessoire = await _accessoireService.CreateAccessoire(_testAccessoire);
            Assert.NotNull(accessoire);
            Assert.Contains(accessoire, await _accessoireService.GetAccessoires());
        }

        [Fact]
        public async void CanGetAccessoire()
        {
            var accessoire = await _accessoireService.GetAccessoire(_modelMocks.Accessoires[1].Id);
            Assert.NotNull(accessoire);
            Assert.Equal(_modelMocks.Accessoires[1].Name, accessoire.Name);
        }

        [Fact]
        public async void CanEditAccessoire()
        {
            var accessoire = await _accessoireService.GetAccessoire(_modelMocks.Accessoires[2].Id);
            accessoire.Name = "Barstoel";

            await _accessoireService.EditAccessoire(_modelMocks.Accessoires[2].Id, accessoire);
            var updatedAccessoire = await _accessoireService.GetAccessoire(_modelMocks.Accessoires[2].Id);
            Assert.True((updatedAccessoire.Name == "Barstoel"));
        }

        [Fact]
        public async void CanDeleteAccessoire()
        {
            await _accessoireService.DeleteAccessoire(_modelMocks.Accessoires[3].Id);
            Assert.Null(await _accessoireService.GetAccessoire(_modelMocks.Accessoires[3].Id));
        }
    }
}
