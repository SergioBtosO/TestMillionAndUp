using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MillionAndUp.Core.Aplication.Interfaces;
using MillionAndUp.Core.Application.Feautures.PropertyImages.Queries.GetAllPropertyImages;
using MillionAndUp.Core.Application.Helpers;
using MillionAndUp.Core.Domain.Entities;
using MillionAndUp.Infraestructure.Persistence.Contexts;
using MillionAndUp.Infraestructure.Persistence.Repositories;
using MillionAndUp.Infraestructure.Shared.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.PropertyImages.Queries
{
    [TestFixture]
    public class GetAllPropertyImagesQueryNUnitTests
    {
        private GetAllPropertyImagesQueryHandler handlerAllPropertyImages;
        private IDateTimeService _dateTime;

        [SetUp]
        public void Setup()
        {

            var fixture = new Fixture();

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var propertyImagesRecords = fixture.CreateMany<PropertyImage>().ToList();

            propertyImagesRecords.Add(fixture.Build<PropertyImage>()
                .With(t => t.Id, 0)
                .Create()
            );

            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: $"ApplicationDBContext-{Guid.NewGuid()}")
                .Options;

            var applicationDBContextFake = new ApplicationDBContext(options, _dateTime);
            applicationDBContextFake.PropertyImages.AddRange(propertyImagesRecords);

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();
            var repository = new MyRepositoryAsync<PropertyImage>(applicationDBContextFake);

            handlerAllPropertyImages = new GetAllPropertyImagesQueryHandler(repository, mapper);
        }

        [Test]
        public async Task GetAllPropertyImagesQueryHandler_QueryPropertyImages_RetunrsTrue()
        {
            GetAllPropertyImagesQuery request = new();
            var resultados = await handlerAllPropertyImages.Handle(request, new CancellationToken());

            Assert.IsNotNull(resultados);
        }
    }
}
