using AhmedovTravel.Core.Contracts;
using AhmedovTravel.Core.Models.Destination;
using AhmedovTravel.Infrastructure.Data.Common;
using AhmedovTravel.Infrastructure.Data.Entities;
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
 
    }
}
