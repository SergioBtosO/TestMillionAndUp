using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MillionAndUp.Core.Aplication.Interfaces;
using MillionAndUp.Core.Application.Feautures.Owners.Queries.GetAllOwners;
using MillionAndUp.Core.Application.Helpers;
using MillionAndUp.Core.Domain.Entities;
using MillionAndUp.Infraestructure.Persistence.Contexts;
using MillionAndUp.Infraestructure.Persistence.Repositories;
using MillionAndUp.Infraestructure.Shared.Services;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.Owners.Queries
{
    [TestFixture]
    public class GetAllOwnersQueryNUnitTests
    {
        private GetAllOwnersQueryHandler handlerAllOwners;
        private readonly IDateTimeService _dateTime;
       
        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture();

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var ownerRecords = fixture.CreateMany<Owner>().ToList();

            ownerRecords.Add(fixture.Build<Owner>()
                .With(t => t.Id,0)
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

            handlerAllOwners = new GetAllOwnersQueryHandler(repository, mapper);
        }

        [Test]
        public async Task GetAllOwnersQueryHandler_QueryOwners_RetunrsTrue()
        {
            GetAllOwnersQuery request = new();
            var resultados = await handlerAllOwners.Handle(request, new CancellationToken());

            Assert.IsNotNull(resultados);
        }
    }
}
