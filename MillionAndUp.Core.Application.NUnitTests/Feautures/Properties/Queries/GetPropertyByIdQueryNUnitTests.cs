using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MillionAndUp.Core.Aplication.Interfaces;
using MillionAndUp.Core.Application.Feautures.Properties.Queries.GetPropertyById;
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

namespace MillionAndUp.Core.Application.Feautures.Properties.Queries
{
    [TestFixture]
    public class GetPropertyByIdQueryNUnitTests
    {
        private GetPropertyByIdQuery.GetPropertyByIdQueryHandler handlerByIdProperty;
        private readonly IDateTimeService _dateTime;
        private int IdProperty;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            IdProperty = 999;
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var propertyRecords = fixture.CreateMany<Property>().ToList();

            propertyRecords.Add(fixture.Build<Property>()
                .With(t => t.Id, IdProperty)
                .Create()
            );

            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: $"ApplicationDBContext-{Guid.NewGuid()}")
                .Options;

            var applicationDBContextFake = new ApplicationDBContext(options, _dateTime);
            applicationDBContextFake.Properties.AddRange(propertyRecords);

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();
            var repository = new MyRepositoryAsync<Property>(applicationDBContextFake);

            handlerByIdProperty = new GetPropertyByIdQuery.GetPropertyByIdQueryHandler(repository, mapper);
        }

        [Test]
        public async Task GetOwnerByIdQueryHandler_InputIdOwner_RetunrsNotNull()
        {
            GetPropertyByIdQuery request = new()
            {
                Id = IdProperty
            };
            var resultado = await handlerByIdProperty.Handle(request, new CancellationToken());

            Assert.IsNotNull(resultado);
        }
    }
}
