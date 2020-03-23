using System;
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
    public class BeestjeServiceTests
    {
        private ModelMocks _modelMocks;

        private static Beestje testBeestje = new Beestje()
        {
            BeestjeAccessoires = new List<BeestjeAccessoires>(),
            BookingBeestjes = new List<BookingBeestje>(),
            Image = "test.png",
            Price = 200,
            Type = Type.Boerderij,
            Id = 1000
        };


        public BeestjeServiceTests()
        {
            _modelMocks = new ModelMocks();
            _dbMock = _modelMocks.Context;

            var beestjeRepository = new BeestjeRepository(_dbMock);
            var accessoireRepository = new AccessoireRepository(_dbMock);

            _beestjeService = new BeestjeService(beestjeRepository, accessoireRepository);
        }


        [Fact]
        public async  void CanAddGet()
        {
            var savedBeestje = await _beestjeService.CreateBeestje(testBeestje);
            Assert.NotNull(savedBeestje);
            Assert.Equal(testBeestje, savedBeestje);
        }

        [Fact]
        public async void CanGetBeestjes()
        {
            var beestjes = await _beestjeService.GetBeestjes();
            Assert.NotEmpty(beestjes);
            Assert.Contains(_modelMocks.Beestjes[0], beestjes);
        }


        private readonly ApplicationDbContext _dbMock;
        private readonly BeestjeService _beestjeService;
    }
}
