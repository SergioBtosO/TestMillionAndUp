using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MillionAndUp.Core.Aplication.Interfaces;
using MillionAndUp.Core.Application.Feautures.PropertyImages.Queries.GetPropertyImagesById;
using MillionAndUp.Core.Application.Helpers;
using MillionAndUp.Core.Domain.Entities;
using MillionAndUp.Infraestructure.Persistence.Contexts;
using MillionAndUp.Infraestructure.Persistence.Repositories;
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
    public class GetPropertyImageByIdQueryNUnitTests
    {
        private GetPropertyImageByIdQuery.GetPropertyImageByIdQueryHandler handlerByIdPropertyImage;
        private readonly IDateTimeService _dateTime;
        private int IdPropertyImage;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            IdPropertyImage = 999;
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var propertyImageRecords = fixture.CreateMany<PropertyImage>().ToList();

            propertyImageRecords.Add(fixture.Build<PropertyImage>()
                .With(t => t.Id, IdPropertyImage)
                .Create()
            );

            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: $"ApplicationDBContext-{Guid.NewGuid()}")
                .Options;

            var applicationDBContextFake = new ApplicationDBContext(options, _dateTime);
            applicationDBContextFake.PropertyImages.AddRange(propertyImageRecords);

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();
            var repository = new MyRepositoryAsync<PropertyImage>(applicationDBContextFake);

            handlerByIdPropertyImage = new GetPropertyImageByIdQuery.GetPropertyImageByIdQueryHandler(repository, mapper);
        }

        [Test]
        public async Task GetOwnerByIdQueryHandler_InputIdOwner_RetunrsNotNull()
        {
            GetPropertyImageByIdQuery request = new()
            {
                Id = IdPropertyImage
            };
            var resultado = await handlerByIdPropertyImage.Handle(request, new CancellationToken());

            Assert.IsNotNull(resultado);
        }
    }
}
