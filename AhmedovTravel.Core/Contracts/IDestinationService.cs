﻿using AhmedovTravel.Core.Models.Destination;

namespace AhmedovTravel.Core.Contracts
{
    public interface IDestinationService
    {
        Task AddDestinationAsync(AddDestinationViewModel model);
        Task<IEnumerable<AllDestinationsViewModel>> GetAllAsync();
        Task AddDestinationToCollectionAsync(int destinationId, string userId);
        Task<IEnumerable<MineDestinationsViewModel>> ShowDestinationCollectionAsync(string userId);
        Task RemoveDestinationFromCollectionAsync(int destinationId, string userId);
        Task EditDestinationAsync(int destinationId, EditDestinationViewModel model);
        Task<bool> Exists(int id);
        Task<AllDestinationsViewModel> DestinationDetailsById(int id);
    }
}
