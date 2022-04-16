using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantAPI.Authorization
{
    public class MinimumRestaurantsRequirementHandler : AuthorizationHandler<MinimumRestaurantsRequirement>
    {
        private readonly RestaurantDbContext _dbContext;

        public MinimumRestaurantsRequirementHandler(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsRequirement requirement)
        {
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var restaurants = _dbContext.Restaurants.Where(p => p.CreatedById == int.Parse(userId)).ToList();

            if (restaurants.Count >= requirement.Count)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
