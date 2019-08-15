﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectCoffeAPI.Models;
using ProjectCoffeAPI.Services;

namespace ProjectCoffeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeController : ControllerBase
    {
        private readonly ICoffeService _coffeService;
        public CoffeController(ICoffeService coffeService)
        {
            _coffeService = coffeService;
        }

        // GET: api/Coffe
        [HttpGet]
        public IActionResult Get()
        {
            var result = _coffeService.GetUserList();
            return Ok(result);
        }

        // GET: api/Coffe/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Coffe
        [HttpPost]
        public IActionResult AddUser([FromBody] UserModel user)
        {
            _coffeService.AddUserToUserList(user);
            return Ok();

        }

        // PUT: api/Coffe/5
        [HttpPut("{id}")]
        public void Put(string uid, [FromBody] UserModel value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string uid)
        {
        }
    }
}