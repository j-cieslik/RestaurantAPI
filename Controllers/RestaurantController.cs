using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Interfaces;
using RestaurantAPI.Models;
using RestaurantAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
        {
            //dodanie [ApiController] sprawia, że model jest walidowany
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            
            _restaurantService.UpdateRestaurant(id, dto);

            return Ok();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Manager")]
        [Authorize(Roles = "Admin,Manager")] //prioretet ma atrybut umieszczony tu
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var restaurantID = _restaurantService.Create(dto);    

            return Created($"/api/restaurant/{restaurantID}", null);
        }


        [HttpGet]
        [Authorize(Policy = "HasNationality")] //policy musi pokrywać się z tą w klasie startup
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDtos = _restaurantService.GetAll();

            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurantDto = _restaurantService.GetById(id);

            return Ok(restaurantDto);
        }
    }
}
