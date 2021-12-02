using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MillionAndUp.Core.Aplication.Interfaces;
using MillionAndUp.Core.Application.Feautures.Owners.Commands.CreateOwnerCommand;
using MillionAndUp.Core.Application.Helpers;
using MillionAndUp.Core.Domain.Entities;
using MillionAndUp.Infraestructure.Persistence.Contexts;
using MillionAndUp.Infraestructure.Persistence.Repositories;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MillionAndUp.Core.Application.Feautures.Owners.Commands
{
    [TestFixture]
    public class CreateOwnerCommandNUnitTests
    {
        private CreateOwnerCommandHandler handlerCreateOwner;
        private IDateTimeService _dateTime;


        [SetUp]
        public void Setup()
        {
           
            var fixture = new Fixture();            
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var ownerRecords = fixture.CreateMany<Owner>().ToList();

            ownerRecords.Add(fixture.Build<Owner>()
                .With(t => t.Id, 0)
                .Create()
            );

            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: $"ApplicationDBContext-{Guid.NewGuid()}")
                .Options;


            var applicationDBContextFake = new ApplicationDBContext(options, _dateTime)  ;
            applicationDBContextFake.Owners.AddRange(ownerRecords);

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();
            var repository = new MyRepositoryAsync<Owner>(applicationDBContextFake);

            handlerCreateOwner = new CreateOwnerCommandHandler(repository, mapper);
        }

        [Test]
        public async Task CreateOwnerCommandHandler_AddOwner_RetunrsNumber()
        {
            CreateOwnerCommand.CreateOwnerCommand request = new();
            request.Name = "Name";
            request.Address = "Address";
            request.Birthday = DateTime.UtcNow.AddYears(-20);

            var resultado = await handlerCreateOwner.Handle(request, new CancellationToken());

            Assert.IsTrue(resultado.Data > 0 && resultado.Succeded);
        }
    }
}
