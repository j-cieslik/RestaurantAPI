using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class MinimumRestaurantsRequirement : IAuthorizationRequirement
    {
        public int Count { get; }
        public ResourceOperation ResourceOperation { get; }

        public MinimumRestaurantsRequirement(int count)
        {
            Count = count;
        }

    }
}
