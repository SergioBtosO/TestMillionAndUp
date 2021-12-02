using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MillionAndUp.Core.Aplication.Interfaces;
using MillionAndUp.Core.Application.Feautures.Owners.Queries.GetOwnerById;
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

namespace MillionAndUp.Core.Application.Feautures.Owners.Queries
{
    [TestFixture]
    public class GetOwnerByIdQueryNUnitTests
    {
        private GetOwnerByIdQuery.GetOwnerByIdQueryHandler handlerByIdOwner;
        private readonly IDateTimeService _dateTime;
        private int IdOwner;

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();
            IdOwner = 999;
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var ownerRecords = fixture.CreateMany<Owner>().ToList();

            ownerRecords.Add(fixture.Build<Owner>()
                .With(t => t.Id, IdOwner)
                .Create()
            );

            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: $"ApplicationDBContext-{Guid.NewGuid()}")
                .Options;

            var applicationDBContextFake = new ApplicationDBContext(options, _dateTime);
            applicationDBContextFake.Owners.AddRange(ownerRecords);

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();
            var repository = new MyRepositoryAsync<Owner>(applicationDBContextFake);

            handlerByIdOwner = new GetOwnerByIdQuery.GetOwnerByIdQueryHandler(repository, mapper);
        }

        [Test]
        public async Task GetOwnerByIdQueryHandler_InputIdOwner_RetunrsNotNull()
        {
            GetOwnerByIdQuery request = new()
            {
                Id = IdOwner
            };
            var resultado = await handlerByIdOwner.Handle(request, new CancellationToken());

            Assert.IsNotNull(resultado);
        }
    }
}
