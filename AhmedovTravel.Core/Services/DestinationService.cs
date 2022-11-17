﻿using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Destination;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhmedovTravel.Core.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly IRepository repo;

        public DestinationService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task AddDestinationAsync(AddDestinationViewModel model)
        {
            var destination = new Destination()
            {
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
                Price = model.Price
            };
            await repo.AddAsync(destination);
            await repo.SaveChangesAsync();
        }

        public async Task AddDestinationToCollectionAsync(int destinationId, string userId)
        {
            var user = await repo.All<User>()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }

            var destination = await repo.All<Destination>()
                 .FirstOrDefaultAsync(d => d.Id == destinationId);

            if (destination == null)
            {
                throw new ArgumentException("Invalid Destination ID");
            }

            if (!user.UsersDestinations.Any(d => d.DestinationId == destinationId))
            {
                user.UsersDestinations.Add(new UserDestination()
                {
                    DestinationId = destination.Id,
                    UserId = user.Id,
                    Destination = destination,
                    User = user
                });
            }
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllDestinationsViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<Destination>()
               .OrderBy(d => d.Id)
               .Select(d => new AllDestinationsViewModel()
               {
                   Id = d.Id,
                   ImageUrl = d.ImageUrl,
                   Price = d.Price,
                   Rating = d.Rating,
                   Title = d.Title
               })
               .ToListAsync();
        }
    }
}
