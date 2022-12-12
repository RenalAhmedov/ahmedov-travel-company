﻿using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Services;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastrucutre.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhmedovTravel.Tests.ServiceTests
{
    public class TransportServiceTests
    {
        private IRepository repo;
        private ITransportService transportService;
        private ApplicationDbContext data;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("RoomService")
            .Options;

            data = new ApplicationDbContext(contextOptions);

            repo = new Repository(data);
            transportService = new TransportService(repo);

            data.Database.EnsureDeleted();
            data.Database.EnsureCreated();
        }



        [TearDown]
        public void TearDown()
        {
            data.Dispose();
        }
    }
}
